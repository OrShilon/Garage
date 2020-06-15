using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_VehicleModel;
        private string m_LicencePlate;
        private Wheel[] m_Wheels;
        private VehicleOwner owner;

        public Vehicle(string i_VehicleModel, string i_LicencePlate, byte i_NumOfWheels, VehicleOwner i_Owner)
        {
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_Wheels = new Wheel[i_NumOfWheels];
            owner = i_Owner;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run - time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Vehicle vehicle = (Vehicle)obj;
                return vehicle.m_LicencePlate.Equals(m_LicencePlate);
            }
        }

        public static bool operator ==(Vehicle i_LeftVehicle, Vehicle i_RightVehicle)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(i_LeftVehicle, null))
            {
                if (Object.ReferenceEquals(i_RightVehicle, null))
                {
                    // null == null = true.
                    return true;
                }
                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return i_LeftVehicle.Equals(i_RightVehicle);
        }

        //private string m_VehicleModel;
        //private string m_LicencePlate;
        //private Wheel[] m_Wheels;
        //private VehicleOwner owner;
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
                throw new ValueOutOfRangeException(m_Wheels[0].MaxAirPressure, GarageManager.k_Zero);
            }
            else
            {
                for (int i = 0; i < m_Wheels.Length; i++)
                {
                    m_Wheels[i].CurrentAirPressure = i_WheelPressure;
                }
            }
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
                if (value.Length != 0)
                {
                    m_VehicleModel = value;
                }
            }
        }

        public override string ToString()
        {
            return String.Format(@"Vehicle model is: {0}
Vehicle license plate number is: {1}
{2}
{3}", m_VehicleModel, m_LicencePlate, owner.ToString(), m_Wheels[0].ToString());
        }
    }    
}
