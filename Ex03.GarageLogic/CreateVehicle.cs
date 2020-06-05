using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class CreateVehicle
    {
        class CreateVehicle
        {
            public const float k_CapacityOfFuelEngineCar = 38f;
            public const float k_CapacityTimeOfElectricEngineCar = 3.3f;
            public const float k_CapacityOfFuelEngineMotorcycle = 7.2f;
            public const float k_CapacityTimeOfElectricEngineMotorcycle = 1.9f;
            public const float k_CapacityOfFuelEngineTruck = 135f;
            public const float k_CarWheelMaxAirPressure = 30f;
            public const float k_MotorcycleWheelMaxAirPressure = 31f;
            public const float k_TruckWheelMaxAirPressure = 28f;
            public const int k_NumOfCarWheels = 4;
            public const int k_NumOfMotorcycleWheels = 2;
            public const int k_NumOfTruckWheels = 16;

            private static Car CreateCar(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_Color, int i_NumOfDoors)
            {
                Car car = new Car(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_Color, i_NumOfDoors);
                return car;
            }

            public static ElectricCar CreateElectricCar(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_Color, int i_NumOfDoors)
            {
                ElectricCar electricCar = new ElectricCar(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_Color, i_NumOfDoors);
                return electricCar;
            }

            public static Motorcycle CreateMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, string i_LicenseType, int i_EngineVolume)
            {
                Motorcycle motorcycle = new Motorcycle(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_LicenseType, i_EngineVolume);
                return motorcycle;
            }

            public static ElectricMotorcycle reateElectricMotorcycle(string i_VehicleModel, string i_LicencePlate, float i_BatteryLeft, string i_LicenseType, int i_EngineVolume)
            {
                ElectricMotorcycle electricMotorcycle = new ElectricMotorcycle(i_VehicleModel, i_LicencePlate, i_BatteryLeft, i_LicencePlate, i_EngineVolume);
                return electricMotorcycle;
            }

            private static Truck createTruck(string i_VehicleModel, string i_LicencePlate, float i_FuelLeft, bool i_CarryDangerousMaterials, float i_CargoVolume)
            {
                Truck truck = new Truck(i_VehicleModel, i_LicencePlate, i_FuelLeft, i_CarryDangerousMaterials, i_CargoVolume);
                return truck;
            }
        }
    }
}
