using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class GarageManager
    {
        private static List<Vehicle> m_AllCarsInGarage;

        public static void PrintVehiclesInGarage()
        {
            foreach (Vehicle vehicle in m_AllCarsInGarage)
            {

            }
        }

        public static List<Vehicle> CarsList
        {
            get 
            {
                return m_AllCarsInGarage;
            }
        }
    }
}
