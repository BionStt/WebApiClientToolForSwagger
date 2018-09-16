using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClient.Tool
{
    /// <summary>
    /// http://host/swagger/v1/swagger.json
    /// </summary>
    internal class SwaggerJson
    {
        public Info Info { get; set; }
        public string Swagger { get; set; }
        public IEnumerable<ApiPath> Paths { get; set; }
        public IEnumerable<ApiParameterDefinition> Definitions { get; set; }
    }

    /// <summary>
    /// Info
    /// </summary>
    internal class Info
    {
        public Contact contact { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string version { get; set; }
    }

    /// <summary>
    /// Contact
    /// </summary>
    internal class Contact
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    internal class ApiPath
    {
        public string Url { get; set; }
        public IEnumerable<ApiPathDetail> Details { get; set; }
    }

    internal class ApiPathDetail
    {
        public string HttpMethod { get; set; }
        public string Summary { get; set; }
        public string[] Produces { get; set; }
        public string[] Consumes { get; set; }
        public ApiResponse Responses { get; set; }
        public IEnumerable<ApiParameter> Parameters { get; set; }
    }

    internal class ApiResponse
    {
        public string StatusCode { get; set; }
        public string Description{ get; set; }
        public ApiResponseSchema Schema{ get; set; }
    }
    internal class ApiResponseSchema
    {
        public string Type { get; set; }
        public string Ref { get; set; }
        public ApiResponseSchemaItem Items { get; set; }
    }
    internal class ApiResponseSchemaItem
    {
        public string Type{ get; set; }
    }

    internal class ApiParameter : ApiBase
    {
        public string In { get; set; }
        public bool Required { get; set; }
        public string Format { get; set; }
        public ApiParameterSchema Schema { get; set; }
    }

    internal class ApiParameterSchema
    {
        public string Ref { get; set; }
        public string Type { get; set; }
    }

    #region definitions
    internal class ApiParameterDefinition : ApiBase
    {
        public IEnumerable<ApiParameterDefinitionProperty> Properties { get; set; }
    }
    internal class ApiParameterDefinitionProperty : ApiBase
    {
        public string Format { get; set; }
        public string Ref { get; set; }
        public ApiResponseSchemaItem items { get; set; }
    }

    internal class ApiBase
    {
        public string Name { get; set; }
        public string Type { get; set; }

        private string _description;
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_description))
                {
                    return Name;
                }
                return _description;
            }
            set
            {
                _description = value;
            }
        }
    }
    #endregion
}
