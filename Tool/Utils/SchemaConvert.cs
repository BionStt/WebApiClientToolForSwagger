using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiClient.Tool
{
    internal class SchemaConvert
    {
        public static string Convert(string type, string format, string reference, ApiSchemaItem item)
        {

            switch (type)
            {
                case "string":
                    if (!string.IsNullOrEmpty(format))
                    {
                        switch (format)
                        {
                            case "date-time":
                                return "DateTime?";
                            case "uuid":
                                return "Guid";
                        }
                    }
                    return "string";
                case "integer":
                    if (!string.IsNullOrEmpty(format))
                    {
                        switch (format)
                        {
                            case "int32":
                                return "int";
                            case "int64":
                                return "long";
                        }
                    }
                    return "int";
                case "number":
                    return format;
                case "boolean":
                    return "bool";
                case "object":
                    return reference;
                case "array":
                    return Convert(item?.Type??"object", item?.Format??"", reference, null) + "[]";
            }
            return null;

        }
    }
}
