using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.Tool
{
    internal class ApiGenerator
    {

        public async Task Generate(SwaggerJson json)
        {
            string template = await File.ReadAllTextAsync("templates\\ApiClientTemplate.txt");
            string code = template
                .Replace("{NAMESPACE}", GlobalConfiguration.NameSpace)
                .Replace("{INTERFACE}", "ISwaggerApi")
                .Replace("{METHODS}", GenerateApiMethods(json.Paths));

            await FileGenerator.CreateFile("codes\\", "ISwaggerApi.cs", code);
        }

        private string GenerateApiMethods(IEnumerable<ApiPath> apiPaths)
        {
            StringBuilder str = new StringBuilder();
            bool first = true;
            foreach (var path in apiPaths)
            {
                foreach (var detail in path.Details)
                {
                    str.Append((first ? "" : "\t\t") + "/// <summary>")
                     .AppendLine()
                     .AppendFormat("\t\t/// {0}", detail.Summary)
                     .AppendLine()
                     .Append("\t\t/// <summary>")
                     .AppendLine()
                     .AppendFormat("\t\t{0}", GenerateHttpMethodAttribute(detail.HttpMethod, path.Url))
                        .AppendLine();
                    str.AppendFormat("\t\tITask{0} {1}({2});",GenerateReturnValue(detail.Responses),GenerateOperation(detail.operationId), GenerateParameters(detail.Parameters,detail.Consumes));
                    str.AppendLine().AppendLine();
                    first = false;
                }
            }
            return str.ToString();
        }

        private string GenerateReturnValue(ApiResponse response)
        {
            if (response.Schema == null)
            {
                return $"<object>";
            }
         
            var returnType = SchemaConvert.Convert(response.Schema.Type, response.Schema.Items?.Format, response.Schema.Ref, response.Schema.Items);
            return $"<{returnType}>";
        }

        private string GenerateOperation(string operationId)
        {
            if (operationId.LastIndexOf("Get") > -1)
            {
                return operationId.Substring(0, operationId.Length - 3);
            }
            if (operationId.LastIndexOf("Post") > -1)
            {
                return operationId.Substring(0, operationId.Length - 4);
            }
            if (operationId.LastIndexOf("Put") > -1)
            {
                return operationId.Substring(0, operationId.Length - 3);
            }
            if (operationId.LastIndexOf("Delete") > -1)
            {
                return operationId.Substring(0, operationId.Length - 6);
            }
            return operationId;
        }

        private string GenerateParameters(IEnumerable<ApiParameter> parameters,string[] consumers)
        {
            StringBuilder str = new StringBuilder();
            int i = 0;
            string type = null;
            foreach (var param in parameters)
            {
                type = SchemaConvert.Convert(param.Type?? param.Schema?.Type, param.Format, param.Schema?.Ref, param.Schema?.Items);
                str.AppendFormat("{0}{1}{2} {3}", i == 0 ? "" : ",",GenerateParameterPrefix(param,consumers), type, param.Name);
                i++;
            }
            return str.ToString();
        }

        private string GenerateParameterPrefix(ApiParameter parameter, string[] consumers)
        {
            string parameterIn = parameter.In;
            string type = parameter.Type ?? parameter.Schema.Type;
            if (parameterIn == "body" || parameterIn == "formData")
            {
                return consumers.Any(x => x.Contains("json")) ? "[JsonContent]" : type == "object" ? "[FormContent]" : "[FormField]";
            }
            if (parameterIn == "header")
            {
                return "[Header(\"" + parameter.Name + "\")]";
            }
            return "[PathQuery]";
        }

        /// <summary>
        /// 构造Http方法头
        /// 例如：HttpGet("panzi123")
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GenerateHttpMethodAttribute(string method, string url)
        {
            string httpMethod = GetMethod(method);
            return $"[{httpMethod}(\"{url}\")]";
        }

        private string GetMethod(string method)
        {
            switch (method)
            {
                case "get":
                    return "HttpGet";
                case "post":
                    return "HttpPost";
                case "put":
                    return "HttpPut";
                case "delete":
                    return "HttpDelete";
            }
            throw new NotSupportedException($"method {method} not supported");
        }
    }
}
