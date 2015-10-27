using System;
using System.Runtime.Serialization;
using Intercom.Csharp.Tools;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Intercom.Csharp.Events
{
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class Event<T> where T : class, new()
    {
        /// <summary>
        /// Event's full name describing it's purpose
        /// </summary>
        [DeserializeAs(Name = "event_name")]
        [JsonProperty("event_name")]
        public string EventName { get; set; }

        /// <summary>
        /// The datetime the event was created
        /// </summary>
        [DeserializeAs(Name = "created_at")]
        [JsonProperty("created_at")]
        [JsonConverter(typeof(UnixJsonDateTimeConverter))]
        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// The unique identifier for the user
        ///    eitheir an Id or an email address must be set
        /// </summary>
        [DeserializeAs(Name = "user_id")]
        [JsonProperty("user_id", Required = Required.Default)]
        public string UserId { get; set; }

        /// <summary>
        /// The user's email address
        ///    eitheir an Id or an email address must be set
        /// </summary>
        [DeserializeAs(Name = "email")]
        [JsonProperty("email", Required = Required.Default)]
        public string Email { get; set; }


        /// <summary>
        /// Whatever you want Intercom to store.
        /// </summary>
        [DeserializeAs(Name = "metadata")]
        public T MetaData { get; set; }
    }
}
