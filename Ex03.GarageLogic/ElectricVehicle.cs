using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private float m_BatteryLeft;
        private readonly float m_BatteryHourCapacity;

        public ElectricVehicle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, float i_BatteryHourCapacity, byte i_NumOfWheels, VehicleOwner i_Owner) :
            base(i_VehicleModel, i_LicencePlate, i_NumOfWheels, i_Owner)
        {
            m_BatteryLeft = i_BatteryLeft;
            m_BatteryHourCapacity = i_BatteryHourCapacity;
        }

        public float BatteryLeft
        {
            get
            {
                return m_BatteryLeft;
            }
            set
            {
                if(value > 0f)
                {
                    m_BatteryLeft = value;
                }
            }
        } 
        public float BatteryHourCapacity
        {
            get
            {
                return m_BatteryHourCapacity;
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + String.Format(@"Vehicle battery hour capacity is: {0} hours
Vehicle battery left is: {1} hours", m_BatteryHourCapacity, m_BatteryLeft);
        }
    }
    
}
