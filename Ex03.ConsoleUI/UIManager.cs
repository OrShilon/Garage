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
            List<Vehicle> vehiclesList = GarageManager.VehiclesList;
            if (vehiclesList.Any())
            {
                Console.WriteLine("Vehicles in the garage: ");
                foreach (Vehicle vehicle in vehiclesList)
                {
                    Console.WriteLine(vehicle.m_LicencePlate); //להעביר את המימוש של הלולאה הזאת לתןך הגאראז' מנג'ר!
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
            AddNewVehicle();
        }

        public static void AddNewVehicle()
        {
            string vehicleType;
            int ValidvehicleType;
            eVehicles userVehicle;
            string vehicleModel = string.Empty;
            string licencePlate = string.Empty;
            float energyLeft = 0f; //can be fuel or battery
            bool isElectricVehicle;

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
            userVehicle = (eVehicles) ValidvehicleType;

            switch (userVehicle)
            {
                case eVehicles.Car:
                    isElectricVehicle = false;
                    CreateCar(isElectricVehicle, vehicleModel, licencePlate, energyLeft);
                    break;
                case eVehicles.ElectricCar:
                    isElectricVehicle = true;
                    CreateCar(isElectricVehicle, vehicleModel, licencePlate, energyLeft);
                    break;
                case eVehicles.Motorcycle:
                    break;
                case eVehicles.ElectricMotorcycle:
                    break;
                case eVehicles.Truck:
                    break;
                default:
                    //invalid input, need to handle.......
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
        private static void CreateCar(bool i_IsElectric, string i_CarModel, string i_LicencePlate, float i_EnergyLeft)
        {
            string energyLeftInput; //can be fuel or battery
            string color;
            string numOfDoorsInput;
            byte numOfDoors;

            Console.WriteLine("Please enter your car's model:");
            i_CarModel = Console.ReadLine();
            while(!IsValidVehicleModel(i_CarModel))
            {
                Console.WriteLine("Not a valid input. Please enter your car's model:");
                i_CarModel = Console.ReadLine();
            }
            Console.WriteLine("please enter your licence plate:");
            i_LicencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            Console.WriteLine("Please enter how much {0} left in your car:", i_IsElectric ? "battery" : "fuel");
            energyLeftInput = Console.ReadLine();
            while (!IsValidFuelInput(energyLeftInput, out i_EnergyLeft))
            {
                Console.WriteLine("Not a valid input. Please enter how much {0} left in your car:", i_IsElectric ? "battery" : "fuel");
                energyLeftInput = Console.ReadLine();
            }
            Console.WriteLine("Please enter the color of your car:");
            color = Console.ReadLine();
            while (!IsValidCarColor(color))
            {
                Console.WriteLine("Not a valid input. Please enter the color of your car:");
                color = Console.ReadLine();
            }
            Console.WriteLine("Please enter the number of doors your car has:");
            numOfDoorsInput = Console.ReadLine();
            while (!IsValidNumOfDoorsInput(numOfDoorsInput, out numOfDoors))
            {
                Console.WriteLine("Not a valid input. Please enter the number of doors your car has:");
                numOfDoorsInput = Console.ReadLine();
            }

            if (i_IsElectric)
            {
                ElectricCar newElectricCar = CreateVehicle.CreateElectricCar(i_CarModel, i_LicencePlate, i_EnergyLeft, color, numOfDoors);
                GarageManager.AddVehicleToGarage(newElectricCar);
            }
            else
            {
                Car newCar = CreateVehicle.CreateCar(i_CarModel, i_LicencePlate, i_EnergyLeft, color, numOfDoors);
                GarageManager.AddVehicleToGarage(newCar);
            }
        }

        private static void CreateMotorcycle(bool i_IsElectric, string i_MotorcycleModel, string i_LicencePlate, float i_EnergyLeft)
        {
            string energyLeftInput; //can be fuel or battery
            string licenceTypeInput;
            string validLicenceType = string.Empty;
            string engineVolume = string.Empty;
            int validEngineVolume;

            Console.WriteLine("Please enter your motorcycle's model:");
            i_MotorcycleModel = Console.ReadLine();
            while (!IsValidVehicleModel(i_MotorcycleModel))
            {
                Console.WriteLine("Not a valid input. Please enter your motorcycle's model:");
                i_MotorcycleModel = Console.ReadLine();
            }
            Console.WriteLine("please enter your licence plate:");
            i_LicencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            Console.WriteLine("Please enter how much {0} left in your motorcycle:", i_IsElectric ? "battery" : "fuel");
            energyLeftInput = Console.ReadLine();
            while (!IsValidFuelInput(energyLeftInput, out i_EnergyLeft))
            {
                Console.WriteLine("Not a valid input. Please enter how much {0} left in your motorcycle:", i_IsElectric ? "battery" : "fuel");
                energyLeftInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter your licence type:");
            licenceTypeInput = Console.ReadLine();
            while (!IsValidMotorcycleLicence(licenceTypeInput))
            {
                Console.WriteLine("Not a valid input. Please enter the color of your car:");
                licenceTypeInput = Console.ReadLine();
            }

            switch(licenceTypeInput)
            {
                case "1":
                    validLicenceType = "A";
                    break;
                case "2":
                    validLicenceType = "A1";
                    break;
                case "3":
                    validLicenceType = "AA";
                    break;
                case "4":
                    validLicenceType = "B";
                    break;
                default:
                    //exeption??? ----> need to check what to do here, because we wiill never get here!
                    break;

            }

            Console.WriteLine("Please enter your engine's volume (in Cc):");
            engineVolume = Console.ReadLine();
            while (!IsValidEngineVolume(engineVolume, out validEngineVolume))
            {
                Console.WriteLine("Not a valid input. Please enter the number of doors your car has:");
                engineVolume = Console.ReadLine();
            }

            if (i_IsElectric)
            {
                ElectricMotorcycle newElectricMotorcycle = CreateVehicle.CreateElectricMotorcycle(i_MotorcycleModel, i_LicencePlate, i_EnergyLeft, validLicenceType, validEngineVolume);
                GarageManager.AddVehicleToGarage(newElectricMotorcycle);
            }
            else
            {
                Motorcycle newMotorcycle = CreateVehicle.CreateMotorcycle(i_MotorcycleModel, i_LicencePlate, i_EnergyLeft, validLicenceType, validEngineVolume);
                GarageManager.AddVehicleToGarage(newMotorcycle);
            }
        }

        private static bool IsValidVehicleModel(string i_VehicleModel)
        {
            return !i_VehicleModel.Equals(string.Empty);
        }

        private static bool IsValidFuelInput(string i_FuelLeftUnput, out float o_FuelLeft)
        {
            return float.TryParse(i_FuelLeftUnput, out o_FuelLeft);
        }
        private static bool IsValidCarColor(string i_Color)
        {
            //need to change return statement to enum
            return i_Color.Equals("Red") || i_Color.Equals("White") || i_Color.Equals("Black") || i_Color.Equals("Grey");
        }
        private static bool IsValidNumOfDoorsInput(string i_NumOfDoorsInput, out byte o_NumOfDoors)
        {
            byte.TryParse(i_NumOfDoorsInput, out o_NumOfDoors);
            return o_NumOfDoors > 1 && o_NumOfDoors < 6; //need to change 1 and 6 to const
        }

        private static bool IsValidMotorcycleLicence(string i_MotorcycleLicenceType)
        {
            bool isValidLicenceType = false;
            
            if(i_MotorcycleLicenceType.Length == 1)
            {
                if(i_MotorcycleLicenceType[0] > '0' && i_MotorcycleLicenceType[0] < '5')  //need to change to constants
                {
                    isValidLicenceType = true;
                }
            }
            else
            {
                isValidLicenceType = false;
            }
            return isValidLicenceType;
        }

        private static bool IsValidEngineVolume(string i_EngineVolume, out int i_ValidEngineVolume)
        {
            return int.TryParse(i_EngineVolume, out i_ValidEngineVolume);
        }




        public static void FillAir(string i_LicensePlateNumber)
        {
            if (GarageManager.CheckIfVehicleInGarage(i_LicensePlateNumber))
            {
                GarageManager.FillAir(i_LicensePlateNumber);
            }
            else
            {
                Console.WriteLine("sorry the given vehicle is not in the garage");
            }
        }

        public static void FillBattery(string i_LicensePlateNumber)
        {
            if (GarageManager.CheckIfVehicleInGarage(i_LicensePlateNumber))
            {
                GarageManager.FillBattery(i_LicensePlateNumber);
            }
            else
            {
                Console.WriteLine("sorry the given vehicle is not in the garage");
            }
        }

        public static void Refuel(string i_LicensePlateNumber)
        {
            if (GarageManager.CheckIfVehicleInGarage(i_LicensePlateNumber))
            {
                GarageManager.Refuel(i_LicensePlateNumber);
            }
            else
            {
                Console.WriteLine("sorry the given vehicle is not in the garage");
            }
        }
    }
}
