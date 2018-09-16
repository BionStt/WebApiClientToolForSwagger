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
                .Replace("{NAMESPACE}", GlobalConfiguration.NameSpace)
                .Replace("{ENTITYNAME}", definition.Name)
                .Replace("{DESCRIPTION}", definition.Description)
                .Replace("{PROPERTIES}", properties);

            await FileGenerator.CreateFile("codes\\", definition.Name + ".cs", code);
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
            return SchemaConvert.Convert(property.Type, property.Format, property.Ref, property.items);
        }
    }
}
