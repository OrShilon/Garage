using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    
    class ElectricMotorcycle : Vehicle
    {
        internal string m_LicenseType;
        internal int m_EngineVolume;
        internal const float m_MaxBattery = 1.2f;
        internal const byte m_NumOfWheels = 2;
        public ElectricMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, string i_LicenseType, int i_EngineVolume)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_FuelOrBatteryLeft = i_BatteryLeft;
            m_Wheels = new Wheel[m_NumOfWheels];
            m_MaxFuelOrBattery = m_MaxBattery;
        }
    }
    
}
