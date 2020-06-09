using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        public string m_Name;
        public string m_PhoneNumber;
        public Vehicle m_OwnersVehicle;

        public VehicleOwner(string i_Name, string i_PhoneNumber, Vehicle i_OwnersVehicle)
        {
            m_Name = i_Name;
            m_PhoneNumber = i_PhoneNumber;
            m_OwnersVehicle = i_OwnersVehicle;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
            set
            {
                m_PhoneNumber = value;
            }
        }
    }
}
