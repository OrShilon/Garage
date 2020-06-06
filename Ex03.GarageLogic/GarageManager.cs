using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class GarageManager
    {
        private static List<Vehicle> m_AllVehiclesInGarage = new List<Vehicle>();

        public static void PrintVehiclesInGarage()
        {
            foreach (Vehicle vehicle in m_AllVehiclesInGarage)
            {
                //need to print after filtering by status
            }
        }

        public static List<Vehicle> VehiclesList
        {
            get 
            {
                return m_AllVehiclesInGarage;
            }
        }

        public static void AddVehicleToGarage(Vehicle vehicle)
        {
            m_AllVehiclesInGarage.Add(vehicle);
        }
        public static void RemoveVehicleFromGarage(Vehicle vehicle)
        {
            m_AllVehiclesInGarage.Remove(vehicle);
        }
    }
}
