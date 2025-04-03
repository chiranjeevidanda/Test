
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Core.Common;

namespace NEC.Fulf3PL.Application.Inbound.Implementation.COMMON
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

        public static IReadOnlyDictionary<string, List<(string name, string value)>> LoadGeodisHeaderParams(string companyId)
        {
            var collection = new Dictionary<string, List<(string name, string value)>>();


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
            collection.Add(Enums.Region.BRIAN_GEODIS.ToString(), new List<(string name, string value)>
                {
                    ("aeg-sas-key", configuration["GeodisBRIANClientSecret"])
                });
            collection.Add(Enums.Region.SOFIA_GEODIS.ToString(), new List<(string name, string value)>
                {
                    ("aeg-sas-key", configuration["GeodisSOFIAClientSecret"])
                });

            return collection;
        }


    }
}
