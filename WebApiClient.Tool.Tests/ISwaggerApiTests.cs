using System;
using System.Threading.Tasks;
using Xunit;

namespace WebApiClient.Tool.Tests
{
    public class ISwaggerApiTests
    {
        [Fact]
        public async Task GetSwaggerJsonTest()
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            await codeGenerator.Start("http://localhost:5000/swagger/v1/swagger.json");
        }

    }
}
