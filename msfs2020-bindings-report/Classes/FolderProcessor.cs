using System.Diagnostics;
using FSControls.Models.Source;

namespace FSControls.Classes
{
    public class FolderProcessor
    {
        /// <summary>
        /// Attempts to determine the path to the parent folder containing controller profiles.
        /// This should be something like the following:
        /// C:\Users\core\AppData\Local\Packages\Microsoft.FlightSimulator_8wekyb3d8bbwe\SystemAppData\wgs\000901F72B889C47_00000000000000000000000069F80140
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool GetProfilePath(out string path)
        {
            const string dialogCaption = "Data Path Detection Failure";
            const string dialogTitleFlightSim = "Unable to automatically identify the main Flight Simulator folder. Please use \"Select Profiles Path\" button to manually locate.";
            const string dialogTitleProfiles = "Unable to automatically identify parent folder of controller profiles. Please use \"Select Profiles Path\" button to manually locate.";

            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Debug.WriteLine($"Local Application Data: {basePath}");

            //Add packages folder to path
            path = $"{basePath}\\Packages";

            //Now find Flight Sim in packages
            var flightSimFolder = Directory.GetDirectories(path, "Microsoft.FlightSimulator_*")
                .FirstOrDefault();
            if (string.IsNullOrEmpty(flightSimFolder))
            {
                MessageBox.Show(dialogTitleFlightSim, dialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            //Add on the next path levels
            path = $"{flightSimFolder}\\SystemAppData\\wgs";

            //Now find the controller profile parent folder
            var dataFolders = Directory.GetDirectories(path).Where(d => System.IO.Path.GetFileName(d) != "t").ToList();

            if (!dataFolders.Any() || dataFolders.Count > 1)
            {
                MessageBox.Show(dialogTitleProfiles, dialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            path = dataFolders[0];
            return true;
        }

        public List<ControllerDefinition> ProcessPath(string folderPath)
        {
            var result = new List<ControllerDefinition>();

            var directories = Directory.GetDirectories(folderPath);
            foreach (var directory in directories)
            {
                if (directory.Length < 3) continue;

                var foundDefinition = FindDefinitionInFolder(directory);
                if (foundDefinition != null)
                {
                    result.Add(foundDefinition);
                }
            }
            return result;
        }

        public ControllerDefinition? FindDefinitionInFolder(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                if (file.StartsWith("container")) continue;

                var fileContent = File.ReadAllLines(file).ToList();
                if (!fileContent[0].StartsWith("<?xml")) continue; //not a processable file 

                fileContent.Insert(1, @"<ControllerDefinition>");
                fileContent.Add(@"</ControllerDefinition>");
                using (StringReader reader = new StringReader(string.Join("", fileContent)))
                {
                    var result = Serializer.DeserializeObject<ControllerDefinition>(reader);
                    return result;
                }
            }
            return null;
        }
    }
}
