namespace FSProfiles.Common.Classes;

public class FolderProcessorInstance
{
    public IFolderProcessor FolderProcessor { get; }
    public string? ErrorMessage { get; }
    public string Path { get; set; }
    public HostVersionType HostVersion => FolderProcessor.HostVersion;

    public FolderProcessorInstance(IFolderProcessor folderProcessor)
    {
        FolderProcessor = folderProcessor;
        Path = "";
        if (folderProcessor.GetBasePath(out var basePath, out var error))
        {
            if (folderProcessor.GetProfilePath(basePath, out var profilePath, out error))
            {
                Path = profilePath;
                return;
            }
        }

        ErrorMessage = error;
    }
}