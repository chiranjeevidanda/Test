
namespace NEC.Fulf3PL.Core.Common
{
    public class ExternalRequest
    {
        private string _basicToken = string.Empty;
        public ExternalRequest()
        {
            Header = new Dictionary<string, string>();
        }

        public string URL { get; set; } = null!;
        public Dictionary<string, string> Header { get; set; }
        public string Body { get; set; }
        public string BearerToken { get; set; }
        public string BasicToken
        {
            get { return _basicToken; }
            set { _basicToken = value; }
        }
        public string Base64Encoded
        {
            get
            {
                //var authenticationString = $"{clientId}:{clientSecret}";
                if (!string.IsNullOrEmpty(_basicToken))
                    //return Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_basicToken));
                    return System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1")
                               .GetBytes(_basicToken));
                else
                    return _basicToken;
            }
        }

        public string? AdditionalInfo { get; set; }

        public string? ContentType { get; set; }
        public Dictionary<string, string> FormData { get; set; }

    }
}
