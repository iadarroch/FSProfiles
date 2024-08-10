namespace FSProfiles.Builder.Classes
{
    public class Correlation
    {
        public string Section { get; set; } = "";

        public string SubSection { get; set; } = "";

        public string Action { get; set; } = "";

        public string DedupeKey { get; set; } = "";

        public List<string> Inputs { get; set; } = [];

    }
}
