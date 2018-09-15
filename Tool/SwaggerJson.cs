﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClient.Tool
{
    /// <summary>
    /// http://host/swagger/v1/swagger.json
    /// </summary>
    public class SwaggerJson
    {
        public Info Info { get; set; }
        public string Swagger { get; set; }
        public IEnumerable<ApiPath> Paths { get; set; }
        public IEnumerable<ApiParameterDefinition> Definitions { get; set; }
    }

    /// <summary>
    /// Info
    /// </summary>
    public class Info
    {
        public Contact contact { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string version { get; set; }
    }

    /// <summary>
    /// Contact
    /// </summary>
    public class Contact
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ApiPath
    {
        public string Url { get; set; }
        public IEnumerable<ApiPathDetail> Details { get; set; }
    }

    public class ApiPathDetail
    {
        public string HttpMethod { get; set; }
        public string Summary { get; set; }
        public string[] Produces { get; set; }
        public string[] Consumes { get; set; }
        public ApiResponse Responses { get; set; }
        public IEnumerable<ApiParameter> Parameters { get; set; }
    }

    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public string Description{ get; set; }
        public ApiResponseSchema Schema{ get; set; }
    }
    public class ApiResponseSchema
    {
        public string Type { get; set; }
        public string Ref { get; set; }
        public ApiResponseSchemaItem Items { get; set; }
    }
    public class ApiResponseSchemaItem
    {
        public string Type{ get; set; }
    }

    public class ApiParameter
    {
        public string Description { get; set; }
        public string In { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public ApiParameterSchema Schema { get; set; }
    }

    public class ApiParameterSchema
    {
        public string Type { get; set; }
    }

    #region definitions
    public class ApiParameterDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<ApiParameterDefinitionProperty> Properties { get; set; }
    }
    public class ApiParameterDefinitionProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Ref { get; set; }
    }
    #endregion
}
