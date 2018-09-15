using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClient.Tool
{
    /// <summary>
    /// 加载swagger.json
    /// </summary>
    public class SwaggerJsonParser
    {
        public static SwaggerJson Parse(JObject json)
        {
            var swaggerJson = new SwaggerJson();

            swaggerJson.Info = json["info"].ToObject<Info>();
            swaggerJson.Swagger = json["swagger"].Value<string>();
            swaggerJson.Paths = ParsePaths(json["paths"]);
            return swaggerJson;
        }

        private static IEnumerable<ApiPath> ParsePaths(JToken pathToken)
        {
            var paths = new List<ApiPath>();
            foreach (JProperty pathProperty in pathToken)
            {
                var path = new ApiPath();

                path.Url = pathProperty.Name;

                var details = new List<ApiPathDetail>();

                foreach (JObject detail in pathProperty)
                {
                    foreach (JProperty detailProperty in detail.Children())
                    {
                        ApiPathDetail pathDetail = detailProperty.Value.ToObject<ApiPathDetail>();
                        pathDetail.HttpMethod = detailProperty.Name;
                        details.Add(pathDetail);
                    }
                }
                path.Details = details;

                paths.Add(path);
            }
            return paths;
        }
    }
}
