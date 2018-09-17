using Newtonsoft.Json.Linq;
using System;
using SysConsole = System.Console;
using System.Threading.Tasks;

namespace WebApiClient.Tool.Console
{
    class Program
    {

        static string swaggerJsonUrl = "http://localhost:2121/swagger/v1/swagger.json";


        static void Main(string[] args)
        {
            GlobalConfiguration.NameSpace = "MyNameSpace";

            SwaggerToWebApiClientGenerator codeGenerator = new SwaggerToWebApiClientGenerator();
            codeGenerator.Start(swaggerJsonUrl).Wait();
            SysConsole.WriteLine("finished...");
            SysConsole.Read();
        }
    }
}
