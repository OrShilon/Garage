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

        public Car(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_Color, int i_NumOfDoors)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_FuelOrBatteryLeft = i_FuelLeft;
            m_Wheels = new Wheel[m_NumOfWheels];
            m_MaxFuelOrBattery = m_MaxFuel;
            m_FuelType = eFuelTypes.Octan96;
        }
    }
}
