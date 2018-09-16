using Newtonsoft.Json.Linq;
using System;
using SysConsole = System.Console;
using System.Threading.Tasks;

namespace WebApiClient.Tool.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //SwaggerToWebApiClientGenerator codeGenerator = new SwaggerToWebApiClientGenerator();
            //codeGenerator.Start("http://localhost:5000/swagger/v1/swagger.json").Wait();
            //SysConsole.WriteLine("finished...");

            Test.NameSpace.ISwaggerApi swaggerApi = HttpApiClient.Create<Test.NameSpace.ISwaggerApi >("http://localhost:5000");
            var result = swaggerApi.ApiValuesById(1).GetAwaiter().GetResult();
            SysConsole.WriteLine("获取到的结果：" + result.result + ".score:" + result.score);
            SysConsole.Read();
        }
    }
}
