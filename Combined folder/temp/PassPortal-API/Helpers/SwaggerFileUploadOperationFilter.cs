using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerFileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody != null && operation.RequestBody.Content.ContainsKey("multipart/form-data"))
        {
            foreach (var schema in operation.RequestBody.Content["multipart/form-data"].Schema.Properties)
            {
                if (schema.Value.Type == "string" && schema.Value.Format == "binary")
                {
                    schema.Value.Type = "file";
                    schema.Value.Format = null;
                }
            }
        }
    }
}
