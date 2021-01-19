using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RsaSecureToken;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace RsaSecureTokenTests
{
    [TestFixture]
    public class HolidayTests
    {
        private HolidayForTest _holiday;

        [SetUp]
        public void Setup()
        {
            _holiday = new HolidayForTest();
        }

        [Test]
        public void Today_Is_Not_Xmas()
        {
            GivenToday(11, 25);
            ResponseShouldBe("Today is not Xmas");
        }

        private void ResponseShouldBe(string expectedString)
        {
            Assert.AreEqual(expectedString, _holiday.SayHello());
        }

        private void GivenToday(int month, int day)
        {
            _holiday.Today = new DateTime(2020, month, month);
        }

        [Test]
        public void Today_Is_Xmas()
        {
            // arrange
            var holidayForTest = new HolidayForTest();
            holidayForTest.Today = new DateTime(2020, 12, 25);
            var expectedHelloString = "Xmas";

            // act
            var sayHelloString = holidayForTest.SayHello();

            // assert
            Assert.AreEqual(sayHelloString, expectedHelloString);
        }

        internal class HolidayForTest : Holiday
        {
            private DateTime _today;

            public DateTime Today
            {
                set => _today = value;
            }

            protected override DateTime GetToday()
            {
                return _today;
            }
        }
    }
}
