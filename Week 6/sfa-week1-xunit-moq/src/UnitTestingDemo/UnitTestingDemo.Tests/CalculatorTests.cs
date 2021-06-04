using System;
using System.Text;
using Xunit;

namespace UnitTestingDemo.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Addition_ValidNumbers_ReturnsCorrectResult()
        {
            // arrange
            var sut = new Calculator();
            var first = "10";
            var second = "20";

            // act
            var result = sut.Addition(first, second);

            // assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void Addition_InvalidNumber_ThrowsException()
        {
            // arrange
            var sut = new Calculator();
            var first = "10ss";
            var second = "20";

            // act
            Action act = () => sut.Addition(first, second);

            // assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}
