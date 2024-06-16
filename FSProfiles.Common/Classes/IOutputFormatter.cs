using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes;

public interface IOutputFormatter
{
    void ConvertToHtml(BindingList bindingList, string fileName);
}