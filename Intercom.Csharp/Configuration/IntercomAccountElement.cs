using System.Configuration;

namespace Intercom.Csharp.Configuration
{
    /// <summary>
    /// The object that holds information about an Intercom account, including app id and api key
    /// </summary>
    public class IntercomAccountElement : ConfigurationElement
    {
        /// <summary>
        /// The "Name" of the account, used when setting the Default
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// The application id (provided in the Intercom account) needed for security reasons
        /// </summary>
        [ConfigurationProperty("appid", IsRequired = true, DefaultValue = "myaccount@somedomain.com")]
        public string AppID
        {
            get { return (string)this["appid"]; }
            set { this["appid"] = value; }
        }

        /// <summary>
        /// The API key (provided in the Intercom account) needed for security reasons.
        /// This is your active API key, which can be seen through the "API Keys" menu item.
        /// </summary>
        [ConfigurationProperty("apikey", IsRequired = true, DefaultValue = "apikey")]
        public string APIKey
        {
            get { return (string)this["apikey"]; }
            set { this["apikey"] = value; }
        }
    }
}
