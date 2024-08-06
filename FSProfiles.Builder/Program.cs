using CommandLine;
using FSProfiles.Builder.Classes;

namespace FSProfiles.Builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<BuilderArguments>(args)
                .WithParsed(programArguments =>
                {
                    var builder = new BindingBuilder(programArguments);
                    if (builder.PathFound())
                    {
                        builder.Build();
                    }
                })
                .WithNotParsed(errors =>
                {
                    var msg = "Errors in command line options: \r\n";
                    foreach (var error in errors)
                    {
                        msg += $"{error}\r\n";
                    }

                    Console.WriteLine(msg);
                });
        }
    }
}
