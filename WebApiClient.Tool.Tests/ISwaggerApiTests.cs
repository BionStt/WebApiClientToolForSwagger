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
            var client = HttpApiClient.Create<ISwaggerApi>();
            var result = await client.GetApiJson("http://localhost:5000/swagger/v1/swagger.json");
            var json = SwaggerJsonParser.Parse(result);
        }
    }
}
