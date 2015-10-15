using System;

namespace Intercom.Csharp
{
    /// <summary>
    /// Intercom Exception
    /// </summary>
    [Serializable]
    public class IntercomException : Exception
    {
        /// <summary>
        /// Exception
        /// </summary>
        /// <param name="message">The exception message</param>
        public IntercomException(string message) : base(message) { }

        /// <summary>
        /// Exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The exception that caused this exception to be thrown</param>
        public IntercomException(string message, Exception innerException) : base(message, innerException) { }
    }
}
