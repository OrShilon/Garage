using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class UIManager
    {
        public static void Welcome()
        {
            Console.WriteLine("Hello and welcome to our garage.{0}", Environment.NewLine);
            Thread.Sleep(1200);
            GarageOptionsForCustomer();
        }

        public static void GarageOptionsForCustomer()
        {
            string userInput;
            int validOption;
            string menu = string.Format(@"What is the purpose of your visit?
1. I would like to check in a new vehicle
2. Display the current license plates that are in the garage
3. Change my vehicle status
4. Fill air in my vehicle wheels
5. Refuel my vehicle
6. Recharge my vehicle
7. Display my vehicle details
8. Exit program
Type in the corresponding number to your visit purpose please{0}", Environment.NewLine);
            Console.Write(menu);
            userInput = Console.ReadLine();
            while(!IsValidEnumInput(userInput, Enum.GetNames(typeof(eMenu)).Length, out validOption))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine(menu);
                userInput = Console.ReadLine();
            }

            eMenu userOption = (eMenu) validOption;

            //need to add more cases for all the options
            switch (userOption)
            {
                case eMenu.AddNewVehicle:
                    AddNewVehicle();
                    break;
                case eMenu.DisplayGarageLicensePlates:
                    DisplayVehiclesInGarage();
                    break;
                case eMenu.ChangeCarStatus:
                    ChangeVehicleStatus();
                    break;
                case eMenu.FillAir:
                    FillAir();
                    break;
                case eMenu.Refuel:
                    ReFuel();
                    break;
                case eMenu.Recharge:
                    FillBattery();
                    break;
                case eMenu.DisplayVehicleDitails:
                    DisplayVehicleDetails();
                    break;
                default:
                    //invalid input, need to handle.......
                    break;
            }
        }

        public static void AddNewVehicle()
        {
            eVehicles userVehicle;
            string vehicleModel;
            string licencePlate;
            float energyLeft = 0f; //can be fuel or battery
            bool isElectricVehicle;
            string ownerName;
            string ownerPhoneNumber;


            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            if (GarageManager.VehiclesStatusDictionary.ContainsKey(licencePlate))
            {
                Console.WriteLine("Your vehicle is already in our garage!{0}", Environment.NewLine);
                GarageOptionsForCustomer();
            }
            else
            {
                Console.WriteLine("Please enter you name:");
                ownerName = Console.ReadLine();
                Console.WriteLine("Please enter your phone number:");
                ownerPhoneNumber = Console.ReadLine();
                VehicleOwner owner = new VehicleOwner(ownerName, ownerPhoneNumber);
                //                string Messege = string.Format(@"What is the Type of your Vehicle?
                //1. Regular car
                //2. Electric Car
                //3. Regular Motorcycle
                //4. Electric Motorcycle
                //5. Truck
                //Type in the corresponding number to your vehicle please.{0}", Environment.NewLine);
                userVehicle = (eVehicles) PrintOptions(typeof(eVehicles));

                Console.WriteLine("Please enter your vehicle's model:");
                vehicleModel = Console.ReadLine();
                while (!IsValidVehicleModel(vehicleModel))
                {
                    Console.WriteLine("Not a valid input. Please enter your vehicle's model:");
                    vehicleModel = Console.ReadLine();
                }

                switch (userVehicle)
                {
                    case eVehicles.Car:
                        isElectricVehicle = false;
                        CreateCar(isElectricVehicle, vehicleModel, licencePlate, energyLeft, owner);
                        break;
                    case eVehicles.ElectricCar:
                        isElectricVehicle = true;
                        CreateCar(isElectricVehicle, vehicleModel, licencePlate, energyLeft, owner);
                        break;
                    case eVehicles.Motorcycle:
                        isElectricVehicle = false;
                        CreateMotorcycle(isElectricVehicle, vehicleModel, licencePlate, energyLeft, owner);
                        break;
                    case eVehicles.ElectricMotorcycle:
                        isElectricVehicle = true;
                        CreateMotorcycle(isElectricVehicle, vehicleModel, licencePlate, energyLeft, owner);
                        break;
                    case eVehicles.Truck:
                        CreateTruck(vehicleModel, licencePlate, energyLeft, owner);
                        break;
                    default:
                        //invalid input, need to handle.......
                        break;
                }
                
                GarageOptionsForCustomer();
            }

        }

        private static bool IsValidEnumInput(string i_UserInput, int i_EnumLength, out int i_ValidUserInput)
        {
            int.TryParse(i_UserInput, out i_ValidUserInput);

            return i_ValidUserInput <= i_EnumLength && i_ValidUserInput > 0;
        }

        private static bool IsValidVehicleChoice(string i_UserChoice)
        {
            bool isValidInput = false;

            if (i_UserChoice.Length != 1)
            {
                isValidInput = false;
            }
            else
            {
                //צריך לשנות את '0' ו '6' להיות קונסט כי אי אפשר לשים רק מספר
                if (i_UserChoice[0] > '0' && i_UserChoice[0] < '6')
                {
                    isValidInput = true;
                }
            }

            return isValidInput;
        }

        // there is another method name CreateCar in GarageLogic! need to check if it is ok
        private static Vehicle CreateCar(bool i_IsElectric, string i_CarModel, string i_LicencePlate, float i_EnergyLeft, VehicleOwner i_VehicleOwner)
        {
            Vehicle vehicle;
            string energyLeftInput; //can be fuel or battery
            string color;
            string numOfDoorsInput;
            byte numOfDoors;
            float wheelsCurrentAirPressure;
            string wheelMaker;

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

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_CarMaxAirPressure);
            if (i_IsElectric)
            {
                ElectricCar newElectricCar = CreateVehicle.CreateElectricCar(i_CarModel, i_LicencePlate, i_EnergyLeft, color, numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newElectricCar);
                vehicle = newElectricCar;
            }
            else
            {
                Car newCar = CreateVehicle.CreateCar(i_CarModel, i_LicencePlate, i_EnergyLeft, color, numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newCar);
                vehicle = newCar;
            }

            return vehicle;
        }

        private static void CreateMotorcycle(bool i_IsElectric, string i_MotorcycleModel, string i_LicencePlate, float i_EnergyLeft, VehicleOwner i_VehicleOwner)
        {
            string energyLeftInput; //can be fuel or battery
            string licenceTypeInput;
            string validLicenceType = string.Empty;
            string engineVolume = string.Empty;
            int validEngineVolume;
            float wheelsCurrentAirPressure;
            string wheelMaker;
            eMotorcycleLicenceType licenceType;

            Console.WriteLine("Please enter how much {0} left in your motorcycle:", i_IsElectric ? "battery" : "fuel");
            energyLeftInput = Console.ReadLine();
            while (!IsValidFuelInput(energyLeftInput, out i_EnergyLeft))
            {
                Console.WriteLine("Not a valid input. Please enter how much {0} left in your motorcycle:", i_IsElectric ? "battery" : "fuel");
                energyLeftInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter your licence type:");
            licenceType = (eMotorcycleLicenceType) PrintOptions(typeof(eMotorcycleLicenceType));

            switch (licenceType)
            {
                case eMotorcycleLicenceType.A:
                    validLicenceType = "A";
                    break;
                case eMotorcycleLicenceType.A1:
                    validLicenceType = "A1";
                    break;
                case eMotorcycleLicenceType.AA:
                    validLicenceType = "AA";
                    break;
                case eMotorcycleLicenceType.B:
                    validLicenceType = "B";
                    break;
                default:
                    //exeption??? ----> need to check what to do here, because we will never get here!
                    break;

            }

            Console.WriteLine("Please enter your engine's volume (in Cc):");
            engineVolume = Console.ReadLine();
            while (!IsValidEngineVolume(engineVolume, out validEngineVolume))
            {
                Console.WriteLine("Not a valid input. Please enter the number of doors your car has:");
                engineVolume = Console.ReadLine();
            }

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_CarMaxAirPressure);

            if (i_IsElectric)
            {
                ElectricMotorcycle newElectricMotorcycle = CreateVehicle.CreateElectricMotorcycle(i_MotorcycleModel, i_LicencePlate, i_EnergyLeft, validLicenceType, validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newElectricMotorcycle);
            }
            else
            {
                Motorcycle newMotorcycle = CreateVehicle.CreateMotorcycle(i_MotorcycleModel, i_LicencePlate, i_EnergyLeft, validLicenceType, validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newMotorcycle);
            }
        }

        private static void CreateTruck(string i_TruckModel, string i_LicencePlate, float i_FuelLeft, VehicleOwner i_VehicleOwner)
        {
            string fuelLeftInput;
            string cargoVolume = string.Empty;
            float validCargoVolume;
            string dangerousMaterialsInput;
            bool dangerousMaterials;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            Console.WriteLine("Please enter how much fuel left in your truck:");
            fuelLeftInput = Console.ReadLine();
            while (!IsValidFuelInput(fuelLeftInput, out i_FuelLeft))
            {
                Console.WriteLine("Not a valid input. Please enter how much fuel left in your truck:");
                fuelLeftInput = Console.ReadLine();
            }

            Console.WriteLine("Is there any dangerous materials in your truck? Enter 1 for yes, 0 for no.");
            dangerousMaterialsInput = Console.ReadLine();
            while (!IsValidDangerousMaterialsInput(dangerousMaterialsInput, out dangerousMaterials))
            {
                Console.WriteLine("Not a valid input. Please enter the color of your car:");
                dangerousMaterialsInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter your truck's cargo volume:");
            cargoVolume = Console.ReadLine();
            //need to change the while to isvalidtruckcargo from isvalidfuelinput
            while (!IsValidFuelInput(cargoVolume, out validCargoVolume))
            {
                Console.WriteLine("Not a valid input. Please enter the number of doors your car has:");
                cargoVolume = Console.ReadLine();
            }

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_CarMaxAirPressure);

            Truck newTruck = CreateVehicle.CreateTruck(i_TruckModel, i_LicencePlate, i_FuelLeft, dangerousMaterials, validCargoVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
            GarageManager.AddVehicleToGarage(newTruck);
        }

        private static void GetWheelInformation(out string o_WheelMaker, out float o_CurrentAirPressure, float i_MaxAirPressure)
        {
            string currentAirPressureInput;
            Console.WriteLine("Please enter the name of your wheel maker:");
            o_WheelMaker = Console.ReadLine();
            Console.WriteLine("Please enter the currnet air pressure in your wheels:");
            currentAirPressureInput = Console.ReadLine();
            while(!float.TryParse(currentAirPressureInput, out o_CurrentAirPressure))
            {
                Console.WriteLine("Invalid air pressure input. Please enter the currnet air pressure in your wheels:");
                currentAirPressureInput = Console.ReadLine();
            }
                if(i_MaxAirPressure < o_CurrentAirPressure)
                {
                    //throw exception
                }
        }

        public static int PrintOptions(Type i_Enum)
        {
            //need to handle execption - make sure it is enum!!
            int length = i_Enum.GetEnumNames().Length;
            int index;
            string userInput;
            bool isValidInput = true; //true means that the input by the user was invalid
            int validUerInput;

            do
            {
                index = 1;
                if(!isValidInput)
                {
                    Console.WriteLine("Invalid input.{0}", Environment.NewLine);
                }

                foreach (string item in i_Enum.GetEnumNames())
                {
                    Console.WriteLine("{0}. {1}", index, item);
                    index++;
                }

                Console.WriteLine("Type in the corresponding number to your vehicle please.{0}", Environment.NewLine);
                userInput = Console.ReadLine();

                if(!IsValidEnumInput(userInput, length, out validUerInput))
                {
                    isValidInput = false;
                }

            } while (!isValidInput);

            return validUerInput;
        }

        private static bool IsValidVehicleModel(string i_VehicleModel)
        {
            return !i_VehicleModel.Equals(string.Empty);
        }

        private static bool IsValidFuelInput(string i_FuelLeftInput, out float o_FuelLeft)
        {
            return float.TryParse(i_FuelLeftInput, out o_FuelLeft);
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

            if (i_MotorcycleLicenceType.Length == 1)
            {
                if (i_MotorcycleLicenceType[0] > '0' && i_MotorcycleLicenceType[0] < '5')  //need to change to constants
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

        private static bool IsValidDangerousMaterialsInput(string i_IsDangerous, out bool i_DangerousMaterials)
        {
            int isDangerous;
            bool isValidInput = false;
            i_DangerousMaterials = false;
            if (!int.TryParse(i_IsDangerous, out isDangerous))
            {
                i_DangerousMaterials = false;
            }
            else
            {
                //need to change to const
                if (isDangerous == 0 || isDangerous == 1)
                {
                    isValidInput = true;
                    if (isDangerous == 1)
                    {
                        i_DangerousMaterials = true;
                    }
                    i_DangerousMaterials = true;
                }
            }

            return isValidInput;
        }


        public static void FillAir(string i_LicensePlateNumber)
        {
            GarageLogic.GarageManager.FillAir(i_LicensePlateNumber);
        }

        public static void FillBattery(string i_LicensePlateNumber, float i_HowMuchToFill)
        {
            GarageLogic.GarageManager.FillBattery(i_LicensePlateNumber, i_HowMuchToFill);
        }

        public static void Refuel(string i_LicensePlateNumber, byte i_HowMuchToFill, eFuelTypes i_FuelType)
        {
            GarageLogic.GarageManager.Refuel(i_LicensePlateNumber, i_HowMuchToFill, i_FuelType);
        }

        private static void DisplayVehiclesInGarage()
        {
            string userInput;
            eVehicleStatus userStatus;

            Console.WriteLine("Do you want to filter by status? Enter 1 for yes, 0 for no");
            userInput = Console.ReadLine();
            while(!IsValidStatusInput(userInput))
            {
                Console.WriteLine("Invalid input. Do you want to filter by status? Enter 1 for yes, 0 for no");
                userInput = Console.ReadLine();
            }

            if(userInput[0].Equals('0'))
            {
                GarageManager.PrintVehiclesInGarage();
            }
            else
            {
                Console.WriteLine("Please enter the wanted status to display");
                userStatus = (eVehicleStatus) PrintOptions(typeof(eVehicleStatus));
                GarageManager.PrintVehiclesInGarage(userStatus);
            }
        }

        private static bool IsValidStatusInput(string i_StatusInput)
        {
            return i_StatusInput[0].Equals('0') || i_StatusInput[0].Equals('1');
        }

        public static void ChangeVehicleStatus()
        {
            Console.WriteLine("What is the license plate of your vehicle?");
            string licensePlate = Console.ReadLine(); //need to make invalidinput for this line!
            if (GarageManager.CheckIfVehicleInGarage(licensePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                Console.WriteLine("What would you like the new status to be?");
                GarageManager.ChangeVehicleStatus(licensePlate, (eVehicleStatus)PrintOptions(typeof(eVehicleStatus)));
            }
        }

        private static void FillAir()
        {
            string licencePlate;
            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                GarageManager.FillAir(licencePlate);
            }
        }
        private static void ReFuel()
        {
            string licencePlate;
            string LittersOfFuelToFiil;
            float ValidLittersOfFuelToAdd;
            eFuelTypes fuelType;
            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                Console.WriteLine("Please enter how much fuel to fill:");
                LittersOfFuelToFiil = Console.ReadLine();
                while (!IsValidFuelInput(LittersOfFuelToFiil, out ValidLittersOfFuelToAdd))
                {
                    Console.WriteLine("Invalid input. Please enter how much fuel to fill:");
                    LittersOfFuelToFiil = Console.ReadLine();
                }

                fuelType = (eFuelTypes)PrintOptions(typeof(eFuelTypes));
                GarageManager.Refuel(licencePlate, ValidLittersOfFuelToAdd, fuelType);
            }
        }
        private static void FillBattery()
        {
            string licencePlate;
            string batteryHoursInput;
            float ValidBatteryHours;

            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                Console.WriteLine("Please enter how much battery to fill:");
                batteryHoursInput = Console.ReadLine();
                while (!IsValidFuelInput(batteryHoursInput, out ValidBatteryHours))
                {
                    Console.WriteLine("Invalid input. Please enter how much battery to fill:");
                    batteryHoursInput = Console.ReadLine();
                }

                GarageManager.FillBattery(licencePlate, ValidBatteryHours);
            }
        }

        private static void DisplayVehicleDetails()
        {
            string licencePlate;
            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                GarageManager.PrintVehicleDetails(licencePlate);
            }
        }
    }
}
