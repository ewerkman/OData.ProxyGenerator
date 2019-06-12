using OData.ProxyGenerator.Templates;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OData.ProxyGenerator
{
    public class V4CodeGenerator
    {
        public string GenerateCode(string namespacePrefix, 
            string metadataDocumentUri, 
            bool useDataServiceCollection = true, 
            bool ignoreUnexpectedElementsAndAttributes = false, 
            bool enableNamingAlias = false)
        {
            ODataT4CodeGenerator t4CodeGenerator = new ODataT4CodeGenerator
            {
                NamespacePrefix = namespacePrefix,
                MetadataDocumentUri = metadataDocumentUri,
                UseDataServiceCollection = useDataServiceCollection,
                TargetLanguage = ODataT4CodeGenerator.LanguageOption.CSharp,
                IgnoreUnexpectedElementsAndAttributes = ignoreUnexpectedElementsAndAttributes,
                EnableNamingAlias = enableNamingAlias
            };

            return t4CodeGenerator.TransformText();
        }
    }
}
