using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes;

public interface IUserSettingsRepo
{
    UserSettings GetSettings();

    void SaveSettings(UserSettings userSettings);
}