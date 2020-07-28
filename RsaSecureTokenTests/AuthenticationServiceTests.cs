﻿using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private readonly IProfile _fakeProfile = Substitute.For<IProfile>();
        private readonly IToken _fakeToken = Substitute.For<IToken>();
        private readonly AuthenticationService _target;

        public AuthenticationServiceTests()
        {
            _target = new AuthenticationService(_fakeProfile, _fakeToken);
        }

        [Test]
        public void is_valid()
        {
            GivenProfile("joey", "91");
            GivenToken("000000");
            ShouldBeValid("joey", "91000000");
        }

        private void ShouldBeValid(string account, string password)
        {
            _target.IsValid(account, password).Should().BeTrue();
        }

        private void GivenToken(string token)
        {
            _fakeToken.GetRandom("").ReturnsForAnyArgs(token);
        }

        private void GivenProfile(string account, string password)
        {
            _fakeProfile.GetPassword(account).Returns(password);
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