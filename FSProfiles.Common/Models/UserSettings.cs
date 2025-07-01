using System.Text.Json.Serialization;
using FSProfiles.Common.Classes;

namespace FSProfiles.Common.Models;

public class UserSpecifiedPaths
{
    public Dictionary<HostVersionType, string> UserPath { get; set; } = new();

    [JsonIgnore]
    public string this[HostVersionType hostVersionType]
    {
        get => UserPath.ContainsKey(hostVersionType) ? UserPath[hostVersionType] : "";
        set => UserPath[hostVersionType] = value;
    }
}

public class UserSettings
{
    public UserSpecifiedPaths HostVersionPath { get; set; } = new();
}