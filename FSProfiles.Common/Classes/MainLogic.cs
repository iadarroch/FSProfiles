using System.Diagnostics;
using MSFS2020.Profiles.Common.Models;
using Action = MSFS2020.Profiles.Common.Models.Action;
using Binding = MSFS2020.Profiles.Common.Models.Binding;
using Context = MSFS2020.Profiles.Common.Models.Context;

namespace MSFS2020.Profiles.Common.Classes
{
    public enum ContentMode {All, Assigned, New}

    public class MainLogic
    {
        public enum Mode {Generate, Rebuild}

        private FolderProcessor _folderProcessor = new();
        private HtmlFormatter _htmlFormatter = new();

        public EventHandler<ProgressEvent>? OnStart;
        public EventHandler<ProgressEvent>? OnProgress;
        public EventHandler<ProgressEvent>? OnStop;

        public bool GetDefaultPath(out string basePath, out string? errorMessage)
        {
            var result = _folderProcessor.GetProfilePath(out basePath, out errorMessage);
            return result;
        }

        public string GetDefaultOutputFile()
        {
            var tempPath = System.IO.Path.GetTempPath();
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
                //bindingList.SerializeToFile("C:\\Temp\\Bindings.xml");
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
            while (contextIndex < bindingList.Count)
            {
                var context = bindingList[contextIndex];

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
                    bindingList.RemoveAt(contextIndex);
                    continue;
                }
                contextIndex++;  //if we have not removed a binding, move to the next one
            }
        }

        public BindingList BuildBindingList(List<DetectedProfile> profileList, Mode mode)
        {
            var bindingList = Serializer.DeserializeFromFile<BindingList>("KnownBindings.xml");
            bindingList.SelectedControllers = new List<SelectedController>();
            
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
                var bindingContext = bindingList.FirstOrDefault(c => c.ContextName == context.ContextName);
                if (bindingContext == null)
                {
                    bindingContext = new Context
                    {
                        ContextName = context.ContextName,
                        Actions = new List<Action>()
                    };
                    bindingList.Add(bindingContext);
                }

                ProcessContext(profile.ControllerDefinition.Device.DeviceName, context, bindingContext, mode);
            }
        }

        public void ProcessContext(string deviceName, Models.Source.Context context, Context bindingContext, Mode mode)
        {
            foreach (var action in context.Actions)
            {
                var bindingAction = bindingContext.Actions.FirstOrDefault(a => a.ActionName == action.ActionName);
                if (bindingAction == null)
                {
                    bindingAction = new Action
                    {
                        ActionName = action.ActionName,
                        Bindings = new List<Binding>()
                    };
                    bindingContext.Actions.Add(bindingAction);
                }

                if (mode == Mode.Generate)
                {
                    ProcessAction(deviceName, action, bindingAction);
                }
            }
        }

        public void ProcessAction(string deviceName, Models.Source.Action action, Action bindingAction)
        {

            if (action.Primary != null)
            {
                bindingAction.Bindings.Add(new Binding
                {
                    Controller = deviceName,
                    Keys = action.Primary.Keys.Select(k => k.Information).ToList(),
                    Priority = Priority.Primary,
                });
            }
            if (action.Secondary != null)
            {
                bindingAction.Bindings.Add(new Binding
                {
                    Controller = deviceName,
                    Keys = action.Secondary.Keys.Select(k => k.Information).ToList(),
                    Priority = Priority.Secondary,
                });
            }
        }
    }
}