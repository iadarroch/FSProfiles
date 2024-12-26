namespace FSProfiles.Common.Classes;

public class FolderProcessorInstance
{
    public IFolderProcessor FolderProcessor { get; }
    public string? ErrorMessage { get; private set; }
    public string BasePath { get; private set; }
    public string ProfilePath { get; set; }
    public HostVersionType HostVersion => FolderProcessor.HostVersion;

    public FolderProcessorInstance(IFolderProcessor folderProcessor)
    {
        FolderProcessor = folderProcessor;
        BasePath = "";
        ProfilePath = "";
        SetDefaultPath();
    }

    public void SetDefaultPath()
    {
        if (FolderProcessor.GetBasePath(out var basePath, out var error))
        {
            BasePath = basePath;
            if (FolderProcessor.GetProfilePath(basePath, out var profilePath, out error))
            {
                ProfilePath = profilePath;
                return;
            }
        }

        ErrorMessage = error;
    }
}