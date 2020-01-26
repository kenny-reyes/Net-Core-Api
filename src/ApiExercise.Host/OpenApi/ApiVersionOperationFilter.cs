namespace ApiExercise.Host.OpenApi
{
/*    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiVersion = context.ApiDescription.GetApiVersion();

            if (apiVersion == null)
            {
                return;
            }

            var parameters = operation.Parameters;

            if (parameters == null)
            {
                operation.Parameters = parameters = new List<OpenApiParameter>();
            }

            var parameter = parameters.FirstOrDefault(p => p.Name == "api-version");
            if (parameter == null)
            {
                parameter = new OpenApiParameter()
                {
                    Name = "api-version",
                    Required = true,
                    In = ParameterLocation.Query
                };
                parameters.Add(parameter);
            }

            parameter.Description = "The requested API version";
        }
    }*/
}