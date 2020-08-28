using System.Linq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnSumOfTheArguments()
        {
            var sum = _math.Add(1, 2);

            Assert.That(sum, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2,1,2)]
        [TestCase(1,2,2)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnLargerArgument(int a, int b, int expectedResult)
        {
            var max = _math.Max(a, b);

            Assert.That(max, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUnderLimit()
        {
            var result = _math.GetOddNumbers(5);

            //too general
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(3));

            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));
            Assert.That(result, Is.EquivalentTo(new []{1,3,5}));
            
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);

        }

    }
}