using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class UIManager
    {

        private const bool k_FirstTimeLicencePlateInput = true;
        public static void Welcome()
        {
            Console.WriteLine(MessagesEnglish.k_GarageHelloMessage, Environment.NewLine);
            Thread.Sleep(1200);
            GarageOptionsForCustomer();
        }

        public static void GarageOptionsForCustomer()
        {
            string userInput;
            int validOption;
            Console.WriteLine(MessagesEnglish.k_GarageOptionsMessage);
            userInput = Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
            while (!InputValidation.IsValidEnumInput(userInput, Enum.GetNames(typeof(eMenu)).Length, out validOption))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Thread.Sleep(1000);
                Console.WriteLine(MessagesEnglish.k_GarageOptionsMessage);
                userInput = Console.ReadLine();
                Ex02.ConsoleUtils.Screen.Clear();
            }

            eMenu userOption = (eMenu) validOption;

            switch (userOption)
            {
                case eMenu.AddNewVehicle:
                    AddNewVehicle();
                    break;
                case eMenu.DisplayVehiclesInGarage:
                    DisplayVehiclesInGarage();
                    break;
                case eMenu.ChangeCarStatus:
                    ChangeVehicleStatus();
                    break;
                case eMenu.FillAir:
                    FillAir();
                    break;
                case eMenu.Refuel:
                    ReFuel(k_FirstTimeLicencePlateInput);
                    break;
                case eMenu.Recharge:
                    FillBattery(k_FirstTimeLicencePlateInput);
                    break;
                case eMenu.DisplayVehicleDetails:
                    DisplayVehicleDetails();
                    break;
                case eMenu.Exit:
                    ExitProgram();
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



            licencePlate = EnterLicencePlate();
            if (GarageManager.VehiclesStatusDictionary.ContainsKey(licencePlate))
            {
                Console.WriteLine(MessagesEnglish.k_VehicleIsRegistered, Environment.NewLine); 
                Thread.Sleep(1200);
                Ex02.ConsoleUtils.Screen.Clear();
                GarageOptionsForCustomer();
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_GetNameMessage);
                ownerName = Console.ReadLine();
                Console.WriteLine(MessagesEnglish.k_GetPhoneNumberMessage);
                ownerPhoneNumber = Console.ReadLine();
                while(!InputValidation.IsValidPhoneNumber(ownerPhoneNumber))
                {
                    Console.WriteLine(MessagesEnglish.k_NotValidPhoneNumberMessage);
                    ownerPhoneNumber = Console.ReadLine();
                }
                VehicleOwner owner = new VehicleOwner(ownerName, ownerPhoneNumber);
                userVehicle = (eVehicles) DisplayEnumOptions(typeof(eVehicles));

                Console.WriteLine(MessagesEnglish.k_GetVehicleModelMessage);
                vehicleModel = Console.ReadLine();
                while (InputValidation.IsEmptyInput(vehicleModel))
                {
                    Console.WriteLine(MessagesEnglish.k_NotValidVehicleModelMessage);
                    vehicleModel = Console.ReadLine();
                }

                switch (userVehicle)
                {
                    case eVehicles.Car:
                        isElectricVehicle = false;
                        CreateCar(isElectricVehicle, vehicleModel, licencePlate, owner, eVehicles.Car);
                        break;
                    case eVehicles.ElectricCar:
                        isElectricVehicle = true;
                        CreateCar(isElectricVehicle, vehicleModel, licencePlate, owner, eVehicles.ElectricCar);
                        break;
                    case eVehicles.Motorcycle:
                        isElectricVehicle = false;
                        CreateMotorcycle(isElectricVehicle, vehicleModel, licencePlate, owner, eVehicles.Motorcycle);
                        break;
                    case eVehicles.ElectricMotorcycle:
                        isElectricVehicle = true;
                        CreateMotorcycle(isElectricVehicle, vehicleModel, licencePlate, owner, eVehicles.ElectricMotorcycle);
                        break;
                    case eVehicles.Truck:
                        CreateTruck(vehicleModel, licencePlate, owner, eVehicles.Truck);
                        break;
                    default:
                        //invalid input, need to handle.......
                        break;
                }
                Thread.Sleep(1000);
                Ex02.ConsoleUtils.Screen.Clear();
                GarageOptionsForCustomer();
            }
        }

        // there is another method name CreateCar in GarageLogic! need to check if it is ok
        private static void CreateCar(bool i_IsElectric, string i_CarModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles vehicle)
        {
            float energyLeft;
            eCarColors color;
            eNumOfDoors numOfDoors;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            energyLeft = EnterFuellOrEnergyLeft(i_IsElectric, vehicle);

            Console.WriteLine(MessagesEnglish.k_GetColorMessage);
            color = (eCarColors) DisplayEnumOptions(typeof(eCarColors));

            Console.WriteLine(MessagesEnglish.k_GetNumDoorsMessage);
            numOfDoors = (eNumOfDoors)DisplayEnumOptions(typeof(eNumOfDoors));

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_CarMaxAirPressure);
            if (i_IsElectric)
            {
                ElectricCar newElectricCar = CreateVehicle.CreateElectricCar(i_CarModel, i_LicencePlate, energyLeft, color, numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newElectricCar);
            }
            else
            {
                Car newCar = CreateVehicle.CreateCar(i_CarModel, i_LicencePlate, energyLeft, color, numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newCar);
            }
        }


        private static void CreateMotorcycle(bool i_IsElectric, string i_MotorcycleModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles i_Vehicle)
        {
            float energyLeft;
            string validLicenceType = string.Empty;
            int validEngineVolume;
            float wheelsCurrentAirPressure;
            string wheelMaker;
            eMotorcycleLicenceType licenceType;

            energyLeft = EnterFuellOrEnergyLeft(i_IsElectric, i_Vehicle);

            Console.WriteLine("Please enter your licence type:");
            licenceType = (eMotorcycleLicenceType) DisplayEnumOptions(typeof(eMotorcycleLicenceType));

            validEngineVolume = EnterEngineVolume();

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_CarMaxAirPressure);

            if (i_IsElectric)
            {
                ElectricMotorcycle newElectricMotorcycle = CreateVehicle.CreateElectricMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, validLicenceType, validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newElectricMotorcycle);
            }
            else
            {
                Motorcycle newMotorcycle = CreateVehicle.CreateMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, validLicenceType, validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newMotorcycle);
            }
        }

        private static void CreateTruck(string i_TruckModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles i_Vehicle)
        {
            float fuelLeft;
            float CargoVolume;
            bool dangerousMaterials;
            float wheelsCurrentAirPressure;
            string wheelMaker;
            bool isElectric = false;

            fuelLeft = EnterFuellOrEnergyLeft(isElectric, i_Vehicle);

            dangerousMaterials = EnterDangerousMaterialsInput();

            CargoVolume = EnterCargoVolume();

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_TruckMaxAirPressure);

            Truck newTruck = CreateVehicle.CreateTruck(i_TruckModel, i_LicencePlate, fuelLeft, dangerousMaterials, CargoVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
            GarageManager.AddVehicleToGarage(newTruck);
        }

        private static float EnterFuellOrEnergyLeft(bool i_IsElectric, eVehicles i_Vehicle)
        {
            string energyLeftInput;
            float energyLeft;

            Console.WriteLine(MessagesEnglish.k_GetFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle);
            energyLeftInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyLeftInput, out energyLeft) || (i_IsElectric && energyLeft > 2.1) || (!i_IsElectric && energyLeft < 60))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle);
                energyLeftInput = Console.ReadLine();
            }
            return energyLeft;
        }

        private static int EnterEngineVolume()
        {
            string engineVolumeInput;
            int validEngineVolume;

            Console.WriteLine("Please enter your engine's volume (in Cc):");
            engineVolumeInput = Console.ReadLine();
            while (!InputValidation.IsValidEngineVolume(engineVolumeInput, out validEngineVolume))
            {
                Console.WriteLine("Not a valid input. Please enter the engine's volume (in Cc):");
                engineVolumeInput = Console.ReadLine();
            }
            return validEngineVolume;
        }

        private static bool EnterDangerousMaterialsInput()
        {
            string dangerousMaterialsInput;
            bool dangerousMaterials;

            Console.WriteLine("Is there any dangerous materials in your truck? Enter 1 for yes, 0 for no.");
            dangerousMaterialsInput = Console.ReadLine();
            while (!InputValidation.IsValidDangerousMaterialsInput(dangerousMaterialsInput, out dangerousMaterials))
            {
                Console.WriteLine("Not a valid input. Please enter the color of your car:");
                dangerousMaterialsInput = Console.ReadLine();
            }

            return dangerousMaterials;
        }

        private static float EnterCargoVolume()
        {
            string cargoVolumeInput;
            float validCargoVolume;

            Console.WriteLine("Please enter your truck's cargo volume:");
            cargoVolumeInput = Console.ReadLine();
            //need to change the while to isvalidtruckcargo from isvalidfuelinput
            while (!InputValidation.IsValidFloatInput(cargoVolumeInput, out validCargoVolume))
            {
                Console.WriteLine("Not a valid input. Please enter your truck's cargo volume:");
                cargoVolumeInput = Console.ReadLine();
            }

            return validCargoVolume;
        }

        public static int DisplayEnumOptions(Type i_Enum)
        {
            int length = i_Enum.GetEnumNames().Length;
            int index;
            string userInput;
            bool isValidInput = true; //true means that the input by the user was invalid
            int validUerInput;

            if (i_Enum.IsEnum)
            {
                do
                {
                    index = 1;
                    if (!isValidInput)
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

                    if (!InputValidation.IsValidEnumInput(userInput, length, out validUerInput))
                    {
                        isValidInput = false;
                    }
                    else
                    {
                        isValidInput = true;
                    }

                } while (!isValidInput);
            }
            else
            {
                validUerInput = 0;
                //handle bad Type.... (i dont think we will get here.....)
            }

            Ex02.ConsoleUtils.Screen.Clear();

            return validUerInput;
        }

        private static void GetWheelInformation(out string o_WheelMaker, out float o_CurrentAirPressure, float i_MaxAirPressure)
        {
            string currentAirPressureInput;

            Console.WriteLine("Please enter the name of your wheel maker:");
            o_WheelMaker = Console.ReadLine();
            while(InputValidation.IsEmptyInput(o_WheelMaker))
            {
                Console.WriteLine("Invalid input. Please enter the name of your wheel maker:");
                o_WheelMaker = Console.ReadLine();
            }

            Console.WriteLine("Please enter the currnet air pressure in your wheels (maximum {0}):", i_MaxAirPressure);
            currentAirPressureInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(currentAirPressureInput, out o_CurrentAirPressure) || i_MaxAirPressure < o_CurrentAirPressure)
            {
                Console.WriteLine("{1}.{2} Please enter the currnet air pressure in your wheels (maximum {0}):", i_MaxAirPressure, i_MaxAirPressure < o_CurrentAirPressure ? "You have entered a number above the maximum air pressure " : "Invalid air pressure input", Environment.NewLine);
                currentAirPressureInput = Console.ReadLine();
            }
        }

        private static void DisplayVehiclesInGarage()
        {
            string userInput;
            eVehicleStatus userStatus;

            Console.WriteLine("Do you want to filter by status? Enter 1 for yes, 0 for no");
            userInput = Console.ReadLine();
            while(!InputValidation.IsValidStatusFilterInput(userInput))
            {
                Console.WriteLine("Invalid input. Do you want to filter by status? Enter 1 for yes, 0 for no");
                userInput = Console.ReadLine();
            }

            Ex02.ConsoleUtils.Screen.Clear();

            if (userInput[0].Equals('0'))
            {
                GarageManager.PrintVehiclesInGarage();
            }
            else
            {
                Console.WriteLine("Please enter the wanted status to display");
                userStatus = (eVehicleStatus) DisplayEnumOptions(typeof(eVehicleStatus));
                GarageManager.PrintVehiclesInGarage(userStatus);
            }
            Thread.Sleep(8000);
            Ex02.ConsoleUtils.Screen.Clear();
            GarageOptionsForCustomer();
        }

        public static void ChangeVehicleStatus()
        {
            string licencePlate;

            licencePlate = EnterLicencePlate();
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine("What would you like the new status to be?");
                GarageManager.ChangeVehicleStatus(licencePlate, (eVehicleStatus)DisplayEnumOptions(typeof(eVehicleStatus)));
            }
            Ex02.ConsoleUtils.Screen.Clear();
            GarageOptionsForCustomer();
        }

        private static void FillAir()
        {
            string licencePlate;

            licencePlate = EnterLicencePlate();
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                GarageManager.FillAir(licencePlate);
            }
            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
            GarageOptionsForCustomer();
        }
        private static void ReFuel( bool i_FirstTimeLicencePlateInput)
        {
            string licencePlate = string.Empty;
            string LittersOfFuelToFiil;
            float ValidLittersOfFuelToAdd;
            eFuelTypes fuelType;
            if(i_FirstTimeLicencePlateInput)
            {
                licencePlate = EnterLicencePlate();
            }

            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                Console.WriteLine("Please enter how much fuel to fill:");
                LittersOfFuelToFiil = Console.ReadLine();
                while (!InputValidation.IsValidFloatInput(LittersOfFuelToFiil, out ValidLittersOfFuelToAdd))
                {
                    Console.WriteLine("Invalid input. Please enter how much fuel to fill:");
                    LittersOfFuelToFiil = Console.ReadLine();
                }

                fuelType = (eFuelTypes)DisplayEnumOptions(typeof(eFuelTypes));

                try
                {
                    GarageManager.Refuel(licencePlate, ValidLittersOfFuelToAdd, fuelType);
                }
                catch (ValueOutOfRangeException vore)
                {
                    Console.WriteLine(vore.Message);
                    i_FirstTimeLicencePlateInput = false;
                    ReFuel(i_FirstTimeLicencePlateInput);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                    i_FirstTimeLicencePlateInput = false;
                    ReFuel(i_FirstTimeLicencePlateInput);
                }
            }
            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
            GarageOptionsForCustomer();
        }
        private static void FillBattery(bool i_FirstTimeLicencePlateInput)
        {
            string licencePlate;
            string batteryHoursInput;
            float ValidBatteryHours;

            if (i_FirstTimeLicencePlateInput)
            {
                Console.WriteLine("please enter your licence plate:");
                licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter how much fuel to fill:");
                licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            }

            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
            }
            else
            {
                Console.WriteLine("Please enter how much battery to fill:");
                batteryHoursInput = Console.ReadLine();
                while (!InputValidation.IsValidFloatInput(batteryHoursInput, out ValidBatteryHours))
                {
                    Console.WriteLine("Invalid input. Please enter how much battery to fill:");
                    batteryHoursInput = Console.ReadLine();
                }

                try
                {
                    GarageManager.FillBattery(licencePlate, ValidBatteryHours);
                }
                catch (ValueOutOfRangeException vore)
                {
                    Console.WriteLine(vore.Message);
                    i_FirstTimeLicencePlateInput = false;
                    ReFuel(i_FirstTimeLicencePlateInput);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
            GarageOptionsForCustomer();
        }

        private static void DisplayVehicleDetails()
        {
            string licencePlate;
            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine("Sorry the given vehicle is not in the garage.");
                Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
                GarageOptionsForCustomer();
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                GarageManager.PrintVehicleDetails(licencePlate);
            }
            Thread.Sleep(8000);
            Ex02.ConsoleUtils.Screen.Clear();
            GarageOptionsForCustomer();
        }

        private static string EnterLicencePlate()
        {
            string licencePlate;
            Console.WriteLine("please enter your licence plate:");
            licencePlate = Console.ReadLine();
            while(InputValidation.IsEmptyInput(licencePlate))
            {
                Console.WriteLine("Invalid Input. please enter your licence plate:");
                licencePlate = Console.ReadLine();
            }
            return licencePlate;
        }

        private static void ExitProgram()
        {
            Console.WriteLine("Exit program...");
            Thread.Sleep(1500);
            Environment.Exit(0);
        }
    }
}
