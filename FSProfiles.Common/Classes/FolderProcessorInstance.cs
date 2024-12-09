namespace FSProfiles.Common.Classes;

public class FolderProcessorInstance
{
    public IFolderProcessor FolderProcessor { get; }
    public string? ErrorMessage { get; private set; }
    public string Path { get; set; }
    public HostVersionType HostVersion => FolderProcessor.HostVersion;

    public FolderProcessorInstance(IFolderProcessor folderProcessor)
    {
        FolderProcessor = folderProcessor;
        Path = "";
        SetDefaultPath();
    }

    public void SetDefaultPath()
    {
        if (FolderProcessor.GetBasePath(out var basePath, out var error))
        {
            if (FolderProcessor.GetProfilePath(basePath, out var profilePath, out error))
            {
                Path = profilePath;
                return;
            }
        }

        ErrorMessage = error;
    }
}