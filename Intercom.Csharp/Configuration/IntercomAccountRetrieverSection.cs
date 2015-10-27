
using System.Configuration;

namespace Intercom.Csharp.Configuration
{
    /// <summary>
    /// Class that deals with getting the account elements in web/app.config
    /// </summary>
    public class IntercomAccountRetrieverSection : ConfigurationSection
    {
        /// <summary>
        /// The name of the default account, to be used in GetDefaultOrFirst() method.
        /// </summary>
        [ConfigurationProperty("defaultAccount")]
        public string DefaultAccount
        {
            get { return (string)base["defaultAccount"]; }
            set { base["defaultAccount"] = value; }
        }

        /// <summary>
        /// The collection of Intercom account elements
        /// </summary>
        [ConfigurationProperty("accounts", IsDefaultCollection = true)]
        public IntercomAccountElementCollection Accounts
        {
            get { return (IntercomAccountElementCollection)this["accounts"]; }
            set { this["accounts"] = value; }
        }

        /// <summary>
        /// Method that gets the default (as specified via the DefaultAccount property) or the first
        /// </summary>
        public IntercomAccountElement GetDefaultOrFirst()
        {
            IntercomAccountElement result = null;
            if (!string.IsNullOrEmpty(DefaultAccount))
            {
                foreach (IntercomAccountElement element in Accounts)
                {
                    if (element.Name == DefaultAccount)
                    {
                        result = element;
                        break;
                    }
                }
            }
            else
            {
                // If there are account elements in the web.config then ..
                if (Accounts.Count > 0)
                {
                    result = Accounts[0] as IntercomAccountElement;
                }
            }

            if (result == null) { throw new ConfigurationErrorsException("No accounts listed for Intercom in the .config configuation file."); }
            else
            {
                return result;
            }
        }
    }
}
