using CommandLine;

namespace FSProfiles
{
    public class ProgramArguments
    {
        [Option('r', "rebuild", Required = false, HelpText = "Rebuild \"KnownBindings.xml\" from all profiles.")]
        public bool Rebuild { get; set; }

        [Option('d', "debug", Required = false, HelpText = "Output intermediate XML binding data. By default stored to \"C:\\Temp\\Bindings.xml\"")]
        public bool Debug { get; set; }
        [Option('p', "profiles", Required = false, HelpText = "Space delimited list of profile numbers to select by default. Numbers start at 0.")]
        public IList<int> ProfileSelection { get; set; } = [];
    }
}
