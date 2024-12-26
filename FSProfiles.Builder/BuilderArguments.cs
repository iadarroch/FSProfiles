using CommandLine;
using FSProfiles.Common.Classes;

namespace FSProfiles.Builder
{
    public class BuilderArguments : ProgramArguments
    {
        [Option('t', "type", Required = false, HelpText = "Install Type")]
        public HostVersionType HostVersionType { get; set; } = HostVersionType.Native2024;

        [Option('f', "fsPath", Required = false, HelpText = "Path to FS2020 profiles")]
        public string? ProfilePath { get; set; }

        [Option('o', "outputPath", Required = false, HelpText = "Output Path for built known bindings")]
        public string OutputPath { get; set; } = "C:\\Temp";

        [Option('i', "intermediate", Required = false, HelpText = "Output intermediate CSV file.")]
        public bool Intermediate { get; set; }
    }
}
