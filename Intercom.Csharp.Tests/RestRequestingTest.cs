using System.Net;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Intercom.Csharp.Tests
{
    public class RestRequestingTest : RestRequesting
    {
        [TestCase("users")]
        public void GetRequestTest(string path)
        {
            string tmp = "";
            var ex = Assert.Throws<IntercomException>(() => { tmp = GetRequest(path); });
            Assert.That(ex.Message, Is.StringContaining("\"error.list\""));

        }

        public RestRequestingTest() : base("foo", "bar") { }
    }
}
