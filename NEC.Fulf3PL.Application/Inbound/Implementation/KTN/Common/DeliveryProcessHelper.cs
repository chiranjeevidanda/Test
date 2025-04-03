
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Model;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Common
{
    public static class DeliveryProcessHelper
    {
        private static IConfiguration configuration;

        public const string NEWERAB2B = "NEWERAB2B";
        public const string NEWERAB2C = "NEWERAB2C";
        public const string NEWERAPTO = "NEWERAPTO";

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

        public static IReadOnlyDictionary<string, List<(string name, string value)>> LoadKtnHeaderParams(string companyId)
        {
            var collection = new Dictionary<string, List<(string name, string value)>>();
            string company = companyId;

            //Transport Status 
            collection.Add(Enums.InboundAPIEndpoint.TransportStatus.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", company)
                });
            collection.Add(Enums.InboundAPIEndpoint.TransportLinks.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", company)
                });
            collection.Add(Enums.InboundAPIEndpoint.TransportExpandLines.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.TransportCollection.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", company)
                });
            collection.Add(Enums.InboundAPIEndpoint.TransportStockChange.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.OutboundStatus.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", company)
                });
            collection.Add(Enums.InboundAPIEndpoint.OutboundStockChanges.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.OutboundParcels.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.OutboundPallets.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.OutboundLineParcels.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.TransportOutboundStatus.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.TransportOutboundLinks.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.OutboundCollection.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.TransportOutboundCollection.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            collection.Add(Enums.InboundAPIEndpoint.ReturnExpandLines.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId)
                });
            return collection;
        }

        public static IReadOnlyDictionary<string, List<(string name, string value)>> LoadSofiaHeaderParams()
        {
            var collection = new Dictionary<string, List<(string name, string value)>>();

            //Transport Status 
            collection.Add(Enums.Region.EU.ToString(), new List<(string name, string value)>
                {
                    ("aeg-sas-key", configuration["EUClientSecret"])
                });
            collection.Add(Enums.Region.UK.ToString(), new List<(string name, string value)>
                {
                    ("aeg-sas-key", configuration["UKClientSecret"])
                });
            collection.Add(Enums.Region.BRIAN.ToString(), new List<(string name, string value)>
                {
                    ("aeg-sas-key", configuration["BRIANClientSecret"])
                });

            return collection;
        }


        /// <summary>
        /// Gets the web subscription extension header.
        /// </summary>
        /// <param name="data">The webhook extension model.</param>
        /// <returns>A dictionary containing the web subscription extension header.</returns>
        public static IReadOnlyDictionary<string, List<(string name, string value)>> GetWebSubscriptionExtensionHeader(string customer, string subscriptionEtag = "")
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

            collection.Add(Enums.WebhookSubscriptionType.TransportInbound.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", subscriptionEtag)
                });
            collection.Add(Enums.WebhookSubscriptionType.TransportOutbound.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", subscriptionEtag)
                });
            collection.Add(Enums.WebhookSubscriptionType.Outbound.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", subscriptionEtag)
                });
            collection.Add(Enums.WebhookSubscriptionType.ReturnReceipt.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", subscriptionEtag)
                });
            collection.Add(Enums.WebhookSubscriptionType.Inbound.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"]),
                    ("company", companyId),
                    ("If-match", subscriptionEtag)
                });
            collection.Add(Enums.WebhookSubscriptionType.WebhookSubscription.ToString(), new List<(string name, string value)>
                {
                    ("Ocp-Apim-Subscription-Key", configuration["KTNSubscriptionKey"])
                });


            return collection;
        }
    }
}
