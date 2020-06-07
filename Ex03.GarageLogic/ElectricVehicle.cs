using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        internal float m_BatteryLeft;
        internal float m_BatteryHourCapacity;

        public ElectricVehicle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, float i_BatteryHourCapacity, byte i_NumOfWheels) :
            base(i_VehicleModel, i_LicencePlate, i_NumOfWheels)
        {
            m_BatteryLeft = i_BatteryLeft;
            m_BatteryHourCapacity = i_BatteryHourCapacity;
        }
    }
    
}
