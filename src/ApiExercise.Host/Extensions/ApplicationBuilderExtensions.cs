namespace ApiExercise.Host.Extensions
{
    public static class ApplicationBuilderExtensions
    {
/*        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app) =>
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var apiDescriptionProvider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in apiDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                            $"Version {apiVersionDescription.ApiVersion}"
                        );

                        options.RoutePrefix = string.Empty;
                    }
                });*/
    }
}