namespace FSProfiles.Common.Classes;

public class FolderProcessorInstance
{
    public IFolderProcessor FolderProcessor { get; }
    public string? ErrorMessage { get; private set; }
    public string BasePath { get; private set; }
    public string ProfilePath { get; set; }
    public HostVersionType HostVersion => FolderProcessor.HostVersion;

    public FolderProcessorInstance(IFolderProcessor folderProcessor, IUserSettingsRepo userSettingsRepo)
    {
        FolderProcessor = folderProcessor;
        var userPath = userSettingsRepo.GetSettings().HostVersionPath[FolderProcessor.HostVersion];
        if (!string.IsNullOrEmpty(userPath))
        {
            ProfilePath = userPath;
            return;
        }

        BasePath = "";
        ProfilePath = "";
        SetDefaultPath();
    }

    public void SetDefaultPath()
    {
        if (string.IsNullOrEmpty(BasePath))
        {
            if (!FolderProcessor.GetBasePath(out var basePath, out var errorBase))
            {
                ErrorMessage = errorBase;
                return;
            }
            BasePath = basePath;
        }
        if (!FolderProcessor.GetProfilePath(BasePath, out var profilePath, out var errorProfile))
        {
            ErrorMessage = errorProfile;
            return;
        }
        ProfilePath = profilePath;
    }
}