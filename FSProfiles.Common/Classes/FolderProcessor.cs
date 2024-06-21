using System.Diagnostics;
using FSProfiles.Common.Models;
using FSProfiles.Common.Models.Source;

namespace FSProfiles.Common.Classes
{
    public class FolderProcessor
    {
        /// <summary>
        /// Attempts to determine the path to the parent folder containing controller profiles.
        /// This should be something like the following:
        /// C:\Users\core\AppData\Local\Packages\Microsoft.FlightSimulator_8wekyb3d8bbwe\SystemAppData\wgs\000901F72B889C47_00000000000000000000000069F80140
        /// </summary>
        /// <param name="path"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool GetProfilePath(out string path, out string? errorMessage)
        {
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
                errorMessage = dialogTitleFlightSim;
                return false;
            }

            //Add on the next path levels
            path = $"{flightSimFolder}\\SystemAppData\\wgs";

            //Now find the controller profile parent folder
            var dataFolders = Directory.GetDirectories(path).Where(d => System.IO.Path.GetFileName(d) != "t").ToList();

            if (!dataFolders.Any() || dataFolders.Count > 1)
            {
                errorMessage = dialogTitleProfiles;
                return false;
            }

            path = dataFolders[0];
            errorMessage = null;
            return true;
        }

        public List<DetectedProfile> ProcessPath(string folderPath)
        {
            var result = new List<DetectedProfile>();

            var directories = Directory.GetDirectories(folderPath);
            foreach (var directory in directories)
            {
                if (directory.Length < 3) continue;

                var foundProfile = FindProfileInFolder(directory);
                if (foundProfile != null)
                {
                    result.Add(foundProfile);
                }
            }
            return result;
        }

        public DetectedProfile? FindProfileInFolder(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                if (file.StartsWith("container")) continue;

                var fileContent = File.ReadAllLines(file).ToList();
                if (!fileContent[0].StartsWith("<?xml")) continue; //not a processable file 

                //Add new root object so contents are processable as a normal XML
                fileContent.Insert(1, @"<ControllerDefinition>");
                fileContent.Add(@"</ControllerDefinition>");

                using (StringReader reader = new StringReader(string.Join("", fileContent)))
                {
                    var profile = Serializer.DeserializeObject<ControllerDefinition>(reader);
                    if (profile == null) continue;

                    var splitPath = file.Split("\\");
                    var parts = splitPath.Length - 1;
                    var profilePath = splitPath[parts - 1];

                    var dc = new DetectedProfile(profilePath, profile);
                    return dc;
                }
            }
            return null;
        }
    }
}