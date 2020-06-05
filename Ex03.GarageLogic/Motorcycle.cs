using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Motorcycle : Vehicle
    {
        internal string m_LicenseType;
        internal int m_EngineVolume;
        internal const float m_MaxFuel = 7f;
        internal const byte m_NumOfWheels = 2;
        public Motorcycle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, int i_NumOfWheels, string i_LicenseType, int i_EngineVolume)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_FuelOrBatteryLeft = i_FuelLeft;
            m_Wheels = new Wheel[m_NumOfWheels];
            m_MaxFuelOrBattery = m_MaxFuel;
        }
    }
}
