namespace Intercom.Csharp.Events
{
    public class EventService : RestRequesting
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username">The API username to use when accessing the API</param>
        /// <param name="password">The API password to use when accessing the API</param>
        public EventService(string username, string password)
            : base(username, password)
        { }

        
        /// <summary>
        /// Retrieves a user. Expects either the email or user_id you used to create the user.
        /// </summary>
        /// <param name="pevent">The event to send.</param>
        /// <returns>True if the action was successfull.</returns>
        /// <exception cref="IntercomException">Something happened on intercom.</exception>
        public bool Send<T>(Event<T> pevent) where T : class, new()
        {
            return PostRequest(pevent, "/events");
        }
    }
}

