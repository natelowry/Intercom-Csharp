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
            Assert.DoesNotThrow(() => { tmp = GetRequest(path); });
            Assert.That(tmp, Is.StringContaining("\"error.list\""));

        }

        public RestRequestingTest() : base("foo", "bar") { }
    }
}
