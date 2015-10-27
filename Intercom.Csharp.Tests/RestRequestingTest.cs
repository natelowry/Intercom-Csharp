using NUnit.Framework;

namespace Intercom.Csharp.Tests
{
    public class RestRequestingTest : RestRequesting
    {
        [TestCase("users")]
        public void GetRequestTest(string path)
        {
            var ex = Assert.Throws<IntercomException>(() => { GetRequest(path); });
            Assert.That(ex.Message, Is.StringContaining("\"error.list\""));

        }

        public RestRequestingTest() : base("foo", "bar") { }
    }
}
