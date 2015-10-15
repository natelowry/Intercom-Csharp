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
            Assert.DoesNotThrow(() => GetRequest(path));

        }

        public RestRequestingTest() : base("foo", "bar") { }
    }
}
