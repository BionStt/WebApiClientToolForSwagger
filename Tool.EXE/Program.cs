using Newtonsoft.Json.Linq;
using System;
using SysConsole = System.Console;
using System.Threading.Tasks;

namespace WebApiClient.Tool.Console
{
    class Program
    {

        static string swaggerJsonUrl = "http://localhost:5000/swagger/v1/swagger.json";


        static void Main(string[] args)
        {
            SwaggerToWebApiClientGenerator codeGenerator = new SwaggerToWebApiClientGenerator();
            codeGenerator.Start(swaggerJsonUrl).Wait();
            SysConsole.WriteLine("finished...");
            SysConsole.Read();
        }
    }
}
