using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class OrderNumberGenerator
    {
        private static readonly OrderNumberGenerator _instance = new OrderNumberGenerator();
        private int _lastNumber = 1000;

        private OrderNumberGenerator() { }

        public static OrderNumberGenerator GetInstance()
        {
            return _instance;
        }

        public int GetNextNumber()
        {
            return ++_lastNumber;
        }
    }
}
