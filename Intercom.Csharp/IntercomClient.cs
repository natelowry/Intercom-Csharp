using Intercom.Csharp.Configuration;
using Intercom.Csharp.Events;
using Intercom.Csharp.Users;

namespace Intercom.Csharp
{
    public class IntercomClient
    {
        #region Constructors

        public IntercomClient()
            : this(Config.AuthUsername, Config.AuthPassword)
        {
        }

        public IntercomClient(string username, string password)
        {
            Users = new UserService(username, password);
            Events = new EventService(username, password);
        }
        #endregion

        #region Accessors
        
        public UserService Users { get; private set; }
        public EventService Events { get; private set; }

        #endregion
    }
}
