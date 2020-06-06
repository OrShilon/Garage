using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class UIManager
    {
        private const string WelcomeMessage = "Hello and welcome to our garage. What is the purpose of your visit?";

        private static void PrintVehiclesInGarage()
        {
            List<Vehicle> carsList = GarageManager.CarsList;
            if (carsList.Any())
            {
                Console.WriteLine("Vehicles in the garage: ");
                foreach (GarageLogic.Vehicle vehicle in carsList)
                {
                    Console.WriteLine(vehicle.m_LicencePlate);
                }
            }
            else
            {
                Console.WriteLine("No Vehicles in the garage yet.");
                //לטפל בזה שהרשימת רכבים ריקה, כלומר עוד לא נכנסו רכבים למוסך
            }
        }

        public static void Welcome()
        {
            Console.WriteLine(WelcomeMessage);
            Console.WriteLine();
        }
        //DisplayGarageLicensePlates,
        //ChangeCarStatus,
        //FillAir,
        //Refuel,
        //Recharge,
        //DisplayVehicleDetails

        public static void GarageOptionsForCustomer()
        {
            string messege = string.Format(@"1. I would like to check in a new vehicle
2. Display the current license plates that are in the garage
3. Change my vehicle status
4. Fill air in my vehicle wheels
5. Refuel my vehicle
6. Recharge my vehicle
7. Display my vehicle details
Type in the corresponding number to your visit purpose please.");
            Console.Write(messege);
        }

        public static void AddNewVehicle()
        {
            string vehicleType;
            int ValidvehicleType;
            eVehicles userVehicle;
            string Messege = string.Format(@"What is the Type of your Vehicle?
1. Regular car
2. Electric Car
3. Regular Motorcycle
4. Electric Motorcycle
5. Truck
Type in the corresponding number to your vehicle please.");
            Console.WriteLine(Messege);
            vehicleType = Console.ReadLine();
            while(!IsValidVehicleChoice(vehicleType))
            {
                Console.WriteLine("Invalid Input.");
                Console.WriteLine(Messege);
                vehicleType = Console.ReadLine();
            }

            int.TryParse(vehicleType, out ValidvehicleType);
            userVehicle = (eVehicles)ValidvehicleType;

            switch (userVehicle)
            {
                case eVehicles.Car:
                    CreateCar();
                    break;
                case eVehicles.ElectricCar:
                    break;
                case eVehicles.Motorcycle:
                    break;
                case eVehicles.ElectricMotorcycle:
                    break;
                case eVehicles.Truck:
                    break;
                default:
                    break;
            }

        }
        private static bool IsValidVehicleChoice(string i_UserChoice)
        {
            bool isValidInput = false;

            if(i_UserChoice.Length != 1)
            {
                isValidInput = false;
            }
            else
            {
                //צריך לשנות את '0' ו '6' להיות קונסט כי אי אפשר לשים רק מספר
                if(i_UserChoice[0] > '0' && i_UserChoice[0] < '6')
                {
                    isValidInput = true;
                }
            }

            return isValidInput;
        }

        // there is another method name CreateCar in GarageLogic! need to check if it is ok
        private static void CreateCar()
        {
            string carModel;
            string licencePlate;
            string fuelLeft; //need to change type to float later on
            string color;
            string numOfDoors; // need to change type to byte later on

            Console.WriteLine("Please enter your Car's model:");
            carModel = Console.ReadLine();
            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine(); //need to make isvalidinput for this line!
            Console.WriteLine("Please enter how much fuel left in your car:");
            fuelLeft = Console.ReadLine(); //need to make sure it is float, and then convert type to float!!
            Console.WriteLine("Please enter the color of your car:");
            color = Console.ReadLine(); //need to verify the color
            Console.WriteLine("Please enter the number of doors your car has:");
            numOfDoors = Console.ReadLine(); // need to varify this input

            //CreateVehicle.CreateCar(carModel, licencePlate, (float)fuelLeft, color, (byte)numOfDoors);
        }
    }

}
