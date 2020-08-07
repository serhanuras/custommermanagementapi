using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CustomerManagement.API.Filters
{
    public class SnakeCaseSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null) return;
            if (schema.Properties.Count == 0) return;

            var keys = schema.Properties.Keys;
            var newProperties = new Dictionary<string, OpenApiSchema>();
            foreach (var key in keys)
            {
                newProperties[ToSnakeCase(key)] = schema.Properties[key];
            }

            schema.Properties = newProperties;
        }
        
        private string ToSnakeCase(string str)
        {
            return string.Concat(str.Select((character, index) =>
                    index > 0 && char.IsUpper(character)
                        ? "_" + character
                        : character.ToString()))
                .ToLower();
        }
    }
    
}