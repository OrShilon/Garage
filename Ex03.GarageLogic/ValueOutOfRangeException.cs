using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base(string.Format("The value is out of range. please enter a number between {0} - {1}", i_MinValue, i_MaxValue))
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
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
