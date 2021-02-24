using System;
using System.Linq;
using System.Security.Claims;

namespace NetCoreApiScaffolding.Tools.Extensions
{
    public static class ClaimExtensions
    {
        // Here is the mapping of the claims to the longer names. Sadly there isn't any const or static for use here
        //https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/a301921ff5904b2fe084c38e41c969f4b2166bcb/src/System.IdentityModel.Tokens.Jwt/ClaimTypeMapping.cs#L45-L125
        public const string MicrosoftSchemaTenantClaimName = "http://schemas.microsoft.com/identity/claims/tenantid";
        public const string UserNameClaimName = "name";

        public static Guid? GetTenantId(this ClaimsPrincipal claims)
        {
            var claimsIdentity = claims.Identity as ClaimsIdentity;
            var tenantId = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == MicrosoftSchemaTenantClaimName)?.Value;
            return Guid.TryParse(tenantId, out var guid) ? (Guid?) guid : null;
        }

        public static string GetFullName(this ClaimsPrincipal claims)
        {
            var claimsIdentity = claims.Identity as ClaimsIdentity;
            return claimsIdentity?.Claims.FirstOrDefault(x => x.Type == UserNameClaimName)?.Value;
        }
    }
}