using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private string m_Name;
        private string m_PhoneNumber;
        private const int k_EmptyPhoneNumber = 0;

        public VehicleOwner(string i_Name, string i_PhoneNumber)
        {
            m_Name = i_Name;
            m_PhoneNumber = i_PhoneNumber;
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
                if (value.Length > k_EmptyPhoneNumber) 
                {
                    m_PhoneNumber = value;
                }
            }
        }

        public override string ToString()
        {
            return String.Format(@"Owners name is: {0}
Owner's phone number is: {1}", m_Name, m_PhoneNumber);
        }
    }
}
