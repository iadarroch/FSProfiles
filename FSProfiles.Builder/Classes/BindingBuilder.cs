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
        private string _localePath;
        private string _menuPath;
        private string _outputPath;
        private string _intermediateName;

        private string? _basePath;

        public BindingBuilder(BuilderArguments buildArgs)
        {
            _buildArgs = buildArgs;
            IFolderProcessor processor;
            switch (_buildArgs.HostVersionType)
            {
                case HostVersionType.Native2020:
                    processor = new FolderProcessorNative2020();
                    break;
                case HostVersionType.Native2024:
                    processor = new FolderProcessorNative2024();
                    break;
                case HostVersionType.Steam2020:
                    processor = new FolderProcessorSteam2020();
                    break;
                case HostVersionType.Steam2024:
                    processor = new FolderProcessorSteam2024();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (_buildArgs.HostVersionType)
            {
                case HostVersionType.Native2020:
                case HostVersionType.Steam2020:
                    _localePath = @"..\..\..\LocalCache\Packages\Official\OneStore\fs-base\en-US.locPak";
                    _menuPath = @"..\..\..\..\FS2020 Options.csv";
                    _outputPath = @"..\..\..\..\FSProfiles\KnownBindings2020.xml";
                    _intermediateName = "MenuBindings2020.csv";
                    break;
                case HostVersionType.Native2024:
                case HostVersionType.Steam2024:
                    _localePath = @"C:\XboxGames\Microsoft Flight Simulator 2024\Content\Packages\fs-base\en-US.locPak";
                    _menuPath = @"..\..\..\..\FS2024 Options.csv";
                    _outputPath = @"..\..\..\..\FSProfiles\KnownBindings2024.xml";
                    _intermediateName = "MenuBindings2024.csv";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _mainLogic = new MainLogic(_buildArgs, processor);
        }

        public bool PathFound()
        {
            if (!string.IsNullOrEmpty(_buildArgs.ProfilePath))
            {
                _basePath = _buildArgs.ProfilePath;
                return true;
            }

            _mainLogic.SetDefaultLocations();
            _basePath = _mainLogic.HostVersions.First().Value.BasePath;
            return true;
        }

        public void Build()
        {
            Console.WriteLine("Starting");
            var names = LoadDefinitions(_basePath!);

            var menuItems = LoadMenuFile(_menuPath);

            var menuCorrelations = ProcessMenuLines(names, menuItems);

            if (_buildArgs.Intermediate)
            {
                WriteToCsv(Path.Join(_buildArgs.OutputPath, _intermediateName), menuCorrelations);
            }

            var menuBindings = CorrelationsToBindingList(menuCorrelations);
            menuBindings.SerializeToFile(_outputPath);
            Console.WriteLine("Finished");
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

        public static List<Correlation> ProcessMenuLines(Dictionary<string, string>? names, List<Correlation> menuItems)
        {
            foreach (var item in menuItems)
            {
                if (!string.IsNullOrEmpty(item.DedupeKey))
                {
                    item.Inputs.Add(item.DedupeKey);
                    continue;
                }

                if (names == null) continue;
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

        public Dictionary<string, string> LoadDefinitions(string basePath)
        {
            var result = new Dictionary<string, string>();
            const string inputPrefix = "INPUT.";
            var inputPrefixLength = inputPrefix.Length;

            var filePath = Path.IsPathRooted(_localePath) ? _localePath : Path.Combine(basePath, _localePath);
            var jsonData = File.ReadAllText(filePath);
            var definitions = JsonSerializer.Deserialize<JsonObject>(jsonData)!;
            var package = definitions["LocalisationPackage"];
            var strings = package!["Strings"]!.AsObject();

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
            Console.WriteLine($"Input names loaded: {result.Count}");
            return result;
        }
    }

}
