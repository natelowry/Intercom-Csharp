using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Intercom.Csharp.Users;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp.Deserializers;

namespace Intercom.Csharp.Tests
{
    [DataContract]
    [JsonObject(MemberSerialization.OptOut)]
    public class CustomdataModel
    {
        [DeserializeAs(Name = "titi")]
        [JsonProperty("titi")]
        public string Titi { get; set; }
        public int Value { get; set; }
    }

    public class IntercomClientTest
    {
        [TestCase]
        public void CreateNewUser()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            Random rnd = new Random();
            var user = new User<CustomdataModel>()
                       {
                           Email = String.Format("unique{0}@yopmail.fr", rnd.Next()),
                           Name = "User random",
                           CreatedAt = DateTime.Parse("Jan 1, 2012"),
                           LastSeenIP = string.Format("{0}.{1}.{2}.{3}", rnd.Next(255), rnd.Next(255), rnd.Next(255), rnd.Next(255)),
                           CustomData = new CustomdataModel { Titi = "toto", Value = 12 }
                       };
            var ret = client.Users.Create(user);
            Assert.IsNotNull(ret);
            Assert.IsTrue(user.Email == ret.Email);
            Assert.IsTrue(user.CreatedAt != ret.CreatedAt); // this must be udated
            Assert.IsTrue(user.Name == ret.Name);
            Assert.IsTrue(user.LastSeenIP == ret.LastSeenIP);
            
            var continuing = client.Users.Single<CustomdataModel>(ret.Email);
            Assert.IsNotNull(continuing);
            Assert.IsTrue(continuing.Email == ret.Email);
            Assert.IsTrue(continuing.CreatedAt == ret.CreatedAt);
            Assert.IsTrue(continuing.Name == ret.Name);
            Assert.IsTrue(continuing.LastSeenIP == ret.LastSeenIP);
            Assert.IsNotNull(continuing.CustomData);
            Assert.AreEqual(continuing.CustomData.Titi, user.CustomData.Titi);
            Assert.AreEqual(continuing.CustomData.Value, user.CustomData.Value);
            client.Users.Delete(ret.Email);

            Assert.Throws<IntercomException>(() => { client.Users.Single<CustomdataModel>(ret.Email); });
        }

        [TestCase]
        public void SendEvents()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            var eventx = new Events.Event<CustomdataModel>()
            {
                UserId = "1",
                CreatedAt = DateTime.Parse("Jan 1, 2012"),
                EventName = "call-bolden",
                MetaData = new CustomdataModel { Titi = "toto", Value = 12 }
            };
            Assert.Throws<IntercomException>(() => { client.Events.Send(eventx); });
            var user = new User<CustomdataModel>()
            {
                Email = "event-tester@yopmail.fr",
                Name = "event-tester",
                UserId = "50"
            };
            var ret = client.Users.Create(user);
            eventx.UserId = ret.UserId;
            Assert.DoesNotThrow(() => { client.Events.Send(eventx); });
            eventx.UserId = null;
            eventx.Email = ret.Email;
            Assert.DoesNotThrow(() => { client.Events.Send(eventx); });
            client.Users.Delete(ret.Email);

        }
    }
}
