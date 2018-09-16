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
            SwaggerToWebApiClientGenerator codeGenerator = new SwaggerToWebApiClientGenerator();
            await codeGenerator.Start("http://localhost:5000/swagger/v1/swagger.json");
        }

    }
}
