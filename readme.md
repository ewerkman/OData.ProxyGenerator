# OData.ProxyGenerator

When you want to generate code to consume OData services in your a application, you can use a Visual Studio Extension called [OData Connected Service](https://marketplace.visualstudio.com/items?itemName=laylaliu.ODataConnectedService). This extension generates a C# (or Visual Basic) proxy for you that you can use to call an OData service.

The source code for this tool can be found here: [https://github.com/OData/lab/tree/master/ODataConnectedService](https://github.com/OData/lab/tree/master/ODataConnectedService). This tool uses a T4 template to generate the code for the proxy. 

Unfortunately, this tool is not so useful when you want to automate the process of generating the proxy code. 

Based on the source code of the extension, I created a simple class library and a sample console application that use the same T4 template as used in the tool to generate the code. 

> Note: This tool was not extensively tested. 

## How to use the console application

Usage information is provided by typing `OData.ProxyGenerator.Console --help`

If you are using this tool (as I am) to generate the proxy code to connect to a Sitecore Commerce Engine, you can use the following command lines to generate the ServiceProxy code:

`OData.ProxyGenerator.Console -m "https://localhost:5000/api/%24metadata" -o c:\temp\CommerceShops.cs --overwrite`
`OData.ProxyGenerator.Console -m "https://localhost:5000/api/%24metadata" -n "CommerceOps" -o c:\temp\CommerceOps.cs --overwrite`