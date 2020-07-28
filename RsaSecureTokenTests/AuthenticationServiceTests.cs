using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test]
        public void is_valid()
        {
            // var target = new AuthenticationService();
            var target = new AuthenticationService(new FakeProfile(), new FakeToken());

            var actual = target.IsValid("joey", "91000000");

            //always failed
            Assert.IsTrue(actual);
        }
    }
    
    internal class FakeProfile : IProfile
    {
        public string GetPassword(string account) => account == "joey" ? "91" : "";
    }

    internal class FakeToken : IToken
    {
        public string GetRandom(string account) => "000000";
    }
}