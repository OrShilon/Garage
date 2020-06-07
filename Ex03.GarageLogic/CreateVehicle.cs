﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CreateVehicle
    {
        internal const float m_CarMaxFuel = 2.1f;
        internal const byte m_CarNumOfWheels = 4;
        internal const float m_CarMaxBattery = 2.1f;
        internal const float m_MotorcycleMaxFuel = 7f;
        internal const byte m_MotorcycleNumOfWheels = 2;
        internal const float m_MotorcycleMaxBattery = 1.2f;
        internal const float m_TruckMaxFuel = 120f;
        internal const byte m_TruckNumOfWheels = 16;

        public static Car CreateCar(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_Color, byte i_NumOfDoors)
        {
            Car car = new Car(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_CarMaxFuel, m_CarNumOfWheels, i_Color, i_NumOfDoors);
            return car;
        }

        public static ElectricCar CreateElectricCar(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, string i_Color, int i_NumOfDoors)
        {
            ElectricCar electricCar = new ElectricCar(i_VehicleModel, i_LicencePlate, i_BatteryLeft, m_CarMaxBattery, i_Color, m_CarNumOfWheels, i_NumOfDoors);
            return electricCar;
        }

        public static Motorcycle CreateMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_LicenseType, int i_EngineVolume)
        {
            Motorcycle motorcycle = new Motorcycle(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_MotorcycleMaxFuel, m_MotorcycleNumOfWheels, i_LicenseType, i_EngineVolume);
            return motorcycle;
        }

        public static ElectricMotorcycle CreateElectricMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, string i_LicenseType, int i_EngineVolume)
        {
            ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle(i_VehicleModel, i_LicencePlate, i_BatteryLeft, m_MotorcycleMaxBattery, m_MotorcycleNumOfWheels, i_LicenseType);
            return electricMotorcycle;
        }

        public static Truck CreateTruck(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, bool i_CarryDangerousMaterials, float i_CargoVolume)
        {
            Truck truck = new Truck(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_TruckMaxFuel, m_TruckNumOfWheels, i_CarryDangerousMaterials, i_CargoVolume);
            return truck;
        }
    }
}
