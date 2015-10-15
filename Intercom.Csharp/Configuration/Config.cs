using System.Configuration;

namespace Intercom.Csharp.Configuration
{
    /// <summary>
    /// Configuration API:
    ///     1 user, 1 password, 1 url to itercom
    /// </summary>
    public class Config
    {
        /// <summary>
        /// The API consumer's username
        /// </summary>
        public static string AuthUsername
        {
            get
            {
                var config = ConfigurationManager.GetSection("intercom") as IntercomAccountRetrieverSection;
                IntercomAccountElement accountInfo = config.GetDefaultOrFirst();
                return accountInfo.AppID;
            }
        }

        /// <summary>
        /// The API consumer's password
        /// </summary>
        public static string AuthPassword
        {
            get
            {
                var config = ConfigurationManager.GetSection("intercom") as IntercomAccountRetrieverSection;
                IntercomAccountElement accountInfo = config.GetDefaultOrFirst();
                return accountInfo.APIKey;
            }
        }

        /// <summary>
        /// The base API URL
        /// </summary>
        public static string ApiBaseUrl { get; set; }

        static Config()
        {
            ApiBaseUrl = "https://api.intercom.io/";
        }
    }
}
