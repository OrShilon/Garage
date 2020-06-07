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
        private static readonly Dictionary<string, string> VehiclesInGarageStatus = new Dictionary<string, string>();
        const int notInGarage = -1;
        const string m_InRepairStatus = "In repair";
        const float m_CarMaxAirPressure = 32f;
        const float m_MotorcycleMaxAirPressure = 30f;
        const float m_TruckMaxAirPressure = 28f;

        public static void AddVehicleToGarage(string i_VehicleLicencePlate)
        {
            VehiclesInGarageStatus.Add(i_VehicleLicencePlate, m_InRepairStatus);
        }

        public static void RemoveVehicleToGarage(string i_VehicleLicencePlate)
        {
            VehiclesInGarageStatus.Remove(i_VehicleLicencePlate);
        }
        public static Dictionary<string, string> VehiclesStatusDictionary
        {
            get
            {
                return VehiclesInGarageStatus;
            }
        }

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

        /*public  static void GenerateWheels(string i_WheelMaker, byte i_CurrentAirPressure)
        {
            int m_NumOfWheels = 4; //need to remove, just to fix the for for now
            for (int i = 0; i < m_NumOfWheels; i++)
            {
                Wheel newWheel = new Wheel(i_WheelMaker, i_CurrentAirPressure, m_CarMaxAirPressure);
                m_Wheels[i] = newWheel;
            }
        }*/

        public static void FillAir(string i_LicensePlateNumber)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);
            if (vehicleLocation == notInGarage)
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
            if (vehicleLocation == notInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }
            ElectricVehicle customerVehicle = m_AllVehiclesInGarage[vehicleLocation] as ElectricVehicle;
            if (customerVehicle == null)
            {
                throw new ArgumentException("NOT a battery vehicle");
            }
            float newBatteryHoursLeft = customerVehicle.m_BatteryLeft + i_HowMuchToFill;
            if (newBatteryHoursLeft > customerVehicle.m_BatteryHourCapacity)
            {
                throw new ValueOutOfRangeException("refuel quantity is too large");
            }
           
            customerVehicle.m_BatteryLeft = newBatteryHoursLeft;//צריך לבדוק האם לעשות GET וSET m_FuelOrBatteryLeftל
        }
        //לא בודק בכלל אם רכב נמצא במוסך צריך לטפל בזה לפני. כאן במקרה של חריגה/סוג דלק לא נכון נזרקת אקספשיין
        public static void Refuel(string i_LicensePlateNumber, byte i_HowMuchToFill, eFuelTypes i_FuelType)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);
            if(vehicleLocation == notInGarage)
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
            int vehicleLocation = notInGarage;
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
