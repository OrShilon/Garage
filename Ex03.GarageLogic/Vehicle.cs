using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private const int k_EmptyName = 0;
        private readonly Wheel[] m_Wheels;
        private string m_VehicleModel;
        private string m_LicencePlate;
        private VehicleOwner owner;

        public Vehicle(string i_VehicleModel, string i_LicencePlate, byte i_NumOfWheels, VehicleOwner i_Owner)
        {
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_Wheels = new Wheel[i_NumOfWheels];
            owner = i_Owner;
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public void SetWheelPressure(float i_WheelPressure)
        {
            if(m_Wheels[0].MaxAirPressure < i_WheelPressure)
            {
                throw new ValueOutOfRangeException(m_Wheels[0].MaxAirPressure, GarageManager.k_MininumRangeValue);
            }
            else
            {
                for (int i = 0; i < m_Wheels.Length; i++)
                {
                    m_Wheels[i].CurrentAirPressure = i_WheelPressure;
                }
            }
        }

        public override bool Equals(object obj)
        {
            bool isEquals = false;

            // Check for null and compare run - time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                isEquals = false;
            }
            else
            {
                Vehicle vehicle = (Vehicle)obj;
                isEquals = vehicle.m_LicencePlate.Equals(m_LicencePlate);
            }

            return isEquals;
        }

        public static bool operator ==(Vehicle i_LeftVehicle, Vehicle i_RightVehicle)
        {
            bool isEquals = false;

            if(object.ReferenceEquals(i_LeftVehicle, null) && object.ReferenceEquals(i_RightVehicle, null))
            {
                // null == null --> true.
                isEquals = true;
            }
            else if (!object.ReferenceEquals(i_LeftVehicle, null) && object.ReferenceEquals(i_RightVehicle, null))
            {
                // Both not null, check if the licence plate equals.
                isEquals = i_LeftVehicle.m_LicencePlate.Equals(i_RightVehicle.m_LicencePlate);
            }
            else
            {
                // Only one of the Vehicles is not null
                isEquals = false;
            }

            return isEquals;
        }

        public static bool operator !=(Vehicle i_LeftVehicle, Vehicle i_RightVehicle)
        {
            return !(i_LeftVehicle == i_RightVehicle);
        }

        public string LicencePlate
        {
            get
            {
                return m_LicencePlate;
            }
        }

        public string VehicleModel
        {
            get
            {
                return m_VehicleModel;
            }

            set
            {
                if (value.Length != k_EmptyName)
                {
                    m_VehicleModel = value;
                }
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(@"Vehicle model is: {0}
Vehicle license plate number is: {1}
{2}
{3}", m_VehicleModel, m_LicencePlate, owner.ToString(), m_Wheels[0].ToString());
        }
    }    
}
