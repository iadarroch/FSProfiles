using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes;

public interface IOutputFormatter
{
    string OutputDescription { get; }
    string OutputExtension { get; }
    void OutputToFile(BindingReport bindingReport, string fileName);
}