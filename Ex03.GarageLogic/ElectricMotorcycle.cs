using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    
    public class ElectricMotorcycle : Vehicle
    {
        internal string m_LicenseType;
        internal int m_EngineVolume;

        public ElectricMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, float i_MaxBattery, byte i_NumOfwheels, string i_LicenseType, int i_EngineVolume) :
            base(i_VehicleModel, i_LicencePlate, i_BatteryLeft, i_MaxBattery, i_NumOfwheels)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }
    }
    
}
