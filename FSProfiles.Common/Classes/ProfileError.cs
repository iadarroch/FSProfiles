namespace FSProfiles.Common.Classes;

public class ProfileError
{
    public string ProfileFile { get; }
    public string Error { get; }

    public ProfileError(string profileFile, string error)
    {
        ProfileFile = profileFile;
        Error = error;
    }
}