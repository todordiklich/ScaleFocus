using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestingDemo.Tests
{
    public class EntranceCounterTests
    {
        [Fact]
        public void EntranceCounter_DefaultValue_ShouldBeZero()
        {
            // arrange and act
            var sut = new EntranceCounter();

            // assert
            Assert.Equal(0, sut.Value);
        }

        [Fact]
        public void Enter_EnterOne_ValueShouldBeIncremented()
        {
            // arrange
            var sut = new EntranceCounter();

            // act
            sut.Enter();

            // assert
            Assert.Equal(1, sut.Value);
        }

        [Fact]
        public void Enter_EnterOneToExistingNumber_ValueShouldBeIncremented()
        {
            // arrange
            var sut = new EntranceCounter();
            sut.Enter();

            // act
            sut.Enter();

            // assert
            Assert.Equal(2, sut.Value);
        }

        [Fact]
        public void Enter_EnterBulk_ValueShouldBeIncremented()
        {
            // arrange
            var sut = new EntranceCounter();

            // act
            sut.Enter(3);

            // assert
            Assert.Equal(3, sut.Value);
        }
    }
}
