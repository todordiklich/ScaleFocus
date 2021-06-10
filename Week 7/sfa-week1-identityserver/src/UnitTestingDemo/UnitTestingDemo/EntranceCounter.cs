using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingDemo
{
    public class EntranceCounter
    {
        public int Value { get; private set; }

        public void Enter(int number = 1)
        {
            Value += number;
        }

        public void Exit(int number = 1)
        {
            Value -= number;
        }
    }
}
