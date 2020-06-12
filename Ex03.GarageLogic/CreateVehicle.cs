using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CreateVehicle
    {
        internal const float m_CarMaxFuel = 60f;
        internal const byte m_CarNumOfWheels = 4;
        internal const float m_CarMaxBattery = 2.1f;
        internal const float m_CarMaxWheelPressure = 32;
        internal const float m_MotorcycleMaxFuel = 7f;
        internal const byte m_MotorcycleNumOfWheels = 2;
        internal const float m_MotorcycleMaxBattery = 1.2f;
        internal const float m_MotorcycleMaxWheelPressure = 30;
        internal const float m_TruckMaxFuel = 120f;
        internal const byte m_TruckNumOfWheels = 16;
        internal const float m_TruckMaxWheelPressure = 28;


        public static Car CreateCar(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, eCarColors i_Color, eNumOfDoors i_NumOfDoors,
            string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            Car car = new Car(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_CarMaxFuel, m_CarNumOfWheels, i_Color, i_NumOfDoors, i_Owner);
            GenerateWheels(car, m_CarMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return car;
        }

        public static ElectricCar CreateElectricCar(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, eCarColors i_Color, eNumOfDoors i_NumOfDoors,
            string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            ElectricCar electricCar = new ElectricCar(i_VehicleModel, i_LicencePlate, i_BatteryLeft, m_CarMaxBattery, i_Color, m_CarNumOfWheels, i_NumOfDoors, i_Owner);
            GenerateWheels(electricCar, m_CarMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return electricCar;
        }

        public static Motorcycle CreateMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, eMotorcycleLicenceType i_LicenceType,
            int i_EngineVolume, string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            Motorcycle motorcycle = new Motorcycle(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_MotorcycleMaxFuel, m_MotorcycleNumOfWheels, i_LicenceType, i_EngineVolume, i_Owner);
            GenerateWheels(motorcycle, m_MotorcycleMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return motorcycle;
        }

        public static ElectricMotorcycle CreateElectricMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, eMotorcycleLicenceType i_LicenceType,
            int i_EngineVolume, string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle(i_VehicleModel, i_LicencePlate, i_BatteryLeft, m_MotorcycleMaxBattery, m_MotorcycleNumOfWheels,
                i_LicenceType, i_EngineVolume, i_Owner);
            GenerateWheels(electricMotorcycle, m_MotorcycleMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return electricMotorcycle;
        }

        public static Truck CreateTruck(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, bool i_CarryDangerousMaterials,
            float i_CargoVolume, string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            Truck truck = new Truck(i_VehicleModel, i_LicencePlate, i_FuelLeft, m_TruckMaxFuel, m_TruckNumOfWheels, i_CarryDangerousMaterials, i_CargoVolume, i_Owner);
            GenerateWheels(truck, m_TruckMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return truck;
        }

        public static void GenerateWheels(Vehicle i_Vehicle, float i_MaxAirPressure, string i_WheelMaker, float i_CurrentAirPressure)
        {
            if(i_MaxAirPressure < i_CurrentAirPressure)
            {
                throw new ValueOutOfRangeException("Current air pressure is bigger than max air pressure");
            }
            for(int i = 0; i < i_Vehicle.m_Wheels.Length; i++)
            {
                Wheel newWheel = new Wheel(i_WheelMaker, i_CurrentAirPressure, i_MaxAirPressure);
                i_Vehicle.m_Wheels[i] = newWheel;
            }
        }
    }
}
