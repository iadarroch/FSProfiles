using FSProfiles.Common.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using FSProfiles.Common.Classes;
using CsvHelper;
using System.Globalization;
using System.Text;
using CsvHelper.Configuration;

namespace FSProfiles.Builder.Classes
{
    public class BindingBuilder
    {
        private readonly BuilderArguments _buildArgs;
        private readonly MainLogic _mainLogic;

        private string? _basePath;

        public BindingBuilder(BuilderArguments buildArgs)
        {
            _buildArgs = buildArgs;
            _mainLogic = new MainLogic(_buildArgs);
        }

        public bool PathFound()
        {
            if (!string.IsNullOrEmpty(_buildArgs.ProfilePath))
            {
                _basePath = _buildArgs.ProfilePath;
                return true;
            }

            var found = _mainLogic.GetProfilePath(out _basePath, out var errorMessage);
            if (!found)
            {
                Console.WriteLine($"Unable to determine Profiles path: {errorMessage}");
            }

            return found;
        }

        public void Build()
        {
            var names = LoadDefinitions(_basePath!);
            var menuItems = LoadMenuFile("..\\..\\..\\..\\FS2020 Options.csv");

            var menuCorrelations = ProcessMenuLines(names, menuItems);

            if (_buildArgs.Intermediate)
            {
                WriteToCsv(Path.Join(_buildArgs.OutputPath, "MenuBindings.csv"), menuCorrelations);
            }

            var menuBindings = CorrelationsToBindingList(menuCorrelations);
            menuBindings.SerializeToFile("..\\..\\..\\..\\FSProfiles\\KnownBindings.xml");
        }

        public static BindingList CorrelationsToBindingList(IEnumerable<Correlation> correlations)
        {
            var menuBindings = new BindingList();
            Section? menu = null;
            SubSection? subMenu = null;
            
            foreach (var correlation in correlations)
            {
                if (menu?.SectionName != correlation.Section)
                {
                    menu = new Section { SectionName = correlation.Section };
                    menuBindings.Sections.Add(menu);
                }

                var action = new SectionAction
                {
                    ActionName = correlation.Action,
                    Inputs = correlation.Inputs.Select(i => new ActionInput { InputKey = i }).ToList()
                };
                if (correlation.Inputs.Count > 1)
                {
                    Debug.WriteLine($"Action has multiple inputs: {correlation.Section} -> {correlation.SubSection} -> {correlation.Action}");
                }

                if (string.IsNullOrEmpty(correlation.SubSection))
                {
                    menu.Items.Add(action);
                    continue;
                }

                if (subMenu?.SubSectionName != correlation.SubSection)
                {
                    subMenu = new SubSection { SubSectionName = correlation.SubSection };
                    menu.Items.Add(subMenu);
                }
                subMenu.Actions.Add(action);
            }
            return menuBindings;
        }

        public static void WriteToCsv(string outputFile, IEnumerable<Correlation> correlations)
        {
            using var writer = new StreamWriter(outputFile);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            // Write header
            csv.WriteField("Section");
            csv.WriteField("SubSection");
            csv.WriteField("Action");
            csv.WriteField("InputKey");

            csv.NextRecord();

            // Write rows
            foreach (var correlation in correlations)
            {
                csv.WriteField(correlation.Section);
                csv.WriteField(correlation.SubSection);
                csv.WriteField(correlation.Action);
                foreach (var input in correlation.Inputs)
                {
                    csv.WriteField(input);
                }

                csv.NextRecord();
            }
        }

        public static List<Correlation> ProcessMenuLines(Dictionary<string, string> names, List<Correlation> menuItems)
        {
            foreach (var item in menuItems)
            {
                if (!string.IsNullOrEmpty(item.DedupeKey))
                {
                    item.Inputs.Add(item.DedupeKey);
                    continue;
                }

                var inputs = names.Where(kvp => kvp.Value == item.Action).ToList();

                if (inputs.Count == 0)
                {
                    item.Inputs.Add("No matching INPUT");
                    continue;
                }

                item.Inputs.AddRange(inputs.Select(i => i.Key));
            }

            return menuItems;
        }

        public static List<Correlation> LoadMenuFile(string menuPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null,
                Encoding = Encoding.UTF8
            };
            using var reader = new StreamReader(menuPath);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<Correlation>();
            return records.ToList();
        }

        public static Dictionary<string, string> LoadDefinitions(string basePath)
        {
            var result = new Dictionary<string, string>();
            const string inputPrefix = "INPUT.";
            var inputPrefixLength = inputPrefix.Length;

            var filePath = Path.Combine(basePath, "..\\..\\..\\LocalCache\\Packages\\Official\\OneStore\\fs-base\\en-US.locPak");
            var jsonData = File.ReadAllText(filePath);
            var definitions = JsonSerializer.Deserialize<JsonObject>(jsonData)!;
            Console.WriteLine($"{definitions["LocalisationPackage"]}");
            var package = definitions["LocalisationPackage"];
            var strings = package!["Strings"]!.AsObject();
            //Console.WriteLine($"{strings[0]}");

            foreach (var kvp in strings.AsEnumerable()
                         .Where(kvp => kvp.Key.StartsWith(inputPrefix) 
                                       && !kvp.Key.StartsWith("INPUT.CAT_")
                                       && !kvp.Key.StartsWith("INPUT.CONTEXT_")
                                       && !kvp.Key.EndsWith("_DESC")
                                       ))
            {
                var inputId = kvp.Key[inputPrefixLength..].Trim().ToUpper();
                var value = kvp.Value!.ToString().Trim().ToUpper();
                result.Add(inputId, value);
                Debug.WriteLine($"{inputId}\t{value}");
            }
            //var count = strings[0];
            return result;
        }
    }

}
