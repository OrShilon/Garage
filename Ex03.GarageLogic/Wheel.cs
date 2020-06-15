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
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(string i_WheelMaker, float i_CurrentAirPressure, float i_MaxAirPressure)
        {

            m_WheelMaker = i_WheelMaker;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public string WheelMaker
        {
            get
            {
                return m_WheelMaker;
            }
            set
            {
                if (value.Length != GarageManager.k_Zero)
                {
                    m_WheelMaker = value;
                }
            }
        }
        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value <= m_MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                m_MaxAirPressure = value;
            }
        }


        public override string ToString()
        {
            return String.Format(@"Wheel maker is: {0}
Wheels current air pressure is: {1}", m_WheelMaker, m_CurrentAirPressure);
        }
    }
}
