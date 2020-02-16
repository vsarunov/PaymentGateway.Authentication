using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace PaymentGateway.Authentication.Helpers
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };
        }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("PaymentGateway.API", "PaymentGateway API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                
                // resource owner password grant client
                new Client
                {
                    ClientId = "paymentgateway.API",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("paymentgateway.secret".Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "PaymentGateway.API"
                    }
                }
            };
        }
    }
}
