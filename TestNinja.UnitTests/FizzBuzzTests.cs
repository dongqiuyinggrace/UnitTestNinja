using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{   [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(15)]
        [TestCase(30)]
        public void GetOutput_CanBeDevidedBy3and5_ReturnFizzBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        [TestCase(3)]
        [TestCase(9)]
        public void GetOutput_CanBeDevidedBy3ButNot5_ReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        public void GetOutput_CanBeDevidedBy5ButNot3_ReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        [TestCase(1)]
        [TestCase(4)]
        public void GetOutput_CanNotBeDevidedBy3Or5_ReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(number.ToString()));
        }
        
    }
}