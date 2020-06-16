using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : FuelVehicle
    {
        private eCarColors m_Color;
        private eNumOfDoors m_NumOfDoors;

        public Car(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, float i_MaxFuel, byte i_NumOfWheels, eCarColors i_Color, eNumOfDoors i_NumOfDoors, VehicleOwner i_Owner) : 
            base(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_MaxFuel, i_NumOfWheels, eFuelTypes.Octan96, i_Owner)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }

        internal eCarColors Color
        {
            get
            {
                return m_Color;
            }
        }

        internal eNumOfDoors Doors
        {
            get
            {
                return m_NumOfDoors;
            }
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + string.Format(@"Vehicle color is: {0}
Vehicle has {1} doors", m_Color, m_NumOfDoors);
        }
    }
}
