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
            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.Start("http://localhost:5000/swagger/v1/swagger.json").Wait();
            SysConsole.WriteLine("finished...");
            SysConsole.Read();
        }
    }
}
