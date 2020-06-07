using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class BatteryVehicle : Vehicle
    {
        public float m_BatteryHourCapacity;

        public BatteryVehicle(string i_VehicleModel, string i_LicencePlate, float i_FuelOrBatteryLeft, float i_BatteryHourCapacity, byte i_NumOfWheels) :
            base(i_VehicleModel, i_LicencePlate, i_FuelOrBatteryLeft, i_BatteryHourCapacity, i_NumOfWheels)
        {
            if(i_BatteryHourCapacity == 1.2 || i_BatteryHourCapacity == 2.1)
            {
                m_BatteryHourCapacity = i_BatteryHourCapacity;
            }
            else
            {
                throw new ValueOutOfRangeException("NOT valid Battery Capacity");
            }
        }
    }
    
}
