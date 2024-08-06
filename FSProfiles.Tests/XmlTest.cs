using FSProfiles.Common.Classes;
using FSProfiles.Common.Models;
using Xunit;

namespace FSProfiles.Tests
{
    public class XmlTest
    {
        [Fact]
        public void ReadXml()
        {
            using (var reader = new StreamReader(".\\Data\\Bindings.xml"))
            {
                var sut = Serializer.DeserializeObject<BindingReport>(reader);
                Assert.NotNull(sut);
            }
        }

        [Fact]
        public void ReadXmlFromFile()
        {
            var sut = Serializer.DeserializeFromFile<BindingReport>(".\\Data\\Bindings.xml");
            Assert.NotNull(sut);
        }
    }
}