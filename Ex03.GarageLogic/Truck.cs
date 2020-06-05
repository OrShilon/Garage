using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        internal bool m_CarryDangerousMaterials; // false - not carring dangerous materials, true otherwise
        internal float m_CargoVolume;
        internal const float m_MaxFuel = 120f;
        internal const byte m_NumOfWheels = 16;

        public Truck(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, int i_NumOfWheels, bool i_CarryDangerousMaterials, float i_CargoVolume)
        {
            m_CarryDangerousMaterials = i_CarryDangerousMaterials;
            m_CargoVolume = i_CargoVolume;
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_FuelOrBatteryLeft = i_FuelLeft;
            m_Wheels = new Wheel[m_NumOfWheels];
            m_MaxFuelOrBattery = m_MaxFuel;
        }
    }
}
