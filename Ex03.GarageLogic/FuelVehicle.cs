using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        private float m_FuelLeft;
        private readonly float m_FuelTankCapacity;
        private eFuelTypes m_FuelType;

        public FuelVehicle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, float i_FuelTankCapacity, byte i_NumOfWheels, eFuelTypes i_FuelType,VehicleOwner i_Owner) :
            base(i_VehicleModel, i_LicencePlate, i_NumOfWheels, i_Owner)
        {
            m_FuelLeft = i_FuelLeft;
            m_FuelTankCapacity = i_FuelTankCapacity;
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
                if (value > 0f)
                {
                    m_FuelLeft = value;
                }
            }
        }
        public float FuelTankCapacity
        {
            get
            {
                return m_FuelTankCapacity;
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
                if (value > 0f)
                {
                    m_FuelType = value;
                }
            }
        }
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +  String.Format(@"Vehicle fuel type is: {0}
Vehicle fuel tank capacity is: {1} liters
Vehicle fuel left is: {2} liters", m_FuelType, m_FuelTankCapacity, m_FuelLeft);
        }
    }
}
