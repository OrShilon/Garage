using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Motorcycle : FuelVehicle
    {
        internal string m_LicenseType;
        internal int m_EngineVolume;
        internal const float m_MaxFuel = 7f;
        internal const byte m_NumOfWheels = 2;
        public Motorcycle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_LicenseType, int i_EngineVolume) :
            base(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_MaxFuel, m_NumOfWheels, eFuelTypes.Octan96)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;

        }
    }
}
