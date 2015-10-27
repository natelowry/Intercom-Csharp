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
        private User<CustomdataModel> BuildRandomUser()
        {
            Random rnd = new Random();
            return new User<CustomdataModel>
                       {
                           Email = String.Format("unique{0}@yopmail.fr", rnd.Next()),
                           Name = "User random",
                           CreatedAt = DateTime.Parse("Jan 1, 2012"),
                           LastSeenIP = string.Format("{0}.{1}.{2}.{3}", rnd.Next(255), rnd.Next(255), rnd.Next(255), rnd.Next(255)),
                           CustomData = new CustomdataModel { Titi = "toto", Value = 12 }
                       };
        }


        [TestCase]
        public void CreateNewUser()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            var user = BuildRandomUser();
            var ret = client.Users.Create(user);
            Assert.IsNotNull(ret);
            Assert.IsTrue(user.Email == ret.Email);
            Assert.IsTrue(user.CreatedAt != ret.CreatedAt); // this must be udated
            Assert.IsTrue(user.Name == ret.Name);
            Assert.IsTrue(user.LastSeenIP == ret.LastSeenIP);
        }

        [TestCase]
        public void RequestUser()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            var user = BuildRandomUser();
            var ret = client.Users.Create(user);
            var continuing = client.Users.Single<CustomdataModel>(ret.Email);
            Assert.IsNotNull(continuing);
            Assert.AreEqual(continuing.Email, ret.Email);
            Assert.AreEqual(continuing.CreatedAt, ret.CreatedAt);
            Assert.AreEqual(continuing.Name, ret.Name);
            Assert.AreEqual(continuing.LastSeenIP, ret.LastSeenIP);
            Assert.IsNotNull(continuing.CustomData);
            Assert.AreEqual(continuing.CustomData.Titi, user.CustomData.Titi);
            Assert.AreEqual(continuing.CustomData.Value, user.CustomData.Value);
        }

        [TestCase]
        public void DeleteUser()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            var user = BuildRandomUser();
            var ret = client.Users.Create(user);
            Assert.DoesNotThrow(() => { client.Users.Delete(ret.Email); });
            Assert.Throws<IntercomException>(() => { client.Users.Single<CustomdataModel>(ret.Email); });
        }

        [TestCase]
        public void UpdateUser()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            var user = BuildRandomUser();
            client.Users.Create(user);
            var ret = client.Users.Single<User<CustomdataModel>>(user.Email);
            var newversion = new User<CustomdataModel> { Email = ret.Email, Anonymous = true, Pseudonym = "trucmuche", Companies = new List<Company> { new Company() { Id = "15"}}};
            var updated = client.Users.Update(newversion);
            Assert.AreEqual(updated.Anonymous, ret.Anonymous);
            Assert.AreEqual(updated.Pseudonym, ret.Pseudonym);
            Assert.AreEqual(updated.Companies.Count, ret.Companies.Count);
            Assert.AreEqual(updated.Companies[0].Id, ret.Companies[0].Id);
        }

        [TestCase(1,3)]
        [TestCase(2, null)]
        [TestCase(null, 4)]
        [TestCase(null, null)]
        public void LookAtUsers(int? page, int? perPage)
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            var view = client.Users.All<User<CustomdataModel>>(page, perPage);
            Assert.IsTrue(view.Users.Count > 0);
            Assert.AreEqual(view.Pagination.Page, page.HasValue ? page : 1);
            Assert.AreEqual(view.Pagination.PerPage, perPage.HasValue ? perPage : 50);
        }

        [TestCase]
        public void SendEvents()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            var eventx = new Events.Event<CustomdataModel>
                         {
                UserId = "1",
                CreatedAt = DateTime.Parse("Jan 1, 2012"),
                EventName = "call-bolden",
                MetaData = new CustomdataModel { Titi = "toto", Value = 12 }
            };
            Assert.Throws<IntercomException>(() => { client.Events.Send(eventx); });
            var user = new User<CustomdataModel>
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
