
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Common;
using System.Text;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Helpers
{
    public static class DeliveryProcessHelper
    {
        private static IConfiguration configuration;

        public static void Initialize(IConfiguration config)
        {
            configuration = config;
        }

        public static IReadOnlyDictionary<string, List<(string name, string value)>> LoadQueryParams()
        {
            Dictionary<string, List<(string name, string value)>> collection = GetKeyValues();

            return collection;
        }

        private static Dictionary<string, List<(string name, string value)>> GetKeyValues()
        {
            var collection = new Dictionary<string, List<(string name, string value)>>();
            for (int i = 1; i <= 1; i++)
            {
                collection.Add(configuration[$"AuthKey{i}Name"], new List<(string name, string value)>
                {
                    ("client_id", configuration[$"AuthKey{i}ClientId"]),
                    ("client_secret", configuration[$"AuthKey{i}ClientSecret"]),
                    ("grant_type", "client_credentials"),
                    ("scope", configuration[$"AuthKey{i}Scope"])
                });
            }
            return collection;
        }

        public static IReadOnlyDictionary<string, List<(string name, string value)>> LoadKtnHeaderParams(bool additionalHeadersFlag, string customer)
        {
            var collection = new Dictionary<string, List<(string name, string value)>>();
            string companyId;
            if (customer == Constants.NEWERAB2C)
            {
                companyId = configuration["KtnCompanyIdB2C"];
            }
            else if (customer == Constants.NEWERAPTO)
            {
                companyId = configuration["KtnCompanyIdPTO"];
            }
            else
            {
                companyId = configuration["KtnCompanyId"];
            }

            if (additionalHeadersFlag)
            {
                collection.Add(Enums.OutboundAPIEndpoint.PurchaseOrder.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });
                collection.Add(Enums.OutboundAPIEndpoint.ProductMaster.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });
                collection.Add(Enums.OutboundAPIEndpoint.CreateOrder.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });
                collection.Add(Enums.OutboundAPIEndpoint.ReturnOrder.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });

                collection.Add(Enums.OutboundAPIEndpoint.Color.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company",companyId),
                    ("If-match", "*")
                });
                collection.Add(Enums.OutboundAPIEndpoint.Size.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });
                collection.Add(Enums.OutboundAPIEndpoint.Design.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });
                collection.Add(Enums.OutboundAPIEndpoint.Model.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });
                collection.Add(Enums.OutboundAPIEndpoint.Season.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", "*")
                });
            }
            else
            {
                //PurchaseOrder 
                collection.Add(Enums.OutboundAPIEndpoint.PurchaseOrder.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
                //ProductMaster 
                collection.Add(Enums.OutboundAPIEndpoint.ProductMaster.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
                //Create Order 
                collection.Add(Enums.OutboundAPIEndpoint.CreateOrder.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
                //ReturnOrder 
                collection.Add(Enums.OutboundAPIEndpoint.ReturnOrder.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            }

            return collection;
        }

        public static IReadOnlyDictionary<string, List<(string name, string value)>> LoadGeodisHeaderParams()
        {
            var collection = new Dictionary<string, List<(string name, string value)>>();

            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{configuration["GeodisAuthName"]}:{configuration["GeodisAuthPass"]}"));

            collection.Add(Enums.OutboundAPIEndpoint.PurchaseOrder.ToString(), new List<(string name, string value)>
                {
                    ("Authorization", $"Basic {credentials}")
                });

            collection.Add(Enums.OutboundAPIEndpoint.ProductMaster.ToString(), new List<(string name, string value)>
                {
                    ("Authorization", $"Basic {credentials}")
                });

            collection.Add(Enums.OutboundAPIEndpoint.CreateOrder.ToString(), new List<(string name, string value)>
                {
                    ("Authorization", $"Basic {credentials}")
                });

            collection.Add(Enums.OutboundAPIEndpoint.ReturnOrder.ToString(), new List<(string name, string value)>
                {
                    ("Authorization", $"Basic {credentials}")
                });

            return collection;
        }
    }
}
