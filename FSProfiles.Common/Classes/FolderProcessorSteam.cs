using System.Diagnostics;
using System.IO;
using FSProfiles.Common.Models;
using FSProfiles.Common.Models.Source;

namespace FSProfiles.Common.Classes;

public class FolderProcessorSteam : FolderProcessorBase, IFolderProcessor
{
    private const string SteamBasePathError = "Unable to automatically identify the Steam base path. Please use the \"Select Profiles Path\" button to manually locate.";
    private const string SteamUserPathError = "Unable to automatically identify the Steam user id with FS2020 installed. Please use the \"Select Profiles Path\" button to manually locate.";

    private const string SteamAppPath = "1250410\\remote";
    private const string InputPrefix = "inputprofile_";

    /// <summary>
    /// Attempts to determine the path to the Base install folder
    /// This should be something like the following:
    /// C:\Program Files (x86)\Steam\userdata
    /// </summary>
    /// <param name="path"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public bool GetBasePath(out string path, out string? errorMessage)
    {
        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        Debug.WriteLine($"Local Application Data: {basePath}");

        //Add packages folder to path
        path = $"{basePath}\\Steam\\userdata";

        if (Path.Exists(path))
        {
            return IdentifyUserIdFolder(path, out path, out errorMessage);
        }

        errorMessage = SteamBasePathError;
        return false;
    }

    private bool IdentifyUserIdFolder(string basePath, out string userPath, out string? errorMessage)
    {

        var directories = Directory.GetDirectories(basePath);
        foreach (var directory in directories)
        {
            var testPath = Path.Combine(directory, SteamAppPath);
            if (Path.Exists(testPath))
            {
                userPath = directory;
                errorMessage = null;
                return true;
            }
        }
        userPath = basePath;
        errorMessage = SteamUserPathError;
        return false;
    }

    /// <summary>
    /// Attempts to determine the path to the folder containing controller profiles.
    /// This should be something like the following:
    /// C:\Program Files (x86)\Steam\userdata\1234567890\1250410\remote
    /// </summary>
    /// <param name="path"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public bool GetProfilePath(string basePath, out string path, out string? errorMessage)
    {
        var outPath = Path.Combine(basePath, SteamAppPath);
        if (Path.Exists(outPath))
        {
            path = outPath;
            errorMessage = null;
            return true;
        }
        path = basePath;
        errorMessage = SteamUserPathError;
        return IdentifyUserIdFolder(basePath, out path, out errorMessage);
    }

    public List<DetectedProfile> ProcessPath(string folderPath)
    {
        return FindProfilesInFolder(folderPath);
    }

    public List<DetectedProfile> FindProfilesInFolder(string folderPath)
    {

        var detectedProfiles = new List<DetectedProfile>();
        var files = Directory.GetFiles(folderPath);
        foreach (var file in files)
        {
            if (!Path.GetFileName(file).StartsWith(InputPrefix)) continue;

            var detectedProfile = ProcessFile(file);
            if (detectedProfile != null)
            {
                detectedProfiles.Add(detectedProfile);
            }
        }
        return detectedProfiles;
    }
    public override string ProfilePath(string filePath)
    {
        var profilePath = Path.GetFileName(filePath);
        return profilePath;
    }
}