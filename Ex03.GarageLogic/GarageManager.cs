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

        public static void FillAir(string i_LicensePlateNumber)
        {
            foreach (Vehicle vehicle1 in m_AllVehiclesInGarage)// need to make an iteration function(find the given vehicle in list)
            {
                if (vehicle1.m_LicencePlate == i_LicensePlateNumber && vehicle1 is FuelVehicle)
                {
                    for (int i = 0; i < vehicle1.m_Wheels.Length; i++)
                        vehicle1.m_Wheels[i].m_CurrentAirPressure = vehicle1.m_Wheels[i].m_MaxAirPressure;
                }
            }
        }

        public static void FillBattery(string i_LicensePlateNumber)
        {
            foreach (Vehicle vehicle1 in m_AllVehiclesInGarage)// need to make an iteration function(find the given vehicle in list)
            {
                if (vehicle1.m_LicencePlate == i_LicensePlateNumber && !(vehicle1 is FuelVehicle))
                {
                    vehicle1.m_FuelOrBatteryLeft = vehicle1.m_MaxFuelOrBattery;
                }
            }
        }

        public static void Refuel(string i_LicensePlateNumber)
        {
            foreach (Vehicle vehicle1 in m_AllVehiclesInGarage)// need to make an iteration function(find the given vehicle in list)
            {
                if (vehicle1.m_LicencePlate == i_LicensePlateNumber && (vehicle1 is FuelVehicle))
                {
                    vehicle1.m_FuelOrBatteryLeft = vehicle1.m_MaxFuelOrBattery;
                }
            }
        }

        public static bool CheckIfVehicleInGarage(string i_LicensePlateNumber)
        {
            bool VehicleIsInGarage = false;
            foreach (Vehicle vehicle1 in m_AllVehiclesInGarage)
            {
                if (vehicle1.m_LicencePlate == i_LicensePlateNumber)
                {
                    VehicleIsInGarage = true;
                }
            }
            return VehicleIsInGarage;
        }
    }
}
