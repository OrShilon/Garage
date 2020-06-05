using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract class FuelVehicle : Vehicle
    {
        internal eFuelTypes m_FuelType;

        public FuelVehicle(string i_VehicleModel, string i_LicencePlate, float i_FuelOrBatteryLeft, float i_MaxFuelOrBattery, byte i_NumOfWheels, eFuelTypes i_FuelType) :
            base(i_VehicleModel, i_LicencePlate, i_FuelOrBatteryLeft, i_MaxFuelOrBattery, i_NumOfWheels)
        {
            m_FuelType = i_FuelType;
        }
    }


}
