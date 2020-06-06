using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public struct Wheel
    {
        private string m_WheelMaker;
        private string m_CurrentAirPressure;
        private byte m_MaxAirPressure;

        public Wheel(string i_WheelMaker, string i_CurrentAirPressure, byte i_MaxAirPressure)
        {

            m_WheelMaker = i_WheelMaker;
            m_CurrentAirPressure = i_CurrentAirPressure;//
            m_MaxAirPressure = i_MaxAirPressure;
        }
    }
}
