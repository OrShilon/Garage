using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException()
        {
        }

        public ValueOutOfRangeException(string message)
            : base(message)
        {
        }

        public ValueOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
