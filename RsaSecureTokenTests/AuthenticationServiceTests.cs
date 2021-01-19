using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private IProfile _profile;
        private IToken _token;
        private AuthenticationService _target;
        private ILog _log;

        [SetUp]
        public void Setup()
        {
            _profile = Substitute.For<IProfile>();
            _token = Substitute.For<IToken>();
            _log = Substitute.For<ILog>();
            _target = new AuthenticationService(_profile, _token, _log);
        }

        [Test()]
        public void is_valid()
        {
            GivenProfile("joey", "91");

            GivenToken("", "000000");

            ShouldBeValid("joey", "91000000");
        }

        [Test()]
        public void is_invalid()
        {
            GivenProfile("joey", "91");

            GivenToken("", "000000");

            ShouldBeInvalid("joey", "wrong password");
        }

        public void should_log_when_invalid()
        {
            GivenProfile("joey", "91");

            GivenToken("", "000000");

            _log.Received(1).Save(Arg.Is<string>(message => message.Contains("joey") && message.Contains("login failed")));
        }

        public void should_not_log_when_valid()
        {
            //_log.DidNotReceiveWithAnyArgs().Save();
        }

        // WhenIsInvalid

        // should log when invalid

        // should not log when valid

        private void ShouldBeInvalid(string account, string password)
        {
            var actual = _target.IsValid(account, password);

            Assert.IsFalse(actual);
        }

        private void ShouldBeValid(string account, string password)
        {
            var actual = _target.IsValid(account, password);

            Assert.IsTrue(actual);
        }

        private void GivenToken(string account, string returnPassword)
        {
            _token.GetRandom(account).ReturnsForAnyArgs(returnPassword);
        }

        private void GivenProfile(string account, string returnPassword)
        {
            _profile.GetPassword(account).Returns(returnPassword);
        }

        internal class FakeProfile : IProfile
        {
            public string GetPassword(string account)
            {
                if (account == "joey")
                {
                    return "91";
                }

                return "";
            }
        }

        internal class FakeToken : IToken
        {
            public string GetRandom(string account)
            {
                return "000000";
            }
        }
    }
}