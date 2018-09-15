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
            var swaggerJson = new SwaggerJson
            {
                Info = json["info"].ToObject<Info>(),
                Swagger = json["swagger"].Value<string>(),
                Paths = ParsePaths(json["paths"]),
                Definitions = ParseDefinitions(json["definitions"])
            };
            return swaggerJson;
        }

        private static IEnumerable<ApiParameterDefinition> ParseDefinitions(JToken definitionToken)
        {
            var definitions = new List<ApiParameterDefinition>();
            foreach (JProperty definitionProperty in definitionToken)
            {
                ApiParameterDefinition definition = new ApiParameterDefinition
                {
                    Type = definitionProperty.Value["type"].ToObject<string>(),
                    Name = definitionProperty.Name
                };
                definition.Properties = ParseProperties(definitionProperty.Value["properties"]);
                definitions.Add(definition);
            }
            return definitions;
        }

        private static IEnumerable<ApiParameterDefinitionProperty> ParseProperties(JToken propertyToken)
        {
            var properties = new List<ApiParameterDefinitionProperty>();
            foreach (JProperty jProperty in propertyToken)
            {
                var prop = jProperty.Value.ToObject<ApiParameterDefinitionProperty>();
                if (prop == null)
                {
                    prop = new ApiParameterDefinitionProperty();
                    prop.Ref = jProperty.Value?.Value<string>("$ref")?.Replace("#/definitions/", "");
                    prop.Type = "object";
                }
                prop.Name = jProperty.Name;
                properties.Add(prop);
            }
            return properties;
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
                        pathDetail.Responses = ParseReponses(detailProperty.Value["responses"]);
                        details.Add(pathDetail);
                    }
                }
                path.Details = details;

                paths.Add(path);
            }
            return paths;
        }
        private static ApiResponse ParseReponses(JToken reponseToken)
        {
            var response = new ApiResponse();
            foreach (JProperty responseProperty in reponseToken)
            {
                response.StatusCode = responseProperty.Name;
                response.Description = responseProperty.Value["description"].ToString();

                if (responseProperty.Value["schema"] != null)
                {
                    ApiResponseSchema schema = responseProperty.Value["schema"].ToObject<ApiResponseSchema>();
                    if (schema == null)
                    {
                        schema = new ApiResponseSchema
                        {
                            Type = "object",
                            Ref = responseProperty.Value["schema"].Value<string>("$ref")?.Replace("#/definitions/", "")
                        };
                    }
                    response.Schema = schema;
                }
            }
            return response;
        }
    }
}
