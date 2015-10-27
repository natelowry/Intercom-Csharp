using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Intercom.Csharp.Tools;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Intercom.Csharp.Users
{
    /// <summary>
    /// The paging view model
    /// </summary>
    public class UserViewModel<T> where T : class, new()
    {
        /// <summary>
        /// A list of User objects (same as returned by getting a single User)
        /// </summary>
        [DeserializeAs(Name = "users")]
        [JsonProperty("users")]
        public List<User<T>> Users { get; set; }

        /// <summary>
        /// How are listed the users
        /// </summary>
        [DeserializeAs(Name = "pages")]
        [JsonProperty("pages")]
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        [DeserializeAs(Name = "next")]
        [JsonProperty("next")]
        public string Next { get; set; }

        /// <summary>
        /// The total number of Users tracked in your Intercom application
        /// </summary>
        [DeserializeAs(Name = "total_count")]
        [JsonProperty("total_count")]
        public int Total { get; set; }

        /// <summary>
        /// The current requested page
        /// </summary>
        [DeserializeAs(Name = "page")]
        [JsonProperty("page")]
        public int? Page { get; set; }


        [DeserializeAs(Name = "per_page")]
        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        /// <summary>
        /// asc or desc
        /// </summary>
        [DeserializeAs(Name = "order")]
        [JsonProperty("order")]
        public bool? Order { get; set; }
    }

    /// <summary>
    /// A user uses a deserializing specific type to handle custom_data
    /// </summary>
    [DataContract]
    [JsonObject(MemberSerialization.OptIn)]
    public class User<T> where T : class, new()
    {
        public User()
        {
            Pseudonym = "";
        }
        /// <summary>
        /// The user's email address
        /// </summary>
        [DeserializeAs(Name = "email")]
        [JsonProperty("email")]
        public string Email { get; set; }


        /// <summary>
        /// A unique identifier for the user
        /// </summary>
        [DeserializeAs(Name = "id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// A unique identifier for the user
        /// </summary>
        [DeserializeAs(Name = "user_id")]
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// The user's full name
        /// </summary>
        [DeserializeAs(Name = "name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The datetime the user was created
        /// </summary>
        [DeserializeAs(Name = "created_at")]
        [JsonProperty("created_at")]
        [JsonConverter(typeof (UnixJsonDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [DeserializeAs(Name = "updated_at")]
        [JsonConverter(typeof(UnixJsonDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }

        [DeserializeAs(Name = "signed_up_at")]
        [JsonProperty("signed_up_at")]
        [JsonConverter(typeof(UnixJsonDateTimeConverter))]
        public DateTime? SignedAt { get; set; }

        [DeserializeAs(Name = "last_request_at")]
        [JsonProperty("last_request_at")]
        [JsonConverter(typeof(UnixJsonDateTimeConverter))]
        public DateTime? LastRequestAt { get; set; }

        /// <summary>
        /// A hash of key/value pairs containing any other data about the user you want Intercom to store.
        /// </summary>
        [DeserializeAs(Name = "custom_attributes")]
        [JsonProperty("custom_attributes")]
        public T CustomData { get; set; }

        [DeserializeAs(Name = "location_data")]
        public LocationData LocationData { get; set; }

        [DeserializeAs(Name = "session_count")]
        public int? SessionCount { get; set; }

        /// <summary>
        /// An IP address (e.g. "1.2.3.4") representing the last ip address the user visited your application from. (Used for updating location_data)
        /// </summary>
        [DeserializeAs(Name = "last_seen_ip")]
        [JsonProperty("last_seen_ip")]
        public string LastSeenIP { get; set; }

        /// <summary>
        /// The user agent the user last visited your application with.
        /// </summary>
        [DeserializeAs(Name = "last_seen_user_agent")]
        [JsonProperty("last_seen_user_agent")]
        public string LastSeenUserAgent { get; set; }

        [DeserializeAs(Name = "relationship_score")]
        public int? RelationshipScore { get; set; }

        [DeserializeAs(Name = "avatar")]
        public Avatar Avatar { get; set; }

        [DeserializeAs(Name = "unsubscribed_from_emails")]
        [JsonProperty("unsubscribed_from_emails")]
        public bool? UnsubscribedFromEmails { get; set; }

        [DeserializeAs(Name = "pseudonym")]
        public string Pseudonym { get; set; }

        [DeserializeAs(Name = "anonymous")]
        public bool? Anonymous { get; set; }

        [DeserializeAs(Name = "companies")]
        [JsonProperty("companies")]
        public List<Company> Companies { get; set; }

        [DeserializeAs(Name = "social_profiles")]
        public List<SocialProfile> SocialProfiles { get; set; }
    }

    public class Company
    {
        [DeserializeAs(Name = "id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DeserializeAs(Name = "remove")]
        [JsonProperty("remove")]
        public bool Remove { get; set; }
    }

    public class Avatar
    {
        [DeserializeAs(Name = "image_url")]
        [JsonProperty("image_url")]
        public string Url { get; set; }
    }

    public class SocialProfile
    {
        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "url")]
        public string Url { get; set; }

        [DeserializeAs(Name = "username")]
        public string Username { get; set; }

        [DeserializeAs(Name = "id")]
        public string Id { get; set; }
    }

    public class LocationData
    {
        [DeserializeAs(Name = "city_name")]
        public string City { get; set; }

        [DeserializeAs(Name = "continent_code")]
        public string ContinentCode { get; set; }

        [DeserializeAs(Name = "country_code")]
        public string CountryCode { get; set; }

        [DeserializeAs(Name = "country_name")]
        public string Country { get; set; }

        [DeserializeAs(Name = "latitude")]
        public double Latitude { get; set; }

        [DeserializeAs(Name = "longitude")]
        public double Longitude { get; set; }

        [DeserializeAs(Name = "postal_code")]
        public string PostalCode { get; set; }

        [DeserializeAs(Name = "region_name")]
        public string Region { get; set; }

        [DeserializeAs(Name = "timezone")]
        public string Timezone { get; set; }
    }
}

