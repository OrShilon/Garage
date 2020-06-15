using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CreateVehicle
    {
        private const float k_CarMaxFuel = 60f;
        private const byte k_CarNumOfWheels = 4;
        private const float k_CarMaxBattery = 2.1f;
        private const float k_CarMaxWheelPressure = 32;
        private const float k_MotorcycleMaxFuel = 7f;
        private const byte k_MotorcycleNumOfWheels = 2;
        private const float k_MotorcycleMaxBattery = 1.2f;
        private const float k_MotorcycleMaxWheelPressure = 30;
        private const float k_TruckMaxFuel = 120f;
        private const byte k_TruckNumOfWheels = 16;
        private const float k_TruckMaxWheelPressure = 28;


        public static Car CreateCar(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, eCarColors i_Color, eNumOfDoors i_NumOfDoors,
            string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            Car car = new Car(i_VehicleModel, i_LicencePlate, i_FuelLeft, k_CarMaxFuel, k_CarNumOfWheels, i_Color, i_NumOfDoors, i_Owner);
            GenerateWheels(car, k_CarMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return car;
        }

        public static ElectricCar CreateElectricCar(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, eCarColors i_Color, eNumOfDoors i_NumOfDoors,
            string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            ElectricCar electricCar = new ElectricCar(i_VehicleModel, i_LicencePlate, i_BatteryLeft, k_CarMaxBattery, i_Color, k_CarNumOfWheels, i_NumOfDoors, i_Owner);
            GenerateWheels(electricCar, k_CarMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return electricCar;
        }

        public static Motorcycle CreateMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, eMotorcycleLicenceType i_LicenceType,
            int i_EngineVolume, string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            Motorcycle motorcycle = new Motorcycle(i_VehicleModel, i_LicencePlate, i_FuelLeft, k_MotorcycleMaxFuel, k_MotorcycleNumOfWheels, i_LicenceType, i_EngineVolume, i_Owner);
            GenerateWheels(motorcycle, k_MotorcycleMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return motorcycle;
        }

        public static ElectricMotorcycle CreateElectricMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, eMotorcycleLicenceType i_LicenceType,
            int i_EngineVolume, string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle(i_VehicleModel, i_LicencePlate, i_BatteryLeft, k_MotorcycleMaxBattery, k_MotorcycleNumOfWheels,
                i_LicenceType, i_EngineVolume, i_Owner);
            GenerateWheels(electricMotorcycle, k_MotorcycleMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return electricMotorcycle;
        }

        public static Truck CreateTruck(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, bool i_CarryDangerousMaterials,
            float i_CargoVolume, string i_WheelMaker, float i_WheelCurrentPressure, VehicleOwner i_Owner)
        {
            Truck truck = new Truck(i_VehicleModel, i_LicencePlate, i_FuelLeft, k_TruckMaxFuel, k_TruckNumOfWheels, i_CarryDangerousMaterials, i_CargoVolume, i_Owner);
            GenerateWheels(truck, k_TruckMaxWheelPressure, i_WheelMaker, i_WheelCurrentPressure);
            return truck;
        }

        public static void GenerateWheels(Vehicle i_Vehicle, float i_MaxAirPressure, string i_WheelMaker, float i_CurrentAirPressure)
        {
            if(i_MaxAirPressure < i_CurrentAirPressure)
            {
                throw new ValueOutOfRangeException("Current air pressure is bigger than max air pressure");
            }
            for(int i = 0; i < i_Vehicle.Wheels.Length; i++)
            {
                i_Vehicle.Wheels[i] = new Wheel(i_WheelMaker, i_CurrentAirPressure, i_MaxAirPressure);
            }
        }
    }
}
