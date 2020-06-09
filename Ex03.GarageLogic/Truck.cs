using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : FuelVehicle
    {
        internal bool m_CarryDangerousMaterials; // false - not carring dangerous materials, true otherwise
        internal float m_CargoVolume;
        internal const float m_MaxAirPressure = 28;

        public Truck(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, float i_MaxFuel, byte i_NumOfWheels, bool i_CarryDangerousMaterials, float i_CargoVolume, VehicleOwner i_Owner) :
            base(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_MaxFuel, i_NumOfWheels, eFuelTypes.Soler, i_Owner)
        {
            m_CarryDangerousMaterials = i_CarryDangerousMaterials;
            m_CargoVolume = i_CargoVolume;
        }

 
        //m_CarryDangerousMaterials
        //m_CargoVolume;
        //m_MaxAirPressure
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
            String.Format(@"The vehicle max air pressure is {0} and its cargo volume is {1}
The vehicle {2} carry dangerous materials", m_MaxAirPressure, m_CargoVolume, m_CarryDangerousMaterials);
        }
    }
}
