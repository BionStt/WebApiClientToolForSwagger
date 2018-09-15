using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.Tool
{
    /// <summary>
    /// 
    /// </summary>
    internal class EntityGenerator
    {
        public async Task Generate(IEnumerable<ApiParameterDefinition> definitions)
        {
            foreach (var definition in definitions)
            {
                await Generate(definition);               
            }
        }

        private async Task Generate(ApiParameterDefinition definition)
        {
            string properties = GenerateProperties(definition);
            string template = await File.ReadAllTextAsync("templates\\EntityTemplate.txt");
            string code = template
                .Replace("{NAMESPACE}", "Test.NameSpace")
                .Replace("{ENTITYNAME}", definition.Name)
                .Replace("{DESCRIPTION}",definition.Description)
                .Replace("{PROPERTIES}", properties);

            using (FileStream stream = File.Create("templates\\" + definition.Name + ".cs"))
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes(code));
                
            }
        }

        private string GenerateProperties(ApiParameterDefinition definition)
        {
            StringBuilder str = new StringBuilder();

            foreach (var property in definition.Properties)
            {
                str.AppendLine()
                     .Append("\t\t/// <summary>")
                     .AppendLine()
                     .AppendFormat("\t\t/// {0}",property.Description)
                     .AppendLine()
                     .Append("\t\t/// <summary>")
                     .AppendLine()
                     .AppendFormat("\t\tpublic {0} {1} {{ get; set; }}", GetTypeName(property), property.Name);
            }
            return str.ToString();
        }

        private string GetTypeName(ApiParameterDefinitionProperty property)
        {
            switch (property.Type)
            {
                case "string":
                    if (!string.IsNullOrEmpty(property.Format))
                    {
                        switch (property.Format)
                        {
                            case "date-time":
                                return "DateTime?";
                            case "uuid":
                                return "Guid";
                        }
                    }
                    return "string";
                case "integer":
                    if (!string.IsNullOrEmpty(property.Format))
                    {
                        switch (property.Format)
                        {
                            case "int32":
                                return "int";
                            case "int64":
                                return "long";
                        }
                    }
                    return "int";
                case "number":
                    return property.Format;
                case "object":
                    return property.Ref;
            }
            return null;
        }
    }
}
