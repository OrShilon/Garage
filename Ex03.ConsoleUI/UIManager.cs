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
    internal class UIManager
    {
        private static GarageManager myGarage = new GarageManager();
        private const float k_CarMaxFuel = 60f;
        private const float k_ElectricCarMaxBattery = 2.1f;
        private const float k_ElectricMotorcycleMaxBattery = 1.2f;
        private const float k_MotorcycleMaxFuel = 7f;
        private const float k_TruckMaxFuel = 120f;

        internal static void Welcome()
        {
            Console.WriteLine(MessagesEnglish.k_GarageHelloMessage, Environment.NewLine);
            Thread.Sleep(1200);
            DisplayGarageMenu();
        }

        private static void DisplayGarageMenu()
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
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(2000);
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine(MessagesEnglish.k_GarageOptionsMessage);
                    userInput = Console.ReadLine();
                    Ex02.ConsoleUtils.Screen.Clear();
                }
              
                eMenu userOption = (eMenu)validOption;

                if (!(userOption.Equals(eMenu.DisplayVehiclesInGarage) || userOption.Equals(eMenu.Exit)))
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
                        Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                        Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                        Thread.Sleep(2000);
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
        private static void AddNewVehicle(string i_LicencePlate)
        {
            eVehicles userVehicle;
            string vehicleModel;
            string ownerName;
            string ownerPhoneNumber;

            if (myGarage.VehiclesStatusDictionary.ContainsKey(i_LicencePlate))
            {
                Console.WriteLine(MessagesEnglish.k_VehicleIsRegistered);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
            }
            else
            {
                EnterOwnerDetails(out ownerName, out ownerPhoneNumber);
                VehicleOwner owner = new VehicleOwner(ownerName, ownerPhoneNumber);
                userVehicle = (eVehicles) DisplayEnumOptions(typeof(eVehicles), MessagesEnglish.k_GetVehicleTypeMessage);
                EnterVehicleModel(out vehicleModel);
                CreateVehicle(vehicleModel, i_LicencePlate, owner, userVehicle);
                Console.WriteLine(MessagesEnglish.k_AddedNewCarMessage);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
            }

            Thread.Sleep(3000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void EnterOwnerDetails(out string o_OwnerName, out string o_PhoneNumber)
        {
            Console.WriteLine(MessagesEnglish.k_GetNameMessage);
            o_OwnerName = Console.ReadLine();
            while (InputValidation.IsEmptyInput(o_OwnerName))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_GetNameMessage);
                o_OwnerName = Console.ReadLine();
            }

            Console.WriteLine(MessagesEnglish.k_GetPhoneNumberMessage);
            o_PhoneNumber = Console.ReadLine();
            while (!InputValidation.IsValidPhoneNumber(o_PhoneNumber))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_GetPhoneNumberMessage);
                o_PhoneNumber = Console.ReadLine();
            }
        }

        private static void EnterVehicleModel(out string o_VehicleModel)
        {
            Console.WriteLine(MessagesEnglish.k_GetVehicleModelMessage);
            o_VehicleModel = Console.ReadLine();
            while (InputValidation.IsEmptyInput(o_VehicleModel))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_GetVehicleModelMessage);
                o_VehicleModel = Console.ReadLine();
            }
        }

        private static void CreateVehicle(string i_VehicleModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles i_Vehicle)
        {
            bool isElectricVehicle;
            switch (i_Vehicle)
            {
                case eVehicles.Car:
                    isElectricVehicle = false;
                    CreateCar(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.Car);
                    break;
                case eVehicles.ElectricCar:
                    isElectricVehicle = true;
                    CreateCar(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.ElectricCar);
                    break;
                case eVehicles.Motorcycle:
                    isElectricVehicle = false;
                    CreateMotorcycle(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.Motorcycle);
                    break;
                case eVehicles.ElectricMotorcycle:
                    isElectricVehicle = true;
                    CreateMotorcycle(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.ElectricMotorcycle);
                    break;
                case eVehicles.Truck:
                    CreateTruck(i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.Truck);
                    break;
                default:
                    Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(2000);
                    break;
            }
        }

        private static void CreateCar(bool i_IsElectric, string i_CarModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles vehicle)
        {
            float energyLeft;
            eCarColors color;
            eNumOfDoors numOfDoors;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            energyLeft = EnterEnergyLeft(i_IsElectric, vehicle, i_IsElectric ? k_ElectricCarMaxBattery : k_CarMaxFuel);
            color = (eCarColors) DisplayEnumOptions(typeof(eCarColors), MessagesEnglish.k_GetColorMessage);

            numOfDoors = (eNumOfDoors)DisplayEnumOptions(typeof(eNumOfDoors), MessagesEnglish.k_GetNumDoorsMessage);

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, myGarage.CarMaxAirPressure);
            if (i_IsElectric)
            {
                ElectricCar newElectricCar = GarageLogic.CreateVehicle.CreateElectricCar(i_CarModel, i_LicencePlate, energyLeft, color,
                    numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                myGarage.AddVehicleToGarage(newElectricCar);
            }
            else
            {
                Car newCar = GarageLogic.CreateVehicle.CreateCar(i_CarModel, i_LicencePlate, energyLeft, color, numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                myGarage.AddVehicleToGarage(newCar);
            }
        }


        private static void CreateMotorcycle(bool i_IsElectric, string i_MotorcycleModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles i_Vehicle)
        {
            float energyLeft;
            int validEngineVolume;
            float wheelsCurrentAirPressure;
            string wheelMaker;
            eMotorcycleLicenceType licenceType;

            energyLeft = EnterEnergyLeft(i_IsElectric, i_Vehicle, i_IsElectric ? k_ElectricMotorcycleMaxBattery : k_MotorcycleMaxFuel);

            licenceType = (eMotorcycleLicenceType) DisplayEnumOptions(typeof(eMotorcycleLicenceType), MessagesEnglish.k_GetLicenseTypeMessage);

            validEngineVolume = EnterEngineVolume();

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, myGarage.MotorcycleMaxAirPressure);

            if (i_IsElectric)
            {
                ElectricMotorcycle newElectricMotorcycle = GarageLogic.CreateVehicle.CreateElectricMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, licenceType,
                    validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                myGarage.AddVehicleToGarage(newElectricMotorcycle);
            }
            else
            {
                Motorcycle newMotorcycle = GarageLogic.CreateVehicle.CreateMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, licenceType,
                    validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                myGarage.AddVehicleToGarage(newMotorcycle);
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

            fuelLeft = EnterEnergyLeft(isElectric, i_Vehicle, k_TruckMaxFuel);

            dangerousMaterials = EnterDangerousMaterialsInput();

            CargoVolume = EnterCargoVolume();

            GetWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, myGarage.TruckMaxAirPressure);

            Truck newTruck = GarageLogic.CreateVehicle.CreateTruck(i_TruckModel, i_LicencePlate, fuelLeft, dangerousMaterials, CargoVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
            myGarage.AddVehicleToGarage(newTruck);
        }

        //Energy can be fuel or battery
        private static float EnterEnergyLeft(bool i_IsElectric, eVehicles i_Vehicle, float i_MaxEnergyOrFuelLeft)
        {
            string energyLeftInput;
            float energyLeft;

            Console.WriteLine(MessagesEnglish.k_GetFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle, i_MaxEnergyOrFuelLeft);
            energyLeftInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyLeftInput, out energyLeft) || (energyLeft > i_MaxEnergyOrFuelLeft))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_GetFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle, i_MaxEnergyOrFuelLeft);
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
                    Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                    Console.WriteLine(MessagesEnglish.k_GetEngineVolumeMessage);
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
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_DangerMaterialMessage);
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
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_GetCargoVolumeMessage);
                cargoVolumeInput = Console.ReadLine();
            }

            return validCargoVolume;
        }


        private static int DisplayEnumOptions(Type i_Enum, string i_EnumMessage)
        {
            int index;
            string userInput;
            bool isInvalidInput = false; //true means that the input by the user was invalid
            int validUerInput;

                if (i_Enum.IsEnum)
            {
                int length = i_Enum.GetEnumNames().Length;
                do
                {
                    index = 1;
                    if (isInvalidInput)
                    {
                        Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                    }

                    Console.WriteLine(i_EnumMessage);
                    foreach (string item in i_Enum.GetEnumNames())
                    {
                        Console.WriteLine(MessagesEnglish.k_DisplayEnumLine, index, item);
                        index++;
                    }

                    Console.WriteLine(MessagesEnglish.k_ChooseVehicleMessage);
                    userInput = Console.ReadLine();
                    if (!InputValidation.IsValidEnumInput(userInput, length, out validUerInput))
                    {
                        isInvalidInput = true;
                    }
                    else
                    {
                        isInvalidInput = false;
                    }

                } while (isInvalidInput);
            }
            else
            {
                validUerInput = 0;
                Console.WriteLine(MessagesEnglish.k_NotAnEnumType);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(2000);
                DisplayGarageMenu();
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
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_GetWheelMakerMessage);
                o_WheelMaker = Console.ReadLine();
            }

            Console.WriteLine(MessagesEnglish.k_GetCurrentAirPressureMessage, i_MaxAirPressure);
            currentAirPressureInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(currentAirPressureInput, out o_CurrentAirPressure) || i_MaxAirPressure < o_CurrentAirPressure)
            {
                Console.WriteLine(i_MaxAirPressure < o_CurrentAirPressure ? MessagesEnglish.k_InvalidInputMessage : MessagesEnglish.k_InvalidInputMessage);
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
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_FilterByStatusMessage);
                userInput = Console.ReadLine();
            }

            Ex02.ConsoleUtils.Screen.Clear();

            if (InputValidation.isZeroChar(userInput[0]))
            {
                myGarage.PrintVehiclesInGarage();
            }
            else
            {
                userStatus = (eVehicleStatus) DisplayEnumOptions(typeof(eVehicleStatus), MessagesEnglish.k_GetStatusFilterMessage);
                myGarage.PrintVehiclesInGarage(userStatus);
            }
          
            Console.WriteLine(Environment.NewLine + MessagesEnglish.k_PressButtonToGoToMainMenuMessage);
            Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void ChangeVehicleStatus(string i_LicencePlate)
        {

            if (myGarage.CheckIfVehicleInGarage(i_LicencePlate) == myGarage.VehicleNotInGarage)
            {
                NotRegisteredVehiclesMessage();
            }
            else
            {
                myGarage.ChangeVehicleStatus(i_LicencePlate, (eVehicleStatus) DisplayEnumOptions(typeof(eVehicleStatus), MessagesEnglish.k_GetNewStatusMessage));
                Console.WriteLine(MessagesEnglish.k_StatusChangedMessage + MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(1500);
            }
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void FillAir(string i_LicencePlate)
        {
            int indexOfVehicleInGarage;

            if ((indexOfVehicleInGarage = myGarage.CheckIfVehicleInGarage(i_LicencePlate)) == myGarage.VehicleNotInGarage)
            {
                NotRegisteredVehiclesMessage();
            }
            else
            {
                float WheelsMaxAirPressure = myGarage.VehiclesInGarage[indexOfVehicleInGarage].Wheels[0].MaxAirPressure;
                float WheelsCurrentAirPressure = myGarage.VehiclesInGarage[indexOfVehicleInGarage].Wheels[0].CurrentAirPressure;
                if (WheelsMaxAirPressure.Equals(WheelsCurrentAirPressure))
                {
                    Console.WriteLine(MessagesEnglish.k_MaxAirPressureInWheelsMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(2000);
                }
                else
                {
                    try
                    {
                        myGarage.FillAir(i_LicencePlate);
                    }
                    catch(ValueOutOfRangeException vore)
                    {
                        Console.WriteLine(vore.Message);
                        Thread.Sleep(3000);
                        Ex02.ConsoleUtils.Screen.Clear();
                        FillAir(i_LicencePlate);
                    }
                    Console.WriteLine(MessagesEnglish.k_WheelsInflatedMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1000);
                }
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void ReFuel(string i_LicencePlate)
        {
            float ValidLittersOfFuelToAdd;
            eFuelTypes fuelType;
            int indexOfVehicleInGarage;

            if ((indexOfVehicleInGarage = myGarage.CheckIfVehicleInGarage(i_LicencePlate)) == myGarage.VehicleNotInGarage)
            {
                NotRegisteredVehiclesMessage();
            }
            else
            {
                if (myGarage.VehiclesInGarage[indexOfVehicleInGarage] is ElectricVehicle)
                {
                    throw new ArgumentException("NOT a fuel vehicle");
                }

                FuelVehicle feulVehicle = myGarage.VehiclesInGarage[indexOfVehicleInGarage] as FuelVehicle;
                if (feulVehicle.FuelTankCapacity.Equals(feulVehicle.FuelLeft))
                {
                    Console.WriteLine(MessagesEnglish.k_FullTankMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1500);
                }
                else
                {
                    ValidLittersOfFuelToAdd = EnterEnergyToFill(MessagesEnglish.k_RefuelAmountMessage);
                    fuelType = (eFuelTypes)DisplayEnumOptions(typeof(eFuelTypes), MessagesEnglish.k_GetFuelTypeMessage);
                    try
                    {
                        myGarage.Refuel(i_LicencePlate, ValidLittersOfFuelToAdd, fuelType);
                        Console.WriteLine(MessagesEnglish.k_VehicleRefueledMessage);
                        Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                        Thread.Sleep(1000);
                    }
                    catch (ValueOutOfRangeException vore)
                    {
                        Console.WriteLine(vore.Message);
                        Thread.Sleep(3000);
                        Ex02.ConsoleUtils.Screen.Clear();
                        ReFuel(i_LicencePlate);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Thread.Sleep(1500);
                        Ex02.ConsoleUtils.Screen.Clear();
                        ReFuel(i_LicencePlate);
                    }
                }
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }
        private static void FillBattery(string i_LicencePlate)
        {
            float ValidBatteryHours;
            int indexOfVehicleInGarage;
            if ((indexOfVehicleInGarage = myGarage.CheckIfVehicleInGarage(i_LicencePlate)) == myGarage.VehicleNotInGarage)
            {
                NotRegisteredVehiclesMessage();
            }
            else
            {
                if (myGarage.VehiclesInGarage[indexOfVehicleInGarage] is FuelVehicle)
                {
                    throw new ArgumentException("NOT an Electric vehicle");
                }

                ElectricVehicle electricVehicle = myGarage.VehiclesInGarage[indexOfVehicleInGarage] as ElectricVehicle;
                if(electricVehicle.BatteryHourCapacity.Equals(electricVehicle.BatteryLeft))
                {
                    Console.WriteLine(MessagesEnglish.k_BatteryFullyChargedMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1500);
                }
                else
                {

                    ValidBatteryHours = EnterEnergyToFill(MessagesEnglish.k_BatteryToChargeMessage);

                    try
                    {
                        myGarage.FillBattery(i_LicencePlate, ValidBatteryHours);
                        Console.WriteLine(MessagesEnglish.k_VehicleRechargedMessage);
                        Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                        Thread.Sleep(1000);
                    }
                    catch (ValueOutOfRangeException vore)
                    {
                        Console.WriteLine(vore.Message);
                        Thread.Sleep(3000);
                        Ex02.ConsoleUtils.Screen.Clear();
                        FillBattery(i_LicencePlate);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message + Environment.NewLine + MessagesEnglish.k_GoingBackToMainMenuMessage);
                        Thread.Sleep(1000);
                    }
                }
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void DisplayVehicleDetails(string i_LicencePlate)
        {

            if (myGarage.CheckIfVehicleInGarage(i_LicencePlate) == myGarage.VehicleNotInGarage)
            {
                NotRegisteredVehiclesMessage();
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                myGarage.PrintVehicleDetails(i_LicencePlate);
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
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_LicensePlateRequestMessage);
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

        private static void NotRegisteredVehiclesMessage()
        {
            Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
            Thread.Sleep(1000);
            Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
            Thread.Sleep(500);
        }

        // Energy can be fuel or battery
        private static float EnterEnergyToFill(string i_BatteryOrFuelMessage)
        {
            string energyToFill;
            float validEnergyInput;

            Console.WriteLine(i_BatteryOrFuelMessage);
            energyToFill = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyToFill, out validEnergyInput))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(i_BatteryOrFuelMessage);
                energyToFill = Console.ReadLine();
            }

            return validEnergyInput;
        }
    }
}
