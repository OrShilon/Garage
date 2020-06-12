using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : FuelVehicle
    {
        internal eMotorcycleLicenceType m_LicenseType;
        internal int m_EngineVolume;
        public Motorcycle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, float i_MaxFuel, byte i_NumOfWheels, eMotorcycleLicenceType i_LicenseType, int i_EngineVolume, VehicleOwner i_Owner) :
            base(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_MaxFuel, i_NumOfWheels, eFuelTypes.Octan96, i_Owner)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + String.Format(@"Vehicle license type is: {0}
Vehicle engine volume is : {1} cc", m_LicenseType, m_EngineVolume);
        }
    }
}
