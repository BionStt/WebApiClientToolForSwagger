using Newtonsoft.Json.Linq;
using System;
using SysConsole = System.Console;

namespace WebApiClient.Tool.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = HttpApiClient.Create<ISwaggerApi>();
            var result = client.GetApiJson("http://localhost:5000/swagger/v1/swagger.json").GetAwaiter().GetResult();
            var json = SwaggerJsonParser.Parse(result);
            SysConsole.Read();
        }
    }
}
