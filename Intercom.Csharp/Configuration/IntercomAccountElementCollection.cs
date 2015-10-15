using System.Configuration;

namespace Intercom.Csharp.Configuration
{
    /// <summary>
    /// The collection of Intercom account elements in web/app.config
    /// </summary>
    public class IntercomAccountElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create a new configuration element of type IntercomAccountElement
        /// </summary>        
        protected override ConfigurationElement CreateNewElement()
        {
            return new IntercomAccountElement();
        }

        /// <summary>
        /// Get the element
        /// </summary>
        /// <param name="element">The configuration element to retrieve</param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IntercomAccountElement)element).Name;
        }

        /// <summary>
        /// Get the account element by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IntercomAccountElement this[int index]
        {
            get
            {
                return (IntercomAccountElement)base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);

                this.BaseAdd(index, value);
            }
        }
    }
}
