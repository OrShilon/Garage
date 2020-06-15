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
        private static readonly List<Vehicle> m_AllVehiclesInGarage = new List<Vehicle>();
        private static readonly Dictionary<string, eVehicleStatus> VehiclesInGarageStatus = new Dictionary<string, eVehicleStatus>();
        public const int k_NotInGarage = -1;
        public const int k_Zero = 0;
        public const float m_CarMaxAirPressure = 32f;
        public const float m_MotorcycleMaxAirPressure = 30f;
        public const float m_TruckMaxAirPressure = 28f;
        private static int m_CountInRepair = 0;
        private static int m_CountFixed = 0;
        private static int m_CountPayed = 0;

        public static void AddVehicleToGarage(Vehicle i_Vehicle)
        {
            bool isInGarage = false;
            foreach(Vehicle vehicle in m_AllVehiclesInGarage)
            {
                if (vehicle.Equals(i_Vehicle))
                {
                    Console.WriteLine("Your vehicle has already been registered before.");
                    ChangeVehicleStatus(i_Vehicle.m_LicencePlate, eVehicleStatus.InRepair);
                    isInGarage = true;
                    break;
                }
            }
            if (!isInGarage)
            {
                VehiclesInGarageStatus.Add(i_Vehicle.m_LicencePlate, eVehicleStatus.InRepair);
                m_AllVehiclesInGarage.Add(i_Vehicle);
                m_CountInRepair++;
            }
     
        }


        public static Dictionary<string, eVehicleStatus> VehiclesStatusDictionary
        {
            get
            {
                return VehiclesInGarageStatus;
            }
        }
        public static List<Vehicle> VehiclesInGarage
        {
            get
            {
                return m_AllVehiclesInGarage;
            }
        }


        private static int ConvertEnunStatusToInt(eVehicleStatus i_VehicleStatus)
        {
            int convertedStatus;
            switch (i_VehicleStatus)
            {
                case eVehicleStatus.InRepair:
                    convertedStatus = m_CountInRepair;
                    break;    
                case eVehicleStatus.Fixed:
                    convertedStatus = m_CountFixed;
                    break;           
                case eVehicleStatus.Payed:
                    convertedStatus = m_CountPayed;
                    break;
                default:
                    convertedStatus = k_Zero;
                    break;
            }

            return convertedStatus;
        }
        public static void PrintVehiclesInGarage()
        {
            if (VehiclesInGarageStatus.Count == k_Zero)
            {
                Console.WriteLine("Sorry but there are no vehicles in the garage currently");
            }
            else
            {
                foreach (KeyValuePair<string, eVehicleStatus> vehicle in VehiclesInGarageStatus)
                {
                    Console.WriteLine("License plate number: " + vehicle.Key + ", status: " + vehicle.Value);
                }
            }
        }

        public static void PrintVehiclesInGarage(eVehicleStatus i_status)
        {
            if (ConvertEnunStatusToInt(i_status) < 1)
            {
                Console.WriteLine("Sorry but there are no vehicles with this status in the garage currently");
            }
            else
            {
                foreach (KeyValuePair<string, eVehicleStatus> vehicle in VehiclesInGarageStatus)
                {
                    if (vehicle.Value.Equals(i_status))
                    {
                        Console.WriteLine("License plate number: " + vehicle.Key + ", status: " + vehicle.Value);
                    }
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
            eVehicleStatus oldStatus = VehiclesInGarageStatus[i_LicensePlateNumber];
            if (!oldStatus.Equals(i_NewVehicleStatus))
            {
                switch (oldStatus)
                {
                    case eVehicleStatus.InRepair:
                        m_CountInRepair--;
                        break;
                    case eVehicleStatus.Fixed:
                        m_CountFixed--;
                        break;
                    case eVehicleStatus.Payed:
                        m_CountPayed--;
                        break;
                }

                switch (i_NewVehicleStatus)
                {
                    case eVehicleStatus.InRepair:
                        m_CountInRepair++;
                        break;
                    case eVehicleStatus.Fixed:
                        m_CountFixed++;
                        break;
                    case eVehicleStatus.Payed:
                        m_CountPayed++;
                        break;
                }
                VehiclesInGarageStatus[i_LicensePlateNumber] = i_NewVehicleStatus;
            } 
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
            if(customerVehicle == null)
            {
                throw new ArgumentException("NOT an Electric vehicle");
            }

            float newBatteryLeft = customerVehicle.m_BatteryLeft + i_HowMuchToFill;
            float calculatedMaximumBatteryHours = customerVehicle.m_BatteryHourCapacity - customerVehicle.m_BatteryLeft;
            if (newBatteryLeft > customerVehicle.m_BatteryHourCapacity)
            {
                throw new ValueOutOfRangeException(calculatedMaximumBatteryHours, k_Zero);
            }
           
            customerVehicle.m_BatteryLeft = newBatteryLeft;//צריך לבדוק האם לעשות GET וSET m_FuelOrBatteryLeftל
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
            float calculatedMaximumFuelCapacity = customerVehicle.m_FuelTankCapacity - customerVehicle.m_FuelLeft;
            if (newFuelLeft > customerVehicle.m_FuelTankCapacity)
            {
                throw new ValueOutOfRangeException(calculatedMaximumFuelCapacity, k_Zero);
            }
            if (i_FuelType != customerVehicle.m_FuelType)//צריך לדרוס פונקצית == וגם !=
            {
                throw new ArgumentException("fuel type is NOT valid");
            }
            customerVehicle.m_FuelLeft = newFuelLeft;//צריך לבדוק האם לעשות GET וSET m_FuelOrBatteryLeftל

        }

         //checks if the given vehicle is in the garage. If so returns its location in the list, else not returns -1
        public static int CheckIfVehicleInGarage(string i_LicensePlateNumber)
        {
            int vehicleLocation = k_NotInGarage;

            for(int i = 0; i < m_AllVehiclesInGarage.Count; i++)
            {
                if (m_AllVehiclesInGarage[i].m_LicencePlate == i_LicensePlateNumber)
                {
                    vehicleLocation = i;
                    break;
                }
            }
            return vehicleLocation;
        }
    }
}
