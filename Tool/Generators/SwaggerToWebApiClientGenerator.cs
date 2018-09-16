using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.Tool
{
    public class SwaggerToWebApiClientGenerator
    {
        private readonly EntityGenerator entityGenerator = new EntityGenerator();
        private readonly ApiGenerator apiClientGenerator = new ApiGenerator();

        public async Task Start(string swaggerJsonUrl)
        {
            var jobject = await GetSwaggerJson(swaggerJsonUrl);
            var json = SwaggerJsonParser.Parse(jobject);

            await GenerateEntity(json);
        }

        /// <summary>
        /// 获取Swagger文档的JSON
        /// </summary>
        /// <param name="swaggerJsonUrl"></param>
        /// <returns></returns>
        private async Task<JObject> GetSwaggerJson(string swaggerJsonUrl)
        {
            var client = HttpApiClient.Create<ISwaggerApi>();
            var result = await client.GetApiJson(swaggerJsonUrl);
            return result;
        }

        private async Task GenerateEntity(SwaggerJson json)
        {
            await entityGenerator.Generate(json.Definitions);
            await apiClientGenerator.Generate(json);
        }

    }
}
