using OData.ProxyGenerator;

namespace ODataConnectedServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var codeGenerator = new V4CodeGenerator();
            var code = codeGenerator.GenerateCode(null, "https://localhost:5000/api/$metadata");
        }      

    }
}
