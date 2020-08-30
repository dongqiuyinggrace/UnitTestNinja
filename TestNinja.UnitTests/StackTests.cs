using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Count_StackIsEmpty_ReturnZero()
        {
            var stack = new Stack<object>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_ArgumentIsNull_ThrowArgumentNullException()
        {
            var stack = new Stack<object>();
            
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ArgumentIsValid_ArgumentIsPushedToStack()
        {
            var stack = new Stack<object>();
            stack.Push(1);
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Stack<object>();
            
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackIsNotEmpty_TheLastItemIsPoped()
        {
            var stack = new Stack<object>();
            stack.Push(1);
            stack.Push(2);

            var lastItem = stack.Pop();
            
            Assert.That(lastItem, Is.EqualTo(2));
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Stack<object>();
            
            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackIsNotEmpty_ReturnTheLastItem()
        {
            var stack = new Stack<object>();
            stack.Push(1);
            stack.Push(2);

            var lastItem = stack.Peek();
            
            Assert.That(lastItem, Is.EqualTo(2));
            Assert.That(stack.Count, Is.EqualTo(2));
        }
    }
}