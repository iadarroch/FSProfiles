using System.Diagnostics;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes
{
    public abstract class FolderProcessorNativeBase : FolderProcessorBase
    {
        private const string FlightSimPathNotFound = "Unable to automatically identify the main Flight Simulator folder. Please use the \"Select Profiles Path\" button to manually locate.";
        private const string ProfilesPathNotFound = "Unable to automatically identify parent folder of controller profiles. Please use the \"Select Profiles Path\" button to manually locate.";

        protected abstract string NativeAppPath { get; }

        /// <summary>
        /// Attempts to determine the path to the Base install folder
        /// This should be something like the following:
        /// C:\Users\core\AppData\Local\Packages\Microsoft.FlightSimulator_8wekyb3d8bbwe
        /// </summary>
        /// <param name="path"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public override bool GetBasePath(out string path, out string? errorMessage)
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Debug.WriteLine($"Local Application Data: {basePath}");

            //Add packages folder to path
            path = $"{basePath}\\Packages";

            //Now find Flight Sim in packages
            var flightSimFolder = Directory.GetDirectories(path, NativeAppPath)
                .FirstOrDefault();
            if (string.IsNullOrEmpty(flightSimFolder))
            {
                errorMessage = FlightSimPathNotFound;
                return false;
            }

            path = flightSimFolder;
            errorMessage = null;
            return true;
        }

        /// <summary>
        /// Attempts to determine the path to the parent folder containing controller profiles.
        /// This should be something like the following:
        /// C:\Users\core\AppData\Local\Packages\Microsoft.FlightSimulator_8wekyb3d8bbwe\SystemAppData\wgs\000901F72B889C47_00000000000000000000000069F80140
        /// </summary>
        /// <param name="path"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public override bool GetProfilePath(string basePath, out string path, out string? errorMessage)
        {
            //Add on the next path levels
            path = $"{basePath}\\SystemAppData\\wgs";
            if (!Path.Exists(path))
            {
                errorMessage = ProfilesPathNotFound;
                return false;
            }

            //Now find the controller profile parent folder
            var dataFolders = Directory.GetDirectories(path).Where(d => System.IO.Path.GetFileName(d) != "t").ToList();

            if (dataFolders.Count == 0 || dataFolders.Count > 1)
            {
                errorMessage = ProfilesPathNotFound;
                return false;
            }

            path = dataFolders[0];
            errorMessage = null;
            return true;
        }

        public override AggregatedResult<DetectedProfile, ProfileError> ProcessPath(string folderPath)
        {
            var result = new List<DetectedProfile>();
            var errors = new List<ProfileError>();
            try
            {
                var directories = Directory.GetDirectories(folderPath);
                foreach (var directory in directories)
                {
                    if (directory.Length < 3) continue;

                    var profileResult = FindProfilesInFolder(directory);
                    result.AddRange(profileResult.Values);
                    errors.AddRange(profileResult.Errors);
                }

            }
            catch (DirectoryNotFoundException)
            {
                //swallow exception and return no found profiles
            }

            return new AggregatedResult<DetectedProfile, ProfileError>(result, errors);
        }

        public AggregatedResult<DetectedProfile, ProfileError> FindProfilesInFolder(string folderPath)
        {
            var detectedProfiles = new List<DetectedProfile>();
            var profileErrors = new List<ProfileError>();
            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                if (Path.GetFileName(file).StartsWith("container")) continue;

                var detectedProfile = ProcessFile(file);
                switch (detectedProfile.Success)
                {
                    case false:
                        profileErrors.Add(detectedProfile.Error);
                        break;
                    case true when detectedProfile.Value != null:
                        detectedProfiles.Add(detectedProfile.Value);
                        break;
                }
            }
            return new AggregatedResult<DetectedProfile, ProfileError>(detectedProfiles, profileErrors);
        }

        public override string ProfilePath(string filePath)
        {
            var splitPath = filePath.Split("\\");
            var parts = splitPath.Length - 1;
            var profilePath = splitPath[parts - 1];
            return profilePath;
        }
    }

    public class FolderProcessorNative2020 : FolderProcessorNativeBase
    {
        protected override string NativeAppPath => "Microsoft.FlightSimulator_8wekyb3d8bbwe";
        public override HostVersionType HostVersion => HostVersionType.Native2020;
        public override string HostVersionName => "Native 2020";
    }

    public class FolderProcessorNative2024 : FolderProcessorNativeBase
    {
        protected override string NativeAppPath => "Microsoft.Limitless_8wekyb3d8bbwe";
        public override HostVersionType HostVersion => HostVersionType.Native2024;
        public override string HostVersionName => "Native 2024";
    }
}