using System.Diagnostics;
using FSControls.Models;
using Action = FSControls.Models.Action;
using Binding = FSControls.Models.Binding;
using Context = FSControls.Models.Context;

namespace FSControls.Classes
{
    public class MainLogic
    {
        public enum Mode {Generate, Rebuild}

        private FolderProcessor _folderProcessor = new FolderProcessor();
        private string? _basePath;

        private MainForm _mainForm;
        private HtmlFormatter _htmlFormatter;

        public MainLogic(MainForm mainForm)
        {
            _mainForm = mainForm;
            _htmlFormatter = new HtmlFormatter();
        }

        public string? BasePath {
            get { return _basePath; }
            set { 
                _basePath = value;
                _mainForm.txtBasePath.Text = _basePath;
            }
        }

        public bool GetDefaultPath()
        {
            var result = _folderProcessor.GetProfilePath(out var basePath);
            BasePath = basePath;
            return result;
        }

        public string GetDefaultOutputFile()
        {
            var tempPath = System.IO.Path.GetTempPath();
            return $"{tempPath}controllers.html";
        }

        public void SelectBasePath(TextBox txtBasePath, FolderBrowserDialog fbdBasePath)
        {
            fbdBasePath.Description = "Choose path to base of MSFS 2020 files";
            fbdBasePath.RootFolder = Environment.SpecialFolder.MyComputer;
            fbdBasePath.InitialDirectory = txtBasePath.Text;
            var dialogResult = fbdBasePath.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                txtBasePath.Text = fbdBasePath.SelectedPath;
            }
        }

        public void ProcessFolders(TextBox txtBasePath, CheckedListBox clbMappings)
        {
            var controllers = _folderProcessor.ProcessPath(txtBasePath.Text);
            var mappingList = controllers.Select(c => new DetectedController(c)).OrderBy(dc => dc.Name).ToList();

            clbMappings.Items.Clear();
            foreach (var mapping in mappingList)
            {
                clbMappings.Items.Add(mapping);
            }
        }

        public void GenerateMappingList(CheckedListBox clbMappings)
        {
            _mainForm.StartProgress("Rebuilding Known Bindings", clbMappings.CheckedItems.Count);
            try
            {
                var bindingList = BuildBindingList(clbMappings, Mode.Generate);
                bindingList.SerializeToFile("C:\\Temp\\Bindings.xml");
                _htmlFormatter.ConvertToHtml(bindingList, _mainForm.txtOutputFile.Text);
                Process.Start(@"cmd.exe ", @"/c " + _mainForm.txtOutputFile.Text);
            }
            finally
            {
                _mainForm.StopProgress("Completed");
            }
        }

        public void RebuildKnownBindings(CheckedListBox clbMappings)
        {
            _mainForm.StartProgress("Rebuilding Known Bindings", clbMappings.Items.Count);
            try
            {
                var bindingList = BuildBindingList(clbMappings, Mode.Rebuild);
                bindingList.SerializeToFile("C:\\Temp\\KnownBindings.xml");
            }
            finally
            {
                _mainForm.StopProgress("Completed");
            }
        }

        public BindingList BuildBindingList(CheckedListBox clbMappings, Mode mode)
        {
            var bindingList = Serializer.DeserializeFromFile<BindingList>("KnownBindings.xml");
            bindingList.SelectedControllers = new List<SelectedController>();
            
            var progress = 0;
            for (var index = 0; index < clbMappings.Items.Count; index++)
            {
                if (mode != Mode.Rebuild && !clbMappings.GetItemChecked(index)) continue;

                var controller = (DetectedController)clbMappings.Items[index];
                ProcessDetectedController(controller, bindingList, mode);
                _mainForm.UpdateProgress(progress: ++progress);
                Thread.Sleep(10);
            }

            return bindingList;
        }

        public void ProcessDetectedController(DetectedController controller, BindingList bindingList, Mode mode)
        {
            bindingList.SelectedControllers.Add(new SelectedController
            {
                DeviceName = controller.ControllerDefinition.Device.DeviceName,
                ProfileName = controller.ControllerDefinition.FriendlyName.Text
            });

            foreach (var context in controller.ControllerDefinition.Device.Context)
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

                ProcessContext(controller.ControllerDefinition.Device.DeviceName, context, bindingContext, mode);
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
                    Key = action.Primary.KEY.Information,
                    Priority = Priority.Primary,
                });
            }
            if (action.Secondary != null)
            {
                bindingAction.Bindings.Add(new Binding
                {
                    Controller = deviceName,
                    Key = action.Secondary.KEY.Information,
                    Priority = Priority.Secondary,
                });
            }

        }

    }
}
