using System;
using System.Net;
using System.Security.Cryptography;
using Intercom.Csharp.Users;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Intercom.Csharp.Tests
{
    public class IntercomClientTest
    {
        [TestCase]
        public void CreateNewUser()
        {
            var client = new IntercomClient("dn4gzhun", "b3db3b24a6904f471899bb826af0a326a4f72238");
            Random rnd = new Random();
            var user = new User() { 
                Email = String.Format("unique{0}@yopmail.fr", rnd.Next()),
                Name = "User random",                 
                CreatedAt = DateTime.Parse("Jan 1, 2012"),
                LastSeenIP = string.Format("{0}.{1}.{2}.{3}", rnd.Next(255), rnd.Next(255), rnd.Next(255), rnd.Next(255)),
};
            var ret = client.Users.Create(user);
            Assert.IsNotNull(user);
            Assert.IsTrue(user.Email == ret.Email);
            Assert.IsTrue(user.CreatedAt != ret.CreatedAt); // this must be udated
            Assert.IsTrue(user.Name == ret.Name);
            Assert.IsTrue(user.LastSeenIP == ret.LastSeenIP);

            var continuing = client.Users.Single(ret.Email);
            Assert.IsNotNull(continuing);
            Assert.IsTrue(continuing.Email == ret.Email);
            Assert.IsTrue(continuing.CreatedAt == ret.CreatedAt);
            Assert.IsTrue(continuing.Name == ret.Name);
            Assert.IsTrue(continuing.LastSeenIP == ret.LastSeenIP);

            client.Users.Delete(ret.Email);

            Assert.Throws<IntercomException>(() => { client.Users.Single(ret.Email); });
        }

        [TestCase]
        public void SendEvents()
        {
            
        }
    }
}
