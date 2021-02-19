using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace NetCoreApiScaffolding.Tools.Extensions.ServiceCollection
{
	public static class B2CExtensions
    {
        public static IServiceCollection AddB2CAuthentication(this IServiceCollection services, IConfiguration configuration,
            ILogger logger)
        {
            // TODO: Disable in production
            IdentityModelEventSource.ShowPII = true;

            return services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false;
                    option.IncludeErrorDetails = true;
                    option.SaveToken = true;
                    option.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            logger.LogError(context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var con = context.SecurityToken;
                            // TODO: Check valid tenant
                            return Task.CompletedTask;
                        }
                    };
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimTypeRetriever = (token, s) => token.Id,
                        // TODO: this make the authentication unuseful
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = false,
                        SignatureValidator = (token, parameters) => new JwtSecurityToken(token),
                        ClockSkew = new System.TimeSpan(0, 5, 0)
                        // TODO: with this line we will validate the SymmetricKey
                        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("cCXQwODSm79PIg-1y~s.l0uFbW231730--")),
                    };
                })
                .Services;
        }

        public static IServiceCollection AddB2CAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
                options.AddPolicy("auth", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
        }
    }
}