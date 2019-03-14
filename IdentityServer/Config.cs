using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "bal323",
                    ClientSecrets =
                    {
                        new Secret("bal323-secret". Sha256())
                    }, 
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "test-API"
                    }
                }
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("test-API", "My API just for test"), 
                new ApiResource("weather-API", "My fabulous weather API"),
            };
        }

    }
}