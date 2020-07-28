using System;
using FluentAssertions;
using NUnit.Framework;

namespace RsaSecureToken.Tests
{
    [TestFixture]
    public class HolidayTests
    {
        private HolidayForTest _holidayForTest;

        [SetUp]
        public void Setup()
        {
            _holidayForTest = new HolidayForTest();
        }
        
        [Test]
        public void Today_Is_Xmas()
        {
            GivenToday(2020, 12, 25);
            ResponseShouldBe("Xmas");
        }

        [Test]
        public void Today_Is_Not_Xmas()
        {
            GivenToday(2020, 7, 28);
            ResponseShouldBe("Today is not Xmas");
        }

        private void ResponseShouldBe(string expectedResult)
        {
            var actualResult = _holidayForTest.SayHello();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        private void GivenToday(int year, int month, int day)
        {
            _holidayForTest.Today = new DateTime(year, month, day);
        }

        private class HolidayForTest : Holiday
        {
            private DateTime _today;

            protected override DateTime GetToday()
            {
                return _today;
            }

            public DateTime Today
            {
                set => _today = value;
            }
        }
    }
}