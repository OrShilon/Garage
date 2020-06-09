using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        internal string m_VehicleModel;
        public string m_LicencePlate;
        internal Wheel[] m_Wheels;
        VehicleOwner owner;

        public Vehicle(string i_VehicleModel, string i_LicencePlate, byte i_NumOfWheels, VehicleOwner i_Owner)
        {
            m_VehicleModel = i_VehicleModel;
            m_LicencePlate = i_LicencePlate;
            m_Wheels = new Wheel[i_NumOfWheels];
            owner = i_Owner;
        }

        public override string ToString()
        {
            return String.Format(@"Vehicle model is: {0}
Vehicle license plate number is: {1}", m_VehicleModel, m_LicencePlate);
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

        public static bool operator !=(Vehicle i_LeftVehicle, Vehicle i_RightVehicle)
        {
            return !(i_LeftVehicle == i_RightVehicle);
        }

    }

    
}
