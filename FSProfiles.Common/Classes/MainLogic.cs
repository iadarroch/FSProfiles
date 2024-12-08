using System.Diagnostics;
using System.Drawing;
using FSProfiles.Common.Models;
using FSProfiles.Common.Models.Source;

namespace FSProfiles.Common.Classes
{
    public enum ContentMode {All, Assigned, New}
    public enum HostVersionType { Native2020, Native2024, Steam2020, Steam2024 }

    public class MainLogic
    {
        private readonly ColorSequencer _colorSequencer = new();
        #pragma warning disable CA1859
        private readonly IOutputFormatter _htmlFormatter = new XsltFormatter();
        private readonly ProgramArguments _programArguments;
        #pragma warning restore CA1859

        private readonly Dictionary<HostVersionType, FolderProcessorInstance> _hostVersions;

        public EventHandler<ProgressEvent>? OnStart;
        public EventHandler<ProgressEvent>? OnProgress;
        public EventHandler<ProgressEvent>? OnStop;

        public bool IncludeUnrecognised;

        public MainLogic(ProgramArguments programArguments)
        {
            _programArguments = programArguments;

            var ary = new FolderProcessorInstance[]
            {
                new(new FolderProcessorNative2020()),
                new(new FolderProcessorNative2024()),
                new(new FolderProcessorSteam2020()),
                new(new FolderProcessorSteam2024())
            };
            _hostVersions = ary.ToDictionary(k => k.HostVersion, v => v);
        }

        public string GetDefaultOutputFile()
        {
            var tempPath = Path.GetTempPath();
            return $"{tempPath}controllers.html";
        }

        public List<DetectedProfile> ProcessHostVersions()
        {
            var allProfiles = new List<DetectedProfile>();
            foreach (var hostVersion in _hostVersions)
            {
                var processor = hostVersion.Value;
                allProfiles.AddRange(processor.FolderProcessor.ProcessPath(processor.Path));
            }

            return allProfiles.OrderBy(dc => dc.Name).ToList();
        }

        public void GenerateBindingReport(string outputFile, List<DetectedProfile> profileList, ContentMode contentMode, bool includeUnrecognised)
        {
            OnStart?.Invoke(this, new ProgressEvent("Generating Binding Report", profileList.Count));
            IncludeUnrecognised = includeUnrecognised;
            try
            {
                var bindingReport = BuildBindingReport(profileList);
                bindingReport.ContentMode = contentMode;
                if (contentMode != ContentMode.All) FilterBindingList(bindingReport, contentMode);
                //Populate test data with selected bindings
                if (_programArguments.Debug)
                {
                    var defaultPath = "C:\\Development\\FSProfiles\\FSProfiles.Tests\\Data\\Bindings.xml";
                    if (!File.Exists(defaultPath))
                    {
                        defaultPath = $"{Path.GetTempPath()}Bindings.xml";
                    }
                    bindingReport.SerializeToFile(defaultPath);
                }
                _htmlFormatter.ConvertToHtml(bindingReport, outputFile);

                Process.Start(new ProcessStartInfo
                {
                    FileName = outputFile,
                    UseShellExecute = true
                });
            }
            finally
            {
                OnStop?.Invoke(this, new ProgressEvent("Completed"));
            }
        }

        public static void FilterBindingList(BindingReport bindingReport, ContentMode contentMode)
        {
            var sectionIndex = 0;
            while (sectionIndex < bindingReport.BindingList.Sections.Count)
            {
                var section = bindingReport.BindingList.Sections[sectionIndex];

                var itemIndex = 0;
                while (itemIndex < section.Items.Count)
                {
                    var item = section.Items[itemIndex];
                    switch (item)
                    {
                        case SubSection subSection:
                        {
                            var actionIndex = 0;
                            while (actionIndex < subSection.Actions.Count)
                            {
                                if (ShouldRemoveAction(subSection.Actions[actionIndex], contentMode))
                                {
                                    subSection.Actions.RemoveAt(actionIndex);
                                    continue;
                                }

                                actionIndex++;
                            }
                            if ((contentMode == ContentMode.Assigned && subSection.Actions.Count == 0) ||
                                (contentMode == ContentMode.New && subSection.Actions.Count == 0))
                            {
                                section.Items.RemoveAt(itemIndex);
                                continue;
                            }

                            break;
                        }

                        case SectionAction sectionAction:
                        {
                            if (ShouldRemoveAction(sectionAction, contentMode))
                            {
                                section.Items.RemoveAt(itemIndex);
                                continue;
                            }
                            break;
                        }
                    }

                    itemIndex++;
                }

                if ((contentMode == ContentMode.Assigned && section.Items.Count == 0) ||
                    (contentMode == ContentMode.New && section.Items.Count == 0))
                {
                    bindingReport.BindingList.Sections.RemoveAt(sectionIndex);
                    continue;
                }
                sectionIndex++;  //if we have not removed a section, move to the next one
            }
        }

        public static bool ShouldRemoveAction(SectionAction action, ContentMode contentMode)
        {
            var inputIndex = 0;
            while (inputIndex < action.Inputs.Count)
            {
                var bindings = action.Inputs[inputIndex].Bindings;
                if ((contentMode == ContentMode.Assigned && bindings.Count == 0) ||
                    (contentMode == ContentMode.New && bindings.Count != 0))
                {
                    action.Inputs.RemoveAt(inputIndex);
                    continue;
                }

                inputIndex++;
            }

            var bindingCount = action.Inputs.SelectMany(i => i.Bindings).Count();

            return ((contentMode == ContentMode.Assigned && bindingCount == 0) ||
                    (contentMode == ContentMode.New && bindingCount != 0));
        }

        public BindingReport BuildBindingReport(List<DetectedProfile> profileList)
        {
            var bindingReport = new BindingReport
            {
                BindingList = Serializer.DeserializeFromFile<BindingList>("KnownBindings.xml")
            };
            
            var inputDictionary = bindingReport.BindingList.ToInputDictionary();
            var progress = 0;
            foreach (var profile in profileList)
            {
                ProcessControllerProfile(bindingReport, inputDictionary, profile);
                OnProgress?.Invoke(this, new ProgressEvent(progress: ++progress));
                Thread.Sleep(10);
            }

            ApplyFormatting(bindingReport);

            return bindingReport;
        }

        public void ApplyFormatting(BindingReport bindingReport)
        {
            _colorSequencer.ResetDefaultColor(); //ensure color sequence always the same
            foreach (var section in bindingReport.BindingList.Sections)
            {
                section.BackColor ??= _colorSequencer.NextDefaultColor();
                var colorSub = section.BackColor?.Lighter(0.40f) ?? Color.Azure;
                var colorOdd = section.BackColor?.Lighter(0.85f) ?? Color.Azure;
                var colorEven = section.BackColor?.Lighter(0.70f) ?? Color.LightCyan;
                var rowNum = 0;
                foreach (var item in section.Items)
                {
                    switch (item)
                    {
                        case SubSection subSection:
                        {
                            subSection.BackColor = colorSub;
                            foreach (var sectionAction in subSection.Actions)
                            {
                                sectionAction.ActionName = sectionAction.ActionName!.TitleCase();
                                sectionAction.BackColor = (rowNum++ % 2 == 0) ? colorEven : colorOdd;
                            }
                            continue;
                        }
                        case SectionAction sectionAction:
                            sectionAction.ActionName = sectionAction.ActionName.TitleCase();
                            sectionAction.BackColor = (rowNum++ % 2 == 0) ? colorEven : colorOdd;
                            break;
                    }
                }
            }

            _colorSequencer.ResetDefaultColor(); //ensure color sequence always the same
            foreach (var context in bindingReport.UnrecognisedContexts)
            {
                context.BackColor ??= Color.LightGray.Lighter(0.25f);
                var colorOdd = context.BackColor?.Lighter(0.85f) ?? Color.Azure;
                var colorEven = context.BackColor?.Lighter(0.70f) ?? Color.LightCyan;
                var rowNum = 0;
                foreach (var action in context.Actions)
                {
                    //action.ActionName = action.ActionName.TitleCase();
                    action.BackColor = (rowNum++ % 2 == 0) ? colorEven : colorOdd;
                }
            }
        }

        public void ProcessControllerProfile(BindingReport bindingReport, Dictionary<string, ActionInput> inputDictionary, DetectedProfile profile)
        {
            bindingReport.SelectedControllers.Add(new SelectedController
                {
                    DeviceName = profile.ControllerDefinition.Device.DeviceName,
                    ProfileName = profile.ControllerDefinition.FriendlyName.Text,
                    ProfilePath = profile.Path
                });

            foreach (var context in profile.ControllerDefinition.Device.Context)
            {
                foreach (var action in context.Actions)
                {
                    if (!ProcessAction(inputDictionary, profile, action) && IncludeUnrecognised)
                    {
                        ProcessUnrecognised(bindingReport, profile, context, action);
                    }
                }
            }
        }

        public static void ProcessUnrecognised(BindingReport bindingReport, DetectedProfile profile, Context context,
            Models.Source.Action action)
        {
            var activeContext =
                bindingReport.UnrecognisedContexts.FirstOrDefault(c => c.ContextName == context.ContextName);
            if (activeContext == null)
            {
                activeContext = new UnrecognisedContext { ContextName = context.ContextName };
                bindingReport.UnrecognisedContexts.Add(activeContext);
            }

            var activeAction = activeContext.Actions.FirstOrDefault(a => a.ActionName == action.ActionName);
            if (activeAction == null)
            {
                activeAction = new UnrecognisedAction { ActionName = action.ActionName };
                activeContext.Actions.Add(activeAction);
            }

            if (action.Primary != null)
            {
                activeAction.Bindings.Add(new UnrecognisedBinding
                {
                    Controller = profile.ControllerDefinition.Device.DeviceName,
                    Profile = profile.ControllerDefinition.FriendlyName.Text,
                    Priority = Priority.Primary,
                    Keys = action.Primary.Keys.Select(k => k.Information).ToList(),
                });
            }
            if (action.Secondary != null)
            {
                activeAction.Bindings.Add(new UnrecognisedBinding
                {
                    Controller = profile.ControllerDefinition.Device.DeviceName,
                    Profile = profile.ControllerDefinition.FriendlyName.Text,
                    Priority = Priority.Secondary,
                    Keys = action.Secondary.Keys.Select(k => k.Information).ToList(),
                });
            }

        }

        public static bool ProcessAction(Dictionary<string, ActionInput> inputDictionary, DetectedProfile profile, Models.Source.Action action)
        {
            if (!inputDictionary.TryGetValue(action.ActionName, out var actionInput))
            {
                return false;
            }


            var inputKey = inputDictionary[action.ActionName];

            if (action.Primary != null)
            {
                actionInput.Bindings.Add(new InputBinding
                {
                    Controller = profile.ControllerDefinition.Device.DeviceName,
                    Profile = profile.ControllerDefinition.FriendlyName.Text,
                    Keys = action.Primary.Keys.Select(k => k.Information).ToList(),
                    Priority = Priority.Primary,
                });
            }
            if (action.Secondary != null)
            {
                actionInput.Bindings.Add(new InputBinding
                {
                    Controller = profile.ControllerDefinition.Device.DeviceName,
                    Profile = profile.ControllerDefinition.FriendlyName.Text,
                    Keys = action.Secondary.Keys.Select(k => k.Information).ToList(),
                    Priority = Priority.Secondary,
                });
            }

            return true;
        }
    }
}