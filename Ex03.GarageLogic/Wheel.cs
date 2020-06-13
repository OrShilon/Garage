using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public struct Wheel
    {
        public string m_WheelMaker;
        public float m_CurrentAirPressure;
        public float m_MaxAirPressure;

        public Wheel(string i_WheelMaker, float i_CurrentAirPressure, float i_MaxAirPressure)
        {

            m_WheelMaker = i_WheelMaker;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }
        public override string ToString()
        {
            return String.Format(@"Wheel maker is: {0}
Wheels current air pressure is: {1}", m_WheelMaker, m_CurrentAirPressure);
        }
    }
}
