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
                    //exit program.....
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
                Thread.Sleep(1000);
                Ex02.ConsoleUtils.Screen.Clear();
                GarageOptionsForCustomer();
            }
        }

        // there is another method name CreateCar in GarageLogic! need to check if it is ok
        private static Vehicle CreateCar(bool i_IsElectric, string i_CarModel, string i_LicencePlate, float i_EnergyLeft, VehicleOwner i_VehicleOwner)
        {
            Vehicle vehicle;
            string energyLeftInput; //can be fuel or battery
            eCarColors color;
            eNumOfDoors numOfDoors;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            Console.WriteLine(MessagesEnglish.k_GetFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage);
            energyLeftInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyLeftInput, out i_EnergyLeft) || (i_IsElectric && i_EnergyLeft > 2.1) || (!i_IsElectric && i_EnergyLeft < 60))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage);
                energyLeftInput = Console.ReadLine();
            }
            Console.WriteLine(MessagesEnglish.k_GetColorMessage);
            color = (eCarColors) DisplayEnumOptions(typeof(eCarColors));
            Console.WriteLine(MessagesEnglish.k_GetNumDoorsMessage);
            numOfDoors = (eNumOfDoors)DisplayEnumOptions(typeof(eNumOfDoors));

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

            Console.WriteLine("Please enter how much {0} left in your motorcycle:", i_IsElectric ? "battery" : "fuel");//already have same message for car
            energyLeftInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyLeftInput, out i_EnergyLeft) || (i_IsElectric && i_EnergyLeft > 1.2) || (!i_IsElectric && i_EnergyLeft < 7))
            {
                Console.WriteLine("Not a valid input. Please enter how much {0} left in your motorcycle:", i_IsElectric ? "battery" : "fuel");
                energyLeftInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter your licence type:");
            licenceType = (eMotorcycleLicenceType) DisplayEnumOptions(typeof(eMotorcycleLicenceType));

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
            while (!InputValidation.IsValidEngineVolume(engineVolume, out validEngineVolume))
            {
                Console.WriteLine("Not a valid input. Please enter the engine's volume (in Cc): your Motorcycle has:");
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
            string cargoVolume;
            float validCargoVolume;
            string dangerousMaterialsInput;
            bool dangerousMaterials;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            Console.WriteLine("Please enter how much fuel left in your truck:");
            fuelLeftInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(fuelLeftInput, out i_FuelLeft))
            {
                Console.WriteLine("Not a valid input. Please enter how much fuel left in your truck:");
                fuelLeftInput = Console.ReadLine();
            }

            Console.WriteLine("Is there any dangerous materials in your truck? Enter 1 for yes, 0 for no.");
            dangerousMaterialsInput = Console.ReadLine();
            while (!InputValidation.IsValidDangerousMaterialsInput(dangerousMaterialsInput, out dangerousMaterials))
            {
                Console.WriteLine("Not a valid input. Please enter the color of your car:");
                dangerousMaterialsInput = Console.ReadLine();
            }

            Console.WriteLine("Please enter your truck's cargo volume:");
            cargoVolume = Console.ReadLine();
            //need to change the while to isvalidtruckcargo from isvalidfuelinput
            while (!InputValidation.IsValidFloatInput(cargoVolume, out validCargoVolume))
            {
                Console.WriteLine("Not a valid input. Please enter the number of doors your car has:");
                cargoVolume = Console.ReadLine();
            }

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_TruckMaxAirPressure);

            Truck newTruck = CreateVehicle.CreateTruck(i_TruckModel, i_LicencePlate, i_FuelLeft, dangerousMaterials, validCargoVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
            GarageManager.AddVehicleToGarage(newTruck);
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
                        Console.WriteLine(MessagesEnglish.k_InvalidInputAndNewLineMessage, Environment.NewLine);
                    }

                    foreach (string item in i_Enum.GetEnumNames())
                    {
                        Console.WriteLine(MessagesEnglish.k_0OR1Message, index, item);
                        index++;
                    }

                    Console.WriteLine(MessagesEnglish.k_ChooseVehicleMessage, Environment.NewLine);
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

            Console.WriteLine(MessagesEnglish.k_GetWheelMakerMessage);
            o_WheelMaker = Console.ReadLine();
            while(InputValidation.IsEmptyInput(o_WheelMaker))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidWheelMakerMessage);
                o_WheelMaker = Console.ReadLine();
            }

            Console.WriteLine(MessagesEnglish.k_GetCurrentAirPressureMessage, i_MaxAirPressure);
            currentAirPressureInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(currentAirPressureInput, out o_CurrentAirPressure) || i_MaxAirPressure < o_CurrentAirPressure)
            {
                Console.WriteLine(i_MaxAirPressure < o_CurrentAirPressure ? MessagesEnglish.k_AirPressureAboveMAximumMessage : MessagesEnglish.k_InvalidAirPressureMessage);
                Console.WriteLine(MessagesEnglish.k_GetCurrentAirPressureMessage, i_MaxAirPressure);
                currentAirPressureInput = Console.ReadLine();
            }
        }

        private static void DisplayVehiclesInGarage()
        {
            string userInput;
            eVehicleStatus userStatus;

            Console.WriteLine(MessagesEnglish.k_FilterByStatusMessage);
            userInput = Console.ReadLine();
            while(!InputValidation.IsValidStatusFilterInput(userInput))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidFilterByStatusMessage);
                userInput = Console.ReadLine();
            }

            Ex02.ConsoleUtils.Screen.Clear();

            if (userInput[0].Equals('0'))
            {
                GarageManager.PrintVehiclesInGarage();
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_GetStatusFilterMessage);
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
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_GetNewStatusMessage);
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
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
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
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_RefuelAmountMessage);
                LittersOfFuelToFiil = Console.ReadLine();
                while (!InputValidation.IsValidFloatInput(LittersOfFuelToFiil, out ValidLittersOfFuelToAdd))
                {
                    Console.WriteLine(MessagesEnglish.k_NotValidRefuelAmountMessage);
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
                licencePlate = EnterLicencePlate();
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_NotValidBatteryMessage);
                licencePlate = Console.ReadLine(); //need to make invalidinput for this line!
            }

            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_BatteryToChargeMessage);
                batteryHoursInput = Console.ReadLine();
                while (!InputValidation.IsValidFloatInput(batteryHoursInput, out ValidBatteryHours))
                {
                    Console.WriteLine(MessagesEnglish.k_NotValidBatteryMessage);
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
            licencePlate = EnterLicencePlate();
            if (GarageManager.CheckIfVehicleInGarage(licencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
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
            Console.WriteLine(MessagesEnglish.k_LicensePlateRequestMessage);
            licencePlate = Console.ReadLine();
            while(InputValidation.IsEmptyInput(licencePlate))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidLicensePlateMessage);
                licencePlate = Console.ReadLine();
            }
            return licencePlate;
        }

        private static void ExitProgram()
        {
            Console.WriteLine(MessagesEnglish.k_ExitMessage);
            Thread.Sleep(1500);
            Environment.Exit(0);
        }
    }
}
