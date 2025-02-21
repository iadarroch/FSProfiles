using System.Xml.Xsl;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes
{
    public class XsltFormatter : IOutputFormatter
    {
        private const string TransformFile = "./Style/FSProfiles.xslt";
        private readonly Lazy<XslCompiledTransform> _transform;

        public XsltFormatter()
        {
            _transform = new Lazy<XslCompiledTransform>(() =>
            {
                XslCompiledTransform transform = new();

                //load the Xsl 
                transform.Load(TransformFile);
                return transform;
            });
        }

        public string OutputDescription => "HTML file";

        public string OutputExtension => "html";

        public void OutputToFile(BindingReport bindingReport, string fileName)
        {
            var document = bindingReport.SerializeToXmlDoc();

            using var writer = new StreamWriter(fileName);
            _transform.Value.Transform(document, null, writer);
        }
    }
}
