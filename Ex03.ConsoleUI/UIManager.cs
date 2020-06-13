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
        private const float k_CarMaxFuel = 60f;
        private const float k_ElectricCarMaxBattery = 2.1f;
        private const float k_ElectricMotorcycleMaxBattery = 1.2f;
        private const float k_MotorcycleMaxFuel = 7f;
        private const float k_TruckMaxFuel = 120f;

        public static void Welcome()
        {
            Console.WriteLine(MessagesEnglish.k_GarageHelloMessage, Environment.NewLine);
            Thread.Sleep(1200);
            DisplayGarageMenu();
        }

        public static void DisplayGarageMenu()
        {
            string userInput;
            int validOption;
            string licencePlate = string.Empty;
            bool exitProgram = false;

            while(!exitProgram)
            {
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
              
                eMenu userOption = (eMenu)validOption;

                if (!userOption.Equals(eMenu.DisplayVehiclesInGarage))
                {
                    licencePlate = EnterLicencePlate();
                }

                SentToUserChoice(licencePlate, userOption);
            }
        }

        private static void SentToUserChoice(string i_LicencePlate, eMenu i_UserOption)
        {
            try
            {
                switch (i_UserOption)
                {
                    case eMenu.AddNewVehicle:
                        AddNewVehicle(i_LicencePlate);
                        break;
                    case eMenu.DisplayVehiclesInGarage:
                        DisplayVehiclesInGarage();
                        break;
                    case eMenu.ChangeCarStatus:
                        ChangeVehicleStatus(i_LicencePlate);
                        break;
                    case eMenu.FillAir:
                        FillAir(i_LicencePlate);
                        break;
                    case eMenu.Refuel:
                        ReFuel(i_LicencePlate);
                        break;
                    case eMenu.Recharge:
                        FillBattery(i_LicencePlate);
                        break;
                    case eMenu.DisplayVehicleDetails:
                        DisplayVehicleDetails(i_LicencePlate);
                        break;
                    case eMenu.Exit:
                        ExitProgram();
                        break;
                    default:
                        //invalid input, need to handle.......
                        break;
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message + Environment.NewLine + MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
            }

        }
        public static void AddNewVehicle(string i_LicencePlate)
        {
            eVehicles userVehicle;
            string vehicleModel;
            bool isElectricVehicle;
            string ownerName;
            string ownerPhoneNumber;

            if (GarageManager.VehiclesStatusDictionary.ContainsKey(i_LicencePlate))
            {
                Console.WriteLine(MessagesEnglish.k_VehicleIsRegistered);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
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
                Console.WriteLine(MessagesEnglish.k_GetVehicleTypeMessage);
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
                        CreateCar(isElectricVehicle, vehicleModel, i_LicencePlate, owner, eVehicles.Car);
                        break;
                    case eVehicles.ElectricCar:
                        isElectricVehicle = true;
                        CreateCar(isElectricVehicle, vehicleModel, i_LicencePlate, owner, eVehicles.ElectricCar);
                        break;
                    case eVehicles.Motorcycle:
                        isElectricVehicle = false;
                        CreateMotorcycle(isElectricVehicle, vehicleModel, i_LicencePlate, owner, eVehicles.Motorcycle);
                        break;
                    case eVehicles.ElectricMotorcycle:
                        isElectricVehicle = true;
                        CreateMotorcycle(isElectricVehicle, vehicleModel, i_LicencePlate, owner, eVehicles.ElectricMotorcycle);
                        break;
                    case eVehicles.Truck:
                        CreateTruck(vehicleModel, i_LicencePlate, owner, eVehicles.Truck);
                        break;
                    default:
                        //invalid input, need to handle.......
                        break;
                }
                Console.WriteLine(MessagesEnglish.k_AddedNewCarMessage);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
            }

            Thread.Sleep(3000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void CreateCar(bool i_IsElectric, string i_CarModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles vehicle)
        {
            float energyLeft;
            eCarColors color;
            eNumOfDoors numOfDoors;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            energyLeft = EnterFuellOrEnergyLeft(i_IsElectric, vehicle, i_IsElectric ? k_ElectricCarMaxBattery : k_CarMaxFuel);

            Console.WriteLine(MessagesEnglish.k_GetColorMessage);
            color = (eCarColors) DisplayEnumOptions(typeof(eCarColors));

            Console.WriteLine(MessagesEnglish.k_GetNumDoorsMessage);
            numOfDoors = (eNumOfDoors)DisplayEnumOptions(typeof(eNumOfDoors));

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_CarMaxAirPressure);
            if (i_IsElectric)
            {
                ElectricCar newElectricCar = CreateVehicle.CreateElectricCar(i_CarModel, i_LicencePlate, energyLeft, color,
                    numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
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
            int validEngineVolume;
            float wheelsCurrentAirPressure;
            string wheelMaker;
            eMotorcycleLicenceType licenceType;

            energyLeft = EnterFuellOrEnergyLeft(i_IsElectric, i_Vehicle, i_IsElectric ? k_ElectricMotorcycleMaxBattery : k_MotorcycleMaxFuel);

            Console.WriteLine(MessagesEnglish.k_GetLicenseTypeMessage);
            licenceType = (eMotorcycleLicenceType) DisplayEnumOptions(typeof(eMotorcycleLicenceType));

            validEngineVolume = EnterEngineVolume();

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_MotorcycleMaxAirPressure);

            if (i_IsElectric)
            {
                ElectricMotorcycle newElectricMotorcycle = CreateVehicle.CreateElectricMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, licenceType,
                    validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                GarageManager.AddVehicleToGarage(newElectricMotorcycle);
            }
            else
            {
                Motorcycle newMotorcycle = CreateVehicle.CreateMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, licenceType,
                    validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
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

            fuelLeft = EnterFuellOrEnergyLeft(isElectric, i_Vehicle, k_TruckMaxFuel);

            dangerousMaterials = EnterDangerousMaterialsInput();

            CargoVolume = EnterCargoVolume();

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, GarageManager.m_TruckMaxAirPressure);

            Truck newTruck = CreateVehicle.CreateTruck(i_TruckModel, i_LicencePlate, fuelLeft, dangerousMaterials, CargoVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
            GarageManager.AddVehicleToGarage(newTruck);
        }

        private static float EnterFuellOrEnergyLeft(bool i_IsElectric, eVehicles i_Vehicle, float i_MaxEnergyOrFuelLeft)
        {
            string energyLeftInput;
            float energyLeft;

            Console.WriteLine(MessagesEnglish.k_GetFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle);
            energyLeftInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyLeftInput, out energyLeft) || (energyLeft > i_MaxEnergyOrFuelLeft))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle);
                energyLeftInput = Console.ReadLine();
            }
            return energyLeft;
        }

        private static int EnterEngineVolume()
        {
            string engineVolumeInput;
            int validEngineVolume = 0;

            Console.WriteLine(MessagesEnglish.k_GetEngineVolumeMessage);
            engineVolumeInput = Console.ReadLine();

            try
            {
                while (!InputValidation.IsValidEngineVolume(engineVolumeInput, out validEngineVolume))
                {
                    Console.WriteLine(MessagesEnglish.k_NotValidEngineVolumeMessage);
                    engineVolumeInput = Console.ReadLine();
                }
            }
            catch(FormatException fe)
            {
                Console.WriteLine(fe.Message);
                EnterEngineVolume();
            }

            return validEngineVolume;
        }

        private static bool EnterDangerousMaterialsInput()
        {
            string dangerousMaterialsInput;
            bool dangerousMaterials;

            Console.WriteLine(MessagesEnglish.k_DangerMaterialMessage);
            dangerousMaterialsInput = Console.ReadLine();
            while (!InputValidation.IsValidDangerousMaterialsInput(dangerousMaterialsInput, out dangerousMaterials))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidDangerMaterialMessage);
                dangerousMaterialsInput = Console.ReadLine();
            }

            return dangerousMaterials;
        }

        private static float EnterCargoVolume()
        {
            string cargoVolumeInput;
            float validCargoVolume;

            Console.WriteLine(MessagesEnglish.k_GetCargoVolumeMessage);
            cargoVolumeInput = Console.ReadLine();

            while (!InputValidation.IsValidFloatInput(cargoVolumeInput, out validCargoVolume))
            {
                Console.WriteLine(MessagesEnglish.k_NotValidCargoVolumeMessage);
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

            if (userInput[0].Equals(InputValidation.k_ZeroChar))
            {
                GarageManager.PrintVehiclesInGarage();
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_GetStatusFilterMessage);
                userStatus = (eVehicleStatus) DisplayEnumOptions(typeof(eVehicleStatus));
                GarageManager.PrintVehiclesInGarage(userStatus);
            }
          
            Console.WriteLine(Environment.NewLine + MessagesEnglish.k_PressButtonToGoToMainMenuMessage);
            Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static void ChangeVehicleStatus(string i_LicencePlate)
        {

            if (GarageManager.CheckIfVehicleInGarage(i_LicencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
                Thread.Sleep(1000);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine(MessagesEnglish.k_GetNewStatusMessage);
                GarageManager.ChangeVehicleStatus(i_LicencePlate, (eVehicleStatus)DisplayEnumOptions(typeof(eVehicleStatus)));
                Console.WriteLine(MessagesEnglish.k_StatusChangedMessage + MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(1500);
            }
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void FillAir(string i_LicencePlate)
        {

            if (GarageManager.CheckIfVehicleInGarage(i_LicencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
                Thread.Sleep(1000);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(500);
            }
            else
            {
                GarageManager.FillAir(i_LicencePlate);
                Console.WriteLine(MessagesEnglish.k_WheelsInflatedMessage + MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(1000);
            }
            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void ReFuel(string i_LicencePlate)
        {
            string LittersOfFuelToFiil;
            float ValidLittersOfFuelToAdd;
            eFuelTypes fuelType;
            int indexOfVehicleInGarage;

            if ((indexOfVehicleInGarage = GarageManager.CheckIfVehicleInGarage(i_LicencePlate)) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
                Thread.Sleep(1000);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(500);
            }
            else
            {

                if (GarageManager.VehiclesInGarage[indexOfVehicleInGarage] is ElectricVehicle)
                {
                    throw new ArgumentException("NOT a fuel vehicle");
                }

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
                    GarageManager.Refuel(i_LicencePlate, ValidLittersOfFuelToAdd, fuelType);
                    Console.WriteLine(MessagesEnglish.k_IsRefueledMessage + MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1000);
                }
                catch (ValueOutOfRangeException vore)
                {
                    Console.WriteLine(vore.Message);
                    Thread.Sleep(2000);
                    Ex02.ConsoleUtils.Screen.Clear();
                    ReFuel(i_LicencePlate);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message + Environment.NewLine + MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1000);
                }
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }
        private static void FillBattery(string i_LicencePlate)
        {
            string batteryHoursInput;
            float ValidBatteryHours;
            int indexOfVehicleInGarage;
            if ((indexOfVehicleInGarage = GarageManager.CheckIfVehicleInGarage(i_LicencePlate)) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
                Thread.Sleep(1000);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(500);
            }
            else
            {
                if (GarageManager.VehiclesInGarage[indexOfVehicleInGarage] is FuelVehicle)
                {
                    throw new ArgumentException("NOT an Electric vehicle");
                }

                Console.WriteLine(MessagesEnglish.k_BatteryToChargeMessage);
                batteryHoursInput = Console.ReadLine();
                while (!InputValidation.IsValidFloatInput(batteryHoursInput, out ValidBatteryHours))
                {
                    Console.WriteLine(MessagesEnglish.k_NotValidBatteryMessage);
                    batteryHoursInput = Console.ReadLine();
                }

                try
                {
                    GarageManager.FillBattery(i_LicencePlate, ValidBatteryHours);
                    Console.WriteLine(MessagesEnglish.k_IsRechargedMessage + MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1000);
                }
                catch (ValueOutOfRangeException vore)
                {
                    Console.WriteLine(vore.Message);
                    Thread.Sleep(2000);
                    Ex02.ConsoleUtils.Screen.Clear();
                    FillBattery(i_LicencePlate);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message + Environment.NewLine + MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1000);
                }
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void DisplayVehicleDetails(string i_LicencePlate)
        {

            if (GarageManager.CheckIfVehicleInGarage(i_LicencePlate) == GarageManager.k_NotInGarage)
            {
                Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
                Thread.Sleep(1000);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(500);
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                GarageManager.PrintVehicleDetails(i_LicencePlate);
                Console.WriteLine(Environment.NewLine + MessagesEnglish.k_PressButtonToGoToMainMenuMessage);
                Console.ReadLine();
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
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
