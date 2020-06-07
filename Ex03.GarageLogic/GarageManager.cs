﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class GarageManager
    {
        private static List<Vehicle> m_AllVehiclesInGarage = new List<Vehicle>();
        const int k_NotInGarage = -1;

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
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);
            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }
            Vehicle customerVehicle = m_AllVehiclesInGarage[vehicleLocation];
            byte maxAirPressure = customerVehicle.m_Wheels[0].m_MaxAirPressure;
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
            BatteryVehicle customerVehicle = m_AllVehiclesInGarage[vehicleLocation] as BatteryVehicle;
            if (customerVehicle == null)
            {
                throw new ArgumentException("NOT a battery vehicle");
            }
            float newBatteryHoursLeft = customerVehicle.m_FuelOrBatteryLeft + i_HowMuchToFill;
            if (newBatteryHoursLeft > customerVehicle.m_MaxFuelOrBattery)
            {
                throw new ValueOutOfRangeException("refuel quantity is too large");
            }
           
            customerVehicle.m_FuelOrBatteryLeft = newBatteryHoursLeft;//צריך לבדוק האם לעשות GET וSET m_FuelOrBatteryLeftל
        }
        //לא בודק בכלל אם רכב נמצא במוסך צריך לטפל בזה לפני. כאן במקרה של חריגה/סוג דלק לא נכון נזרקת אקספשיין
        public static void Refuel(string i_LicensePlateNumber, byte i_HowMuchToFill, eFuelTypes i_FuelType)
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
            float newFuelLeft = customerVehicle.m_FuelOrBatteryLeft + i_HowMuchToFill;
            if (newFuelLeft > customerVehicle.m_MaxFuelOrBattery)
            {
                throw new ValueOutOfRangeException("refuel quantity is too large");
            }
            if(i_FuelType != customerVehicle.m_FuelType)//צריך לדרוס פונקצית == וגם !=
            {
                throw new ArgumentException("fuel type is NOT valid");
            }
            customerVehicle.m_FuelOrBatteryLeft = newFuelLeft;//צריך לבדוק האם לעשות GET וSET m_FuelOrBatteryLeftל

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
