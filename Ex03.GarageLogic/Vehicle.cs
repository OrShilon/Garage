using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        internal string m_VehicleModel;
        public string m_LicencePlate;
        internal float m_FuelOrBatteryLeft;
        internal float m_MaxFuelOrBattery;
        internal Wheel[] m_Wheels;

        public Vehicle(string i_VehicleModel, string i_LicencePlate, float i_FuelOrBatteryLeft, float i_MaxFuelOrBattery, int i_NumOfWheels)
        {
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_FuelOrBatteryLeft = i_FuelOrBatteryLeft;
            m_MaxFuelOrBattery = i_MaxFuelOrBattery;
            m_Wheels = new Wheel[i_NumOfWheels];
        }
    }


}
