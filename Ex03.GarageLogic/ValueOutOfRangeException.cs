using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_maxValue;
        private float m_minValue;
        public ValueOutOfRangeException(float i_maxValue, float i_minValue)
            : base(string.Format("The value is out of range. please enter a number between {0} - {1}", i_minValue, i_maxValue))
        {
            m_maxValue = i_maxValue;
            m_minValue = i_minValue;
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
