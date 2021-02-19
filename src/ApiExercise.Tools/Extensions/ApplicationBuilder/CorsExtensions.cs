using Microsoft.AspNetCore.Builder;

namespace ApiExercise.Tools.Extensions.ApplicationBuilder
{
    public static class CorsExtensions
    {
        public static IApplicationBuilder AllowAllCorsExtension(this IApplicationBuilder app)
        {
            return app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
    }
}