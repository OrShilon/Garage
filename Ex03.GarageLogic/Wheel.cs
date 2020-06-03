using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private string m_WheelMaker;
        private string m_CurrentAirPressure;
        private string m_MaxAirPressure;

        public Wheel(string i_WheelMaker, string i_CurrentAirPressure, string i_MaxAirPressure)
        {
            this.m_WheelMaker = i_WheelMaker;
            this.m_CurrentAirPressure = i_CurrentAirPressure;//
            this.m_MaxAirPressure = i_MaxAirPressure;
        }
    }
}
