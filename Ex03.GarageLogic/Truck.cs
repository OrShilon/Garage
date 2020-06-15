using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : FuelVehicle
    {
        private bool m_CarryDangerousMaterials; // false - not carring dangerous materials, true otherwise
        private float m_CargoVolume;

        public Truck(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, float i_MaxFuel, byte i_NumOfWheels, bool i_CarryDangerousMaterials, float i_CargoVolume, VehicleOwner i_Owner) :
            base(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_MaxFuel, i_NumOfWheels, eFuelTypes.Soler, i_Owner)
        {
            m_CarryDangerousMaterials = i_CarryDangerousMaterials;
            m_CargoVolume = i_CargoVolume;
        }


        internal bool CarryDangerousMaterials
        {
            get
            {
                return m_CarryDangerousMaterials;
            }
        }

        internal float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
        }
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
            String.Format(@"The vehicle cargo volume is: {0}
The vehicle {1} dangerous materials", m_CargoVolume, m_CarryDangerousMaterials ? "is carrying" : "is NOT carrying");
        }
    }
}
