using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Car : FuelVehicle
    {
        internal string m_Color;
        internal int m_NumOfDoors;
        internal const float m_MaxFuel = 2.1f;
        internal const byte m_NumOfWheels = 4;

        public Car(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_Color, int i_NumOfDoors) : 
            base(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_MaxFuel, m_NumOfWheels, eFuelTypes.Octan96)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }
    }
}
