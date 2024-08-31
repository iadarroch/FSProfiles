using FSProfiles.Common.Models;
using FSProfiles.Common.Models.Source;

namespace FSProfiles.Common.Classes;

public interface IFolderProcessor
{
    bool GetBasePath(out string path, out string? errorMessage);
    bool GetProfilePath(string basePath, out string path, out string? errorMessage);
    List<DetectedProfile> ProcessPath(string folderPath);
}


public abstract class FolderProcessorBase
{
    public DetectedProfile? ProcessFile(string fileName)
    {
        var fileContent = File.ReadAllLines(fileName).ToList();
        if (!fileContent[0].StartsWith("<?xml")) return null; //not a processable file 

        //Add new root object so contents are processable as a normal XML
        fileContent.Insert(1, @"<ControllerDefinition>");
        fileContent.Add(@"</ControllerDefinition>");

        using (StringReader reader = new StringReader(string.Join("", fileContent)))
        {
            var profile = Serializer.DeserializeObject<ControllerDefinition>(reader);
            if (profile == null) return null;

            var detectedProfile = new DetectedProfile(ProfilePath(fileName), profile);
            return detectedProfile;
        }
    }

    public abstract string ProfilePath(string filePath);
}