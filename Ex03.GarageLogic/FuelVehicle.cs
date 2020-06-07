using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        internal float m_FuelLeft;
        internal float m_FuelTankCapacity;
        public eFuelTypes m_FuelType;

        public FuelVehicle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, float i_FuelTankCapacity, byte i_NumOfWheels, eFuelTypes i_FuelType) :
            base(i_VehicleModel, i_LicencePlate, i_NumOfWheels)
        {
            m_FuelLeft = i_FuelLeft;
            m_FuelTankCapacity = i_FuelTankCapacity;
            m_FuelType = i_FuelType;
        }
    }
}
