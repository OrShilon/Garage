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
       
    }

    
}
