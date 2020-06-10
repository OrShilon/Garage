using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class GarageManager
    {
        private static List<Vehicle> m_AllVehiclesInGarage = new List<Vehicle>();
        private static readonly Dictionary<string, eVehicleStatus> VehiclesInGarageStatus = new Dictionary<string, eVehicleStatus>();
        public const int k_NotInGarage = -1;
        public const float m_CarMaxAirPressure = 32f;
        public const float m_MotorcycleMaxAirPressure = 30f;
        public const float m_TruckMaxAirPressure = 28f;

        public static void AddVehicleToGarage(Vehicle i_Vehicle)
        {
            VehiclesInGarageStatus.Add(i_Vehicle.m_LicencePlate, eVehicleStatus.InRepair);
            m_AllVehiclesInGarage.Add(i_Vehicle);
        }


        public static Dictionary<string, eVehicleStatus> VehiclesStatusDictionary
        {
            get
            {
                return VehiclesInGarageStatus;
            }
        }

        public static void PrintVehiclesInGarage()
        {
            foreach (KeyValuePair<string, eVehicleStatus> pair in VehiclesInGarageStatus)
            {
                Console.WriteLine("License plate number: " + pair.Key + ", status: " + pair.Value);
            }
        }

        public static void PrintVehiclesInGarage(eVehicleStatus i_status)
        {
            foreach (KeyValuePair<string, eVehicleStatus> pair in VehiclesInGarageStatus)
            {
                if (pair.Value.Equals(i_status))
                {
                    Console.WriteLine("License plate number: " + pair.Key + ", status: " + pair.Value);
                }
            }
        }

        public static void ChangeVehicleStatus(string i_LicensePlateNumber, eVehicleStatus i_NewVehicleStatus)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);
            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }
            VehiclesInGarageStatus[i_LicensePlateNumber] = i_NewVehicleStatus;
        }

        public static void PrintVehicleDetails(string i_LicencePlate)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicencePlate);

            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }
            else
            {
                Console.WriteLine(m_AllVehiclesInGarage[vehicleLocation].ToString() + Environment.NewLine + "Vehicle status is: " + VehiclesInGarageStatus[i_LicencePlate]);
            }
        }

        public static void FillAir(string i_LicensePlateNumber)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);
            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }
            Vehicle customerVehicle = m_AllVehiclesInGarage[vehicleLocation];
            float maxAirPressure = customerVehicle.m_Wheels[0].m_MaxAirPressure;
            for (int i = 0; i < customerVehicle.m_Wheels.Length; i++)
            {
                customerVehicle.m_Wheels[i].m_CurrentAirPressure = maxAirPressure;
            }
        }

        public static void FillBattery(string i_LicensePlateNumber, float i_HowMuchToFill)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);
            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }

            ElectricVehicle customerVehicle = m_AllVehiclesInGarage[vehicleLocation] as ElectricVehicle;
            if (customerVehicle == null)
            {
                throw new ArgumentException("NOT a battery vehicle");
            }

            float newBatterLeft = customerVehicle.m_BatteryLeft + i_HowMuchToFill;
            if (newBatterLeft > customerVehicle.m_BatteryHourCapacity)
            {
                throw new ValueOutOfRangeException("battery hours quantity is too large");
            }
           
            customerVehicle.m_BatteryLeft = newBatterLeft;//צריך לבדוק האם לעשות GET וSET m_FuelOrBatteryLeftל
        }

        public static void Refuel(string i_LicensePlateNumber, float i_HowMuchToFill, eFuelTypes i_FuelType)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);
            if(vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }
            FuelVehicle customerVehicle = m_AllVehiclesInGarage[vehicleLocation] as FuelVehicle;
            if(customerVehicle == null)
            {
                throw new ArgumentException("NOT a fuel vehicle");
            }
            float newFuelLeft = customerVehicle.m_FuelLeft + i_HowMuchToFill;
            if (newFuelLeft > customerVehicle.m_FuelTankCapacity)
            {
                throw new ValueOutOfRangeException("refuel quantity is too large");
            }
            if(i_FuelType != customerVehicle.m_FuelType)//צריך לדרוס פונקצית == וגם !=
            {
                throw new ArgumentException("fuel type is NOT valid");
            }
            customerVehicle.m_FuelLeft = newFuelLeft;//צריך לבדוק האם לעשות GET וSET m_FuelOrBatteryLeftל

        }
        /**
         * checks if the given vehicle is in the garage if so returns its location in the list
         * if not returns -1
         **/ 
        public static int CheckIfVehicleInGarage(string i_LicensePlateNumber)
        {
            int vehicleLocation = k_NotInGarage;
            for(int i = 0; i < m_AllVehiclesInGarage.Count; i++)
            {
                if (m_AllVehiclesInGarage[i].m_LicencePlate == i_LicensePlateNumber)
                {
                    vehicleLocation = i;
                }
            }
            return vehicleLocation;
        }
    }
}
