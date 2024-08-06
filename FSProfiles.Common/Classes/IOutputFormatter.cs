using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes;

public interface IOutputFormatter
{
    void ConvertToHtml(BindingReport bindingReport, string fileName);
}