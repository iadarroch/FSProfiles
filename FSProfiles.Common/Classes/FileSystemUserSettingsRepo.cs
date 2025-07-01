using System.Text.Json;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes;

public sealed class FileSystemUserSettingsRepo : IUserSettingsRepo
{
    private const string SettingsFileName = "userSettings.json";

    private readonly string _appName;
    private readonly string _settingsFolderPath;
    private readonly string _settingsFilePath;

    public FileSystemUserSettingsRepo(string appName)
    {
        _appName = appName;
        _settingsFolderPath = GetSettingsFolderPath();
        _settingsFilePath = GetSettingsFilePath();
    }

    public UserSettings GetSettings()
    {
        if (!File.Exists(_settingsFilePath))
        {
            SaveSettings(new UserSettings());
        }

        using var settingsFileStream = new FileStream(_settingsFilePath, FileMode.Open);
        var deserializedSettings = JsonSerializer.Deserialize<UserSettings>(settingsFileStream);

        if (deserializedSettings is null)
        {
            throw new InvalidOperationException("Can't deserialize user settings");
        }

        return deserializedSettings;
    }

    public void SaveSettings(UserSettings userSettings)
    {
        if (!Directory.Exists(_settingsFolderPath))
        {
            Directory.CreateDirectory(_settingsFolderPath);
        }

        using var settingsFileStream = new FileStream(_settingsFilePath, FileMode.OpenOrCreate);
        JsonSerializer.Serialize(settingsFileStream, userSettings);
        settingsFileStream.Flush();
        settingsFileStream.SetLength(settingsFileStream.Position);
    }

    private string GetSettingsFolderPath()
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(appDataFolder, _appName);
    }

    private string GetSettingsFilePath()
    {
        var settingsFolder = GetSettingsFolderPath();
        return Path.Combine(settingsFolder, SettingsFileName);
    }
}