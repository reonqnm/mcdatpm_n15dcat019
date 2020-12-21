using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Authentication.Configuration.IdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("API.Core", "API Core"),
            };
        }
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
    {
        new Client
        {
            ClientId = "Inside",
            ClientName = "Inside",
            AllowedGrantTypes = GrantTypes.ClientCredentials,

           // RequireConsent = false,

            ClientSecrets =
            {
                new Secret(configuration["Client:Inside:Secret"].Sha256())
            },
          

            AllowedScopes =
            {
                
                "API.Core"
            },
            AllowOfflineAccess = true,
            AccessTokenLifetime = 2592000,
            IdentityTokenLifetime = 2592000
        }
    };
        }
    }
}
