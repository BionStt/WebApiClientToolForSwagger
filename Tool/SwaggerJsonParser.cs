using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClient.Tool
{
    /// <summary>
    /// 加载swagger.json
    /// </summary>
    internal class SwaggerJsonParser
    {
        private static readonly string KEY_INFO = "info";
        private static readonly string KEY_SWAGGER = "swagger";
        private static readonly string KEY_PATHS = "paths";
        private static readonly string KEY_DEFINITIONS = "definitions";

        private static readonly string KEY_PROPERTIES = "properties";
        private static readonly string KEY_RESPONSES = "responses";
        private static readonly string KEY_DESCRIPTION = "description";
        private static readonly string KEY_PARAMETERS = "parameters";

        private static readonly string KEY_TYPE = "type";
        private static readonly string KEY_OBJECT = "object";
        private static readonly string KEY_SCHEMA = "schema";


        public static SwaggerJson Parse(JObject json)
        {
            var swaggerJson = new SwaggerJson
            {
                Info = json[KEY_INFO].ToObject<Info>(),
                Swagger = json[KEY_SWAGGER].Value<string>(),
                Paths = ParsePaths(json[KEY_PATHS]),
                Definitions = ParseDefinitions(json[KEY_DEFINITIONS])
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
                    Description = definitionProperty.Value[KEY_DESCRIPTION]?.ToObject<string>(),
                    Type = definitionProperty.Value[KEY_TYPE].ToObject<string>(),
                    Name = definitionProperty.Name
                };
                definition.Properties = ParseProperties(definitionProperty.Value[KEY_PROPERTIES]);
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
                    prop.Ref = ParseSchemaRef(jProperty.Value);
                    prop.Type = KEY_OBJECT;
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
                        pathDetail.Responses = ParseReponses(detailProperty.Value[KEY_RESPONSES]);
                        pathDetail.Parameters = ParseParameters(detailProperty.Value[KEY_PARAMETERS]);
                        details.Add(pathDetail);
                    }
                }
                path.Details = details;

                paths.Add(path);
            }
            return paths;
        }

        private static IEnumerable<ApiParameter> ParseParameters(JToken parameterToken)
        {
            var parameters = new List<ApiParameter>();
            foreach (JObject jObject in parameterToken)
            {
                var param = jObject.ToObject<ApiParameter>();
                if (jObject[KEY_SCHEMA] != null) {
                    ApiParameterSchema schema = jObject[KEY_SCHEMA].ToObject<ApiParameterSchema>();
                    //$ref
                    if (schema == null)
                    {
                        schema = new ApiParameterSchema
                        {
                            Type= KEY_OBJECT,
                            Ref= ParseSchemaRef(jObject)
                        };
                    }
                    param.Schema = schema;
                }
                parameters.Add(param);
            }
            return parameters;
        }
        private static ApiResponse ParseReponses(JToken reponseToken)
        {
            var response = new ApiResponse();
            foreach (JProperty responseProperty in reponseToken)
            {
                response.StatusCode = responseProperty.Name;
                response.Description = responseProperty.Value[KEY_DESCRIPTION].ToString();

                if (responseProperty.Value[KEY_SCHEMA] != null)
                {
                    ApiResponseSchema schema = responseProperty.Value[KEY_SCHEMA].ToObject<ApiResponseSchema>();
                    if (schema == null)
                    {
                        schema = new ApiResponseSchema
                        {
                            Type = KEY_OBJECT,
                            Ref = ParseSchemaRef(responseProperty)
                        };
                    }
                    response.Schema = schema;
                }
            }
            return response;
        }

        private static string ParseSchemaRef(JProperty schemaProperty)
        {
            return ParseSchemaRef(schemaProperty.Value[KEY_SCHEMA]);
        }

        private static string ParseSchemaRef(JObject schemaObject)
        {
            return ParseSchemaRef(schemaObject[KEY_SCHEMA]);
        }

        private static string ParseSchemaRef(JToken schemaToken)
        {
            return schemaToken?.Value<string>("$ref")?.Replace("#/definitions/", "");
        }
    }
}