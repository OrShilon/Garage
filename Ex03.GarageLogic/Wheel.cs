using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public struct Wheel
    {
        internal string m_WheelMaker;
        internal float m_CurrentAirPressure;
        internal float m_MaxAirPressure;

        public Wheel(string i_WheelMaker, float i_CurrentAirPressure, float i_MaxAirPressure)
        {

            m_WheelMaker = i_WheelMaker;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }
    }
}
