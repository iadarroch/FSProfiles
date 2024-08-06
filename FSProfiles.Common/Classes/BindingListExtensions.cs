using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes
{
    public static class BindingListExtensions
    {

        public static Dictionary<string, ActionInput> ToInputDictionary(this BindingList bindingList)
        {
            var result = new Dictionary<string, ActionInput>();
            foreach (var section in bindingList.Sections)
            {
                foreach (var item in section.Items)
                {
                    switch (item)
                    {
                        case SubSection subSection:
                        {
                            foreach (var sectionAction in subSection.Actions)
                            {
                                result.AddActionInputs(sectionAction);
                            }
                            continue;
                        }
                        case SectionAction sectionAction:
                            result.AddActionInputs(sectionAction);
                            continue;
                    }
                }
            }

            return result;
        }

        private static void AddActionInputs(this Dictionary<string, ActionInput> inputDictionary,
            SectionAction sectionAction)
        {
            foreach (var input in sectionAction.Inputs)
            {
                if (inputDictionary.ContainsKey(input.InputKey))
                {
                    Console.WriteLine($"Duplicate {input.InputKey}");
                    continue;
                }
                inputDictionary.Add(input.InputKey, input);
            }
        }
    }
}
