namespace ApiExercise.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
/*        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.CustomSchemaIds(schemaSelector => schemaSelector.FullName);
                options.OperationFilter<ApiVersionOperationFilter>();

                var apiDescriptionProvider =
                    services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                
                foreach (var apiVersionDescription in apiDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        apiVersionDescription.GroupName,
                        new OpenApiInfo
                        {
                            Contact = new OpenApiContact {Email = "keniakos@hotmail.com"},
                            Version = apiVersionDescription.ApiVersion.ToString(),
                            Title = $"Swagger {apiVersionDescription.ApiVersion}",
                            TermsOfService = new Uri("www.termsServiceUrl"),
                            Description = apiVersionDescription.IsDeprecated
                                ? "This API version is deprecated"
                                : String.Empty
                        });
                }
            });
        }*/
    }
}