using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private const float k_MinBatteryValue = 0f;
        private float m_BatteryLeft;
        private float m_MaxBatteryHourCapacity;

        public ElectricVehicle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, float i_MaxBatteryHourCapacity, byte i_NumOfWheels, VehicleOwner i_Owner) :
            base(i_VehicleModel, i_LicencePlate, i_NumOfWheels, i_Owner)
        {
            m_BatteryLeft = i_BatteryLeft;
            m_MaxBatteryHourCapacity = i_MaxBatteryHourCapacity;
        }

        public float BatteryLeft
        {
            get
            {
                return m_BatteryLeft;
            }

            set
            {
                if(value > k_MinBatteryValue && value <= m_MaxBatteryHourCapacity)
                {
                    m_BatteryLeft = value;
                }
            }
        }

        public float BatteryHourCapacity
        {
            get
            {
                return m_MaxBatteryHourCapacity;
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + string.Format(@"Vehicle battery hour capacity is: {0} hours
Vehicle battery left is: {1} hours", m_MaxBatteryHourCapacity, m_BatteryLeft);
        }
    }
}