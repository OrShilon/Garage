using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car("qwe", "qwe", 10, 60, 4, "Red", 3, new VehicleOwner("or", "123456"));
            ElectricMotorcycle electricMotor = new ElectricMotorcycle("asd", "asd", 0.8f, 1.2f, 2,"A", 100, new VehicleOwner("amit", "654321"));
            Truck truck = new Truck("zxc", "zxc", 50f, 120f, 16, true, 60f, new VehicleOwner("mitzi", "09876567"));
            GarageManager.AddVehicleToGarage(car);
            GarageManager.AddVehicleToGarage(electricMotor);
            GarageManager.AddVehicleToGarage(truck);
            UIManager.Welcome();
            Console.ReadLine();
        }
    }
}
