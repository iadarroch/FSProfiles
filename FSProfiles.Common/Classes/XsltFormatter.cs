using System.Xml.Xsl;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes
{
    public class XsltFormatter : IOutputFormatter
    {
        private const string TransformFile = "./Style/FSProfiles.xslt";
        private Lazy<XslCompiledTransform> _transform;

        public XsltFormatter()
        {
            _transform = new Lazy<XslCompiledTransform>(() =>
            {
                XslCompiledTransform transform = new XslCompiledTransform();

                //load the Xsl 
                transform.Load(TransformFile);
                return transform;
            });
        }

        public void ConvertToHtml(BindingList bindingList, string fileName)
        {
            var document = bindingList.SerializeToXmlDoc();

            using (var writer = new StreamWriter(fileName))
            {
                _transform.Value.Transform(document, null, writer);
            }
        }
    }
}
