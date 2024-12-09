using FSProfiles.Common.Models;
using FSProfiles.Common.Models.Source;

namespace FSProfiles.Common.Classes;

public interface IFolderProcessor
{
    HostVersionType HostVersion { get; }
    string HostVersionName { get; }
    bool GetBasePath(out string path, out string? errorMessage);
    bool GetProfilePath(string basePath, out string path, out string? errorMessage);
    List<DetectedProfile> ProcessPath(string folderPath);
}

public abstract class FolderProcessorBase : IFolderProcessor
{
    static bool IsXmlFile(string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName))
          return false;
      try
      {
          var line = new byte[6];
          using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
              fs.Read(line, 0, 6);
          return System.Text.Encoding.Default.GetString(line).StartsWith("<?xml");
      }
      catch (Exception)
      {
          return false; //if any errors (permissions, etc) reading file, assume not a profile
      }
    }

    public DetectedProfile? ProcessFile(string fileName)
    {
        if (!IsXmlFile(fileName))
            return null; //not a processable file

        var fileContent = File.ReadAllLines(fileName).ToList();
        //Add new root object so contents are processable as a normal XML
        fileContent.Insert(1, @"<ControllerDefinition>");
        fileContent.Add(@"</ControllerDefinition>");

        using (StringReader reader = new(string.Join("", fileContent)))
        {
            var profile = Serializer.DeserializeObject<ControllerDefinition>(reader);
            if (profile == null) return null;

            var detectedProfile = new DetectedProfile(HostVersionName, ProfilePath(fileName), profile);
            return detectedProfile;
        }
    }

    public abstract string HostVersionName { get; }

    public abstract string ProfilePath(string filePath);

    public abstract HostVersionType HostVersion { get; }

    public abstract bool GetBasePath(out string path, out string? errorMessage);

    public abstract bool GetProfilePath(string basePath, out string path, out string? errorMessage);

    public abstract List<DetectedProfile> ProcessPath(string folderPath);
}