using System.Diagnostics;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes;

public abstract class FolderProcessorSteamBase : FolderProcessorBase
{
    private const string SteamBasePathError = "Unable to automatically identify the Steam base path. Please use the \"Select Profiles Path\" button to manually locate.";
    private const string SteamUserPathError = "Unable to automatically identify the Steam user id with FS2020 installed. Please use the \"Select Profiles Path\" button to manually locate.";

    protected abstract string SteamAppPath { get; }
    private const string InputPrefix = "inputprofile_";

    /// <summary>
    /// Attempts to determine the path to the Base install folder
    /// This should be something like the following:
    /// C:\Program Files (x86)\Steam\userdata
    /// </summary>
    /// <param name="path"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public override bool GetBasePath(out string path, out string? errorMessage)
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
    /// <param name="basePath"></param>
    /// <param name="path"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public override bool GetProfilePath(string basePath, out string path, out string? errorMessage)
    {
        var outPath = Path.Combine(basePath, SteamAppPath);
        if (Path.Exists(outPath))
        {
            path = outPath;
            errorMessage = null;
            return true;
        }
        return IdentifyUserIdFolder(basePath, out path, out errorMessage);
    }

    public override List<DetectedProfile> ProcessPath(string folderPath)
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

public class FolderProcessorSteam2020 : FolderProcessorSteamBase
{
    protected override string SteamAppPath => "1250410\\remote";
    public override HostVersionType HostVersion => HostVersionType.Steam2020;
    public override string HostVersionName => "Steam 2020";
}

public class FolderProcessorSteam2024 : FolderProcessorSteamBase
{
    protected override string SteamAppPath => "2537590\\remote";
    public override HostVersionType HostVersion => HostVersionType.Steam2024;
    public override string HostVersionName => "Steam 2024";
}