using System.Globalization;
using CsvHelper;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes
{
    public class CsvFormatter : IOutputFormatter
    {
        public CsvFormatter()
        {
        }

        public string OutputDescription => "CSV file";

        public string OutputExtension => "csv";

        public void OutputToFile(BindingReport bindingReport, string fileName)
        {
            using var writer = new StreamWriter(fileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            WriteHeaders(csv, bindingReport);
            WriteBindings(csv, bindingReport);
            if (bindingReport.UnrecognisedContexts.Count > 0)
                WriteUnrecognisedBindings(csv, bindingReport);
        }

        public void WriteHeaders(CsvWriter csv, BindingReport bindingReport)
        {
            WriteHeaderLine(csv, bindingReport, c => c.HostVersionName);
            WriteHeaderLine(csv, bindingReport, c => c.DeviceName);
            WriteHeaderLine(csv, bindingReport, c => c.ProfileType);
            WriteHeaderLine(csv, bindingReport, c => c.ProfileName);

            // Write header line 2
            csv.WriteField("Section");
            csv.WriteField("SubSection");
            csv.WriteField("Action");
            csv.WriteField("InputKey");

            foreach (var controller in bindingReport.SelectedControllers)
            {
                csv.WriteField("Priority");
                csv.WriteField("Key Combo");
            }
            csv.NextRecord();
        }

        public void WriteHeaderLine(CsvWriter csv, BindingReport bindingReport, Func<SelectedController, string> controllerData)
        {
            // Write header line
            csv.WriteField("");
            csv.WriteField("");
            csv.WriteField("");
            csv.WriteField("");

            foreach (var controller in bindingReport.SelectedControllers)
            {
                csv.WriteField(controllerData(controller));
                csv.WriteField("");
            }
            csv.NextRecord();
        }

        public void WriteBindings(CsvWriter csv, BindingReport bindingReport)
        {
            // Write rows
            foreach (var section in bindingReport.BindingList.Sections)
            {
                foreach (var item in section.Items)
                {
                    if (item is SubSection subSection)
                    {
                        foreach (var subAction in subSection.Actions)
                        {
                            WriteAction(csv, bindingReport, section.SectionName, subSection.SubSectionName, subAction);
                        }


                    }
                    if (item is SectionAction action)
                    {
                        WriteAction(csv, bindingReport, section.SectionName, "", action);
                    }
                }
            }
        }

        public void WriteAction(CsvWriter csv, BindingReport bindingReport, string sectionName, string subsectionName, SectionAction action)
        {
            foreach (var input in action.Inputs)
            {
                foreach (var binding in input.Bindings)
                {
                    csv.WriteField(sectionName);
                    csv.WriteField(subsectionName);
                    csv.WriteField(action.ActionName);
                    csv.WriteField(input.InputKey);
                    foreach (var controller in bindingReport.SelectedControllers)
                    {
                        if (controller.DeviceName == binding.Controller && controller.ProfileName == binding.Profile)
                        {
                            csv.WriteField(binding.Priority);
                            csv.WriteField(binding.KeyCombo);
                        }
                        else
                        {
                            csv.WriteField("");
                            csv.WriteField("");
                        }
                    }
                    csv.NextRecord();
                }
            }
        }

        public void WriteUnrecognisedBindings(CsvWriter csv, BindingReport bindingReport)
        {
            // Write rows
            foreach (var context in bindingReport.UnrecognisedContexts)
            {
                foreach (var action in context.Actions)
                { 
                    WriteUnrecognisedAction(csv, bindingReport, context.ContextName, action);
                }
            }
        }

        public void WriteUnrecognisedAction(CsvWriter csv, BindingReport bindingReport, string contextName, UnrecognisedAction action)
        {
            foreach (var binding in action.Bindings)
            {
                csv.WriteField("UNRECOGNISED");
                csv.WriteField(contextName);
                csv.WriteField("");
                csv.WriteField(action.ActionName);
                foreach (var controller in bindingReport.SelectedControllers)
                {
                    if (controller.DeviceName == binding.Controller && controller.ProfileName == binding.Profile)
                    {
                        csv.WriteField(binding.Priority);
                        csv.WriteField(binding.KeyCombo);
                    }
                    else
                    {
                        csv.WriteField("");
                        csv.WriteField("");
                    }
                }
                csv.NextRecord();
            }
        }
    }
}
