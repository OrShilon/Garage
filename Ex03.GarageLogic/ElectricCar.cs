using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricCar : Vehicle
    {
        internal string m_Color;
        internal int m_NumOfDoors;
        internal const float m_MaxBattery = 2.1f;
        internal const byte m_NumOfWheels = 4;
        public ElectricCar(string i_VehicleModel, string i_LicencePlate, float i_EnergyLeft, string i_Color, int i_NumOfDoors) :
            base(i_VehicleModel, i_LicencePlate, i_EnergyLeft, m_MaxBattery, m_NumOfWheels)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }
    }
}
