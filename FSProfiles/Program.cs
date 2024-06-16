using System.Windows.Forms.Design;
using CommandLine;

namespace FSProfiles
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Parser.Default.ParseArguments<ProgramArguments>(args)
                .WithParsed(programArguments =>
                {
                    var mainForm = new MainForm(programArguments);
                    Application.Run(mainForm);
                })
                .WithNotParsed(errors =>
                {
                    var msg = "Errors in command line options: \r\n";
                    foreach (var error in errors)
                    {
                        msg += $"{error}\r\n";
                    }

                    MessageBox.Show(msg, "FSProfiles", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
        }
    }
}