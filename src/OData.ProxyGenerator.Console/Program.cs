namespace OData.ProxyGenerator.Console
{
    using CommandLine;
    using OData.ProxyGenerator;
    using System;
    using System.IO;

    class Program
    {
        public class Options
        {
            [Option('n', "namespacePrefix", Required = false, HelpText = "The namespace of the client code generated. It replaces the original namespace in the metadata document, unless the model has several namespaces.")]
            public string NamespacePrefix { get; set; }

            [Option('m', "metadataDocumentUri", Required = true, HelpText = "The URI of the metadata document. The value must be set to a valid service document URI or a local file path " +
                                                            "eg : \"http://services.odata.org/V4/OData/OData.svc/\", \"File:///C:/Odata.edmx\", or @\"C:\\Odata.edmx\".  " +
                                                            "NOTE: If the OData service requires authentication for accessing the metadata document, the value of" +
                                                            "MetadataDocumentUri has to be set to a local file path, or the client code generation process will fail.")]
            public string MetadataDocumentUri { get; set; }

            [Option('o', "outputFile", Required = false, HelpText = "Filename of file to output to. If no outputfile is configured, the code will be output to the console.")]
            public string OutputFile { get; set; }

            [Option("overwrite", Required = false, Default = false, HelpText = "Overwrites the output file if it already exists.")]
            public bool Overwrite { get; set; }

            [Option('u', "useDataServiceCollection", Required = false, Default = true, HelpText = "The use of DataServiceCollection enables entity and property tracking. The value must be set to true or false.")]
            public bool UseDataServiceCollection { get; set; }

            [Option('i', "ignoreUnexpectedElementsAndAttributes", Required = false, Default = false, HelpText = "This flag indicates whether to ignore unexpected elements and attributes in the metadata document and generate the client code if any. The value must be set to true or false.")]
            public bool IgnoreUnexpectedElementsAndAttributes { get; set; }

            [Option('e', "enableNamingAlias ", Required = false, Default = false, HelpText = "This flag indicates whether to enable naming alias. The value must be set to true or false.")]
            public bool EnableNamingAlias { get; internal set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var codeGenerator = new V4CodeGenerator();
                    var generatedCode = codeGenerator.GenerateCode(o.NamespacePrefix,
                                 o.MetadataDocumentUri,
                                 o.UseDataServiceCollection,
                                 o.IgnoreUnexpectedElementsAndAttributes,
                                 o.EnableNamingAlias);

                    if (string.IsNullOrEmpty(o.OutputFile))
                    {
                        Console.Write(generatedCode);
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(o.OutputFile);

                        if(string.IsNullOrEmpty(fileInfo.Name) || fileInfo.Extension != "cs")
                        {
                            Console.Write($"'{o.OutputFile}' is not a valid filename.");
                        }
                        else if (fileInfo.Exists && !o.Overwrite)
                        {
                            Console.Write($"'{o.OutputFile}' already exists. Use --overwrite to overwrite the existing file.");
                        }
                        else
                        {
                            Console.Write($"Writing generated code to '{o.OutputFile}'");

                            WriteGeneratedCodeToFile(generatedCode, fileInfo);
                        }
                    }

                    Console.WriteLine("Finished!");
                });
        }

        private static void WriteGeneratedCodeToFile(string generatedCode, FileInfo toFileInfo)
        {
            Directory.CreateDirectory(toFileInfo.Directory.FullName);
            toFileInfo.Delete();

            using (StreamWriter writer = File.CreateText(toFileInfo.FullName))
            {
                writer.Write(generatedCode);
                writer.Flush();
            }
        }
    }
}