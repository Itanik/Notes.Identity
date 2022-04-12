using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Notes.Identity
{
    // содержит информацию о клиентах, ресурсах и пользователях
    public static class Configuration
    {
        // области доступа по идентификатору
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("NotesWebAPI", "Web API")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource> {
            new ApiResource("NotesWebAPI", "Web API", new [] { JwtClaimTypes.Name })
            {
                Scopes = { "NotesWebAPI" }
            }
        };

        public static IEnumerable<Client> Clients => new List<Client> {
            new Client
            {
                ClientId = "notes-web-api",
                ClientName = "Notes Web",
                AllowedGrantTypes = GrantTypes.Code,// Authorization code
                RequireClientSecret = false,
                RequirePkce = true, // значит нужен ключ подтверждения для Authorization code
                RedirectUris =
                {
                    "http://.../signin-oidc"
                },
                // набор адресов, которым позволено использовать Identity server
                AllowedCorsOrigins =
                {
                    "http://..."
                },
                PostLogoutRedirectUris =
                {
                    "http://.../signin-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "NotesWebAPI"
                },
                AllowAccessTokensViaBrowser = true,
            }
        };
    }
}
