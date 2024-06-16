using System.Diagnostics;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes
{
    public enum ContentMode {All, Assigned, New}

    public class MainLogic(ProgramArguments programArguments)
    {
        public enum Mode {Generate, Rebuild}

        private FolderProcessor _folderProcessor = new();
        private IOutputFormatter _htmlFormatter = new XsltFormatter();
        private ColorSequencer _colorSequencer = new ColorSequencer();

        public EventHandler<ProgressEvent>? OnStart;
        public EventHandler<ProgressEvent>? OnProgress;
        public EventHandler<ProgressEvent>? OnStop;

        public bool GetProfilePath(out string basePath, out string? errorMessage)
        {
            var result = _folderProcessor.GetProfilePath(out basePath, out errorMessage);
            return result;
        }

        public string GetDefaultOutputFile()
        {
            var tempPath = Path.GetTempPath();
            return $"{tempPath}controllers.html";
        }

        public List<DetectedProfile> ProcessFolders(string basePath)
        {
            var detectedProfiles = _folderProcessor.ProcessPath(basePath);
            var profileList = detectedProfiles.OrderBy(dc => dc.Name).ToList();

            return profileList;
        }

        public void GenerateBindingReport(string outputFile, List<DetectedProfile> profileList, ContentMode contentMode)
        {
            OnStart?.Invoke(this, new ProgressEvent("Generating Binding Report", profileList.Count));
            try
            {
                var bindingList = BuildBindingList(profileList, Mode.Generate);
                if (contentMode != ContentMode.All) FilterBindingList(bindingList, contentMode);
                //Populate test data with selected bindings
                if (programArguments.Debug)
                {
                    var defaultPath = "C:\\Development\\FSProfiles\\FSProfiles.Tests\\Data\\Bindings.xml";
                    if (!File.Exists(defaultPath))
                    {
                        defaultPath = $"{Path.GetTempPath()}Bindings.xml";
                    }
                    bindingList.SerializeToFile(defaultPath);
                }
                _htmlFormatter.ConvertToHtml(bindingList, outputFile);
                Process.Start(@"cmd.exe ", @"/c " + outputFile);
            }
            finally
            {
                OnStop?.Invoke(this, new ProgressEvent("Completed"));
            }
        }

        public void RebuildKnownBindings(string outputFile, List<DetectedProfile> profileList)
        {
            OnStart?.Invoke(this, new ProgressEvent("Rebuilding Known Bindings", profileList.Count));
            try
            {
                var bindingList = BuildBindingList(profileList, Mode.Rebuild);
                bindingList.SerializeToFile("C:\\Temp\\KnownBindings.xml");
            }
            finally
            {
                OnStop?.Invoke(this, new ProgressEvent("Completed"));
            }
        }

        public void FilterBindingList(BindingList bindingList, ContentMode contentMode)
        {
            var contextIndex = 0;
            while (contextIndex < bindingList.Contexts.Count)
            {
                var context = bindingList.Contexts[contextIndex];

                var actionIndex = 0;
                while (actionIndex < context.Actions.Count)
                {
                    var action = context.Actions[actionIndex];
                    if ((contentMode == ContentMode.Assigned && action.Bindings.Count == 0) ||
                        (contentMode == ContentMode.New && action.Bindings.Count != 0))
                    {
                        context.Actions.RemoveAt(actionIndex);
                        continue;
                    }
                    actionIndex++;
                }

                if ((contentMode == ContentMode.Assigned && context.Actions.Count == 0) ||
                    (contentMode == ContentMode.New && context.Actions.Count == 0))
                {
                    bindingList.Contexts.RemoveAt(contextIndex);
                    continue;
                }
                contextIndex++;  //if we have not removed a binding, move to the next one
            }
        }

        public BindingList BuildBindingList(List<DetectedProfile> profileList, Mode mode)
        {
            var bindingList = Serializer.DeserializeFromFile<BindingList>("KnownBindings.xml");
            bindingList.SelectedControllers = new List<SelectedController>();

            _colorSequencer.ResetDefaultColor(); //ensure color sequence always the same
            var progress = 0;
            foreach (var profile in profileList)
            {
                ProcessControllerProfile(profile, bindingList, mode);
                OnProgress?.Invoke(this, new ProgressEvent(progress: ++progress));
                Thread.Sleep(10);
            }

            return bindingList;
        }

        public void ProcessControllerProfile(DetectedProfile profile, BindingList bindingList, Mode mode)
        {
            bindingList.SelectedControllers.Add(new SelectedController
            {
                DeviceName = profile.ControllerDefinition.Device.DeviceName,
                ProfileName = profile.ControllerDefinition.FriendlyName.Text,
                ProfilePath = profile.Path
            });

            foreach (var context in profile.ControllerDefinition.Device.Context)
            {
                var bindingContext = bindingList.Contexts.FirstOrDefault(c => c.ContextName.TitleCase() == context.ContextName.TitleCase());
                if (bindingContext == null)
                {
                    bindingContext = new FSContext
                    {
                        ContextName = context.ContextName.TitleCase(),
                        BackColor = _colorSequencer.NextDefaultColor(),
                        Actions = new List<FSAction>()
                    };
                    bindingList.Contexts.Add(bindingContext);
                }

                ProcessContext(profile, context, bindingContext, mode);
            }
        }

        public void ProcessContext(DetectedProfile profile, Models.Source.Context context, FSContext bindingContext, Mode mode)
        {
            var colorOdd = bindingContext.BackColor.Value.Lighter(0.85f);
            var colorEven = bindingContext.BackColor.Value.Lighter(0.70f);
            var rowNum = 0;
            foreach (var action in context.Actions)
            {
                var bindingAction = bindingContext.Actions.FirstOrDefault(a => a.ActionName.TitleCase() == action.ActionName.TitleCase(true));
                if (bindingAction == null)
                {
                    bindingAction = new FSAction
                    {
                        ActionName = action.ActionName.TitleCase(true),
                        BackColor = (rowNum++ % 2 ==0) ? colorEven : colorOdd,
                        Bindings = new List<FSBinding>()
                    };
                    bindingContext.Actions.Add(bindingAction);
                }

                if (mode == Mode.Generate)
                {
                    ProcessAction(profile, action, bindingAction);
                }
            }
        }

        public void ProcessAction(DetectedProfile profile, Models.Source.Action action, FSAction bindingAction)
        {

            if (action.Primary != null)
            {
                bindingAction.Bindings.Add(new FSBinding
                {
                    Controller = profile.ControllerDefinition.Device.DeviceName,
                    Profile = profile.ControllerDefinition.FriendlyName.Text,
                    Keys = action.Primary.Keys.Select(k => k.Information).ToList(),
                    Priority = Priority.Primary,
                });
            }
            if (action.Secondary != null)
            {
                bindingAction.Bindings.Add(new FSBinding
                {
                    Controller = profile.ControllerDefinition.Device.DeviceName,
                    Profile = profile.ControllerDefinition.FriendlyName.Text,
                    Keys = action.Secondary.Keys.Select(k => k.Information).ToList(),
                    Priority = Priority.Secondary,
                });
            }
        }
    }
}