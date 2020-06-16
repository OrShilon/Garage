using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        private const float k_MinFuelValue = 0f;
        private float m_FuelLeft;
        private float m_MaxFuelTankCapacity;
        private eFuelTypes m_FuelType;

        public FuelVehicle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, float i_MaxFuelTankCapacity, byte i_NumOfWheels, eFuelTypes i_FuelType, VehicleOwner i_Owner) :
            base(i_VehicleModel, i_LicencePlate, i_NumOfWheels, i_Owner)
        {
            m_FuelLeft = i_FuelLeft;
            m_MaxFuelTankCapacity = i_MaxFuelTankCapacity;
            m_FuelType = i_FuelType;
        }

        public float FuelLeft
        {
            get
            {
                return m_FuelLeft;
            }

            set
            {
                if (value > k_MinFuelValue && value <= m_MaxFuelTankCapacity)
                {
                    m_FuelLeft = value;
                }
            }
        }

        public float MaxFuelTankCapacity
        {
            get
            {
                return m_MaxFuelTankCapacity;
            }
        }

        public eFuelTypes FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + string.Format(@"Vehicle fuel type is: {0}
Vehicle fuel tank capacity is: {1} liters
Vehicle fuel left is: {2} liters", m_FuelType, m_MaxFuelTankCapacity, m_FuelLeft);
        }
    }
}
