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
        private static GarageManager m_MyGarage = new GarageManager();
        private const float k_CarMaxFuel = 60f;
        private const float k_ElectricCarMaxBattery = 2.1f;
        private const float k_ElectricMotorcycleMaxBattery = 1.2f;
        private const float k_MotorcycleMaxFuel = 7f;
        private const float k_TruckMaxFuel = 120f;

        internal static void Welcome()
        {
            Console.WriteLine(MessagesEnglish.k_GarageHelloMessage, Environment.NewLine);
            Thread.Sleep(1200);
            displayGarageMenu();
        }

        private static void displayGarageMenu()
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
                    licencePlate = enterLicencePlate();
                }

                sentToUserChoice(licencePlate, userOption);
            }
        }

        private static void sentToUserChoice(string i_LicencePlate, eMenu i_UserOption)
        {
            try
            {
                switch (i_UserOption)
                {
                    case eMenu.AddNewVehicle:
                        addNewVehicle(i_LicencePlate);
                        break;
                    case eMenu.DisplayVehiclesInGarage:
                        displayVehiclesInGarage();
                        break;
                    case eMenu.ChangeCarStatus:
                        changeVehicleStatus(i_LicencePlate);
                        break;
                    case eMenu.FillAir:
                        fillAir(i_LicencePlate);
                        break;
                    case eMenu.Refuel:
                        reFuel(i_LicencePlate);
                        break;
                    case eMenu.Recharge:
                        reCharge(i_LicencePlate);
                        break;
                    case eMenu.DisplayVehicleDetails:
                        displayVehicleDetails(i_LicencePlate);
                        break;
                    case eMenu.Exit:
                        exitProgram();
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
        private static void addNewVehicle(string i_LicencePlate)
        {
            eVehicles userVehicle;
            string vehicleModel;
            string ownerName;
            string ownerPhoneNumber;

            if (m_MyGarage.VehiclesStatusDictionary.ContainsKey(i_LicencePlate))
            {
                Console.WriteLine(MessagesEnglish.k_VehicleIsRegistered);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
            }
            else
            {
                enterOwnerDetails(out ownerName, out ownerPhoneNumber);
                VehicleOwner owner = new VehicleOwner(ownerName, ownerPhoneNumber);
                userVehicle = (eVehicles) displayEnumOptions(typeof(eVehicles), MessagesEnglish.k_GetVehicleTypeMessage);
                enterVehicleModel(out vehicleModel);
                createVehicle(vehicleModel, i_LicencePlate, owner, userVehicle);
                Console.WriteLine(MessagesEnglish.k_AddedNewCarMessage);
                Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
            }

            Thread.Sleep(3000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void enterOwnerDetails(out string o_OwnerName, out string o_PhoneNumber)
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

        private static void enterVehicleModel(out string o_VehicleModel)
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

        private static void createVehicle(string i_VehicleModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles i_Vehicle)
        {
            bool isElectricVehicle;

            switch (i_Vehicle)
            {
                case eVehicles.Car:
                    isElectricVehicle = false;
                    createCar(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.Car);
                    break;
                case eVehicles.ElectricCar:
                    isElectricVehicle = true;
                    createCar(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.ElectricCar);
                    break;
                case eVehicles.Motorcycle:
                    isElectricVehicle = false;
                    createMotorcycle(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.Motorcycle);
                    break;
                case eVehicles.ElectricMotorcycle:
                    isElectricVehicle = true;
                    createMotorcycle(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.ElectricMotorcycle);
                    break;
                case eVehicles.Truck:
                    isElectricVehicle = false;
                    createTruck(isElectricVehicle, i_VehicleModel, i_LicencePlate, i_VehicleOwner, eVehicles.Truck);
                    break;
                default:
                    Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(2000);
                    break;
            }
        }

        private static void createCar(bool i_IsElectric, string i_CarModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles vehicle)
        {
            float energyLeft;
            eCarColors color;
            eNumOfDoors numOfDoors;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            energyLeft = enterEnergyLeft(i_IsElectric, vehicle, i_IsElectric ? k_ElectricCarMaxBattery : k_CarMaxFuel);
            color = (eCarColors) displayEnumOptions(typeof(eCarColors), MessagesEnglish.k_GetColorMessage);

            numOfDoors = (eNumOfDoors)displayEnumOptions(typeof(eNumOfDoors), MessagesEnglish.k_GetNumDoorsMessage);

            getWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, m_MyGarage.CarMaxAirPressure);
            if (i_IsElectric)
            {
                ElectricCar newElectricCar = CreateVehicle.CreateElectricCar(i_CarModel, i_LicencePlate, energyLeft, color,
                    numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                m_MyGarage.AddVehicleToGarage(newElectricCar);
            }
            else
            {
                Car newCar = CreateVehicle.CreateCar(i_CarModel, i_LicencePlate, energyLeft, color, numOfDoors, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                m_MyGarage.AddVehicleToGarage(newCar);
            }
        }


        private static void createMotorcycle(bool i_IsElectric, string i_MotorcycleModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles i_Vehicle)
        {
            float energyLeft;
            int validEngineVolume;
            float wheelsCurrentAirPressure;
            string wheelMaker;
            eMotorcycleLicenceType licenceType;

            energyLeft = enterEnergyLeft(i_IsElectric, i_Vehicle, i_IsElectric ? k_ElectricMotorcycleMaxBattery : k_MotorcycleMaxFuel);

            licenceType = (eMotorcycleLicenceType) displayEnumOptions(typeof(eMotorcycleLicenceType), MessagesEnglish.k_GetLicenseTypeMessage);

            validEngineVolume = enterEngineVolume();

            getWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, m_MyGarage.MotorcycleMaxAirPressure);

            if (i_IsElectric)
            {
                ElectricMotorcycle newElectricMotorcycle = CreateVehicle.CreateElectricMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, licenceType,
                    validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                m_MyGarage.AddVehicleToGarage(newElectricMotorcycle);
            }
            else
            {
                Motorcycle newMotorcycle = CreateVehicle.CreateMotorcycle(i_MotorcycleModel, i_LicencePlate, energyLeft, licenceType,
                    validEngineVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
                m_MyGarage.AddVehicleToGarage(newMotorcycle);
            }
        }

        private static void createTruck(bool i_IsElectric, string i_TruckModel, string i_LicencePlate, VehicleOwner i_VehicleOwner, eVehicles i_Vehicle)
        {
            float fuelLeft;
            float CargoVolume;
            bool dangerousMaterials;
            float wheelsCurrentAirPressure;
            string wheelMaker;

            fuelLeft = enterEnergyLeft(i_IsElectric, i_Vehicle, k_TruckMaxFuel);
            dangerousMaterials = enterDangerousMaterialsInput();
            CargoVolume = enterCargoVolume();
            getWheelInformation(out wheelMaker, out wheelsCurrentAirPressure, m_MyGarage.TruckMaxAirPressure);
            Truck newTruck = CreateVehicle.CreateTruck(i_TruckModel, i_LicencePlate, fuelLeft, dangerousMaterials, CargoVolume, wheelMaker, wheelsCurrentAirPressure, i_VehicleOwner);
            m_MyGarage.AddVehicleToGarage(newTruck);
        }

        //Energy can be fuel or battery
        private static float enterEnergyLeft(bool i_IsElectric, eVehicles i_Vehicle, float i_MaxEnergyLeft)
        {
            string energyLeftInput;
            float validEnergyLeft;

            Console.WriteLine(MessagesEnglish.k_GetFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle, i_MaxEnergyLeft);
            energyLeftInput = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyLeftInput, out validEnergyLeft) || (validEnergyLeft > i_MaxEnergyLeft))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_GetFuelOrEnergyLeftMessage, i_IsElectric ? MessagesEnglish.k_BatteryMessage : MessagesEnglish.k_FuelMessage, i_Vehicle, i_MaxEnergyLeft);
                energyLeftInput = Console.ReadLine();
            }

            return validEnergyLeft;
        }

        private static int enterEngineVolume()
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
                enterEngineVolume();
            }

            return validEngineVolume;
        }

        private static bool enterDangerousMaterialsInput()
        {
            string dangerousMaterialsInput;
            bool validDangerousMaterials;

            Console.WriteLine(MessagesEnglish.k_DangerMaterialMessage);
            dangerousMaterialsInput = Console.ReadLine();
            while (!InputValidation.IsValidDangerousMaterialsInput(dangerousMaterialsInput, out validDangerousMaterials))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(MessagesEnglish.k_DangerMaterialMessage);
                dangerousMaterialsInput = Console.ReadLine();
            }

            return validDangerousMaterials;
        }

        private static float enterCargoVolume()
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


        private static int displayEnumOptions(Type i_Enum, string i_EnumMessage)
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
                displayGarageMenu();
            }


            Ex02.ConsoleUtils.Screen.Clear();
            return validUerInput;
        }

        private static void getWheelInformation(out string o_WheelMaker, out float o_CurrentAirPressure, float i_MaxAirPressure)
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

        private static void displayVehiclesInGarage()
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
            if (InputValidation.NotFilterByStatus(userInput[0]))
            {
                m_MyGarage.PrintVehiclesInGarage();
            }
            else
            {
                userStatus = (eVehicleStatus) displayEnumOptions(typeof(eVehicleStatus), MessagesEnglish.k_GetStatusFilterMessage);
                m_MyGarage.PrintVehiclesInGarage(userStatus);
            }
          
            Console.WriteLine(Environment.NewLine + MessagesEnglish.k_PressButtonToGoToMainMenuMessage);
            Console.ReadLine();
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void changeVehicleStatus(string i_LicencePlate)
        {

            if (m_MyGarage.CheckIfVehicleInGarage(i_LicencePlate) == m_MyGarage.VehicleNotInGarage)
            {
                notRegisteredVehiclesMessages();
            }
            else
            {
                m_MyGarage.ChangeVehicleStatus(i_LicencePlate, (eVehicleStatus) displayEnumOptions(typeof(eVehicleStatus), MessagesEnglish.k_GetNewStatusMessage));
                Console.WriteLine(MessagesEnglish.k_StatusChangedMessage + MessagesEnglish.k_GoingBackToMainMenuMessage);
                Thread.Sleep(1500);
            }

            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void fillAir(string i_LicencePlate)
        {
            int indexOfVehicleInGarage;

            if ((indexOfVehicleInGarage = m_MyGarage.CheckIfVehicleInGarage(i_LicencePlate)) == m_MyGarage.VehicleNotInGarage)
            {
                notRegisteredVehiclesMessages();
            }
            else
            {
                float WheelsMaxAirPressure = m_MyGarage.VehiclesInGarage[indexOfVehicleInGarage].Wheels[0].MaxAirPressure;
                float WheelsCurrentAirPressure = m_MyGarage.VehiclesInGarage[indexOfVehicleInGarage].Wheels[0].CurrentAirPressure;
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
                        m_MyGarage.FillAir(i_LicencePlate);
                    }
                    catch(ValueOutOfRangeException vore)
                    {
                        Console.WriteLine(vore.Message);
                        Thread.Sleep(3000);
                        Ex02.ConsoleUtils.Screen.Clear();
                        fillAir(i_LicencePlate);
                    }
                    Console.WriteLine(MessagesEnglish.k_WheelsInflatedMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1000);
                }
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static void reFuel(string i_LicencePlate)
        {
            float ValidLittersOfFuelToAdd;
            eFuelTypes fuelType;
            int indexOfVehicleInGarage;

            if ((indexOfVehicleInGarage = m_MyGarage.CheckIfVehicleInGarage(i_LicencePlate)) == m_MyGarage.VehicleNotInGarage)
            {
                notRegisteredVehiclesMessages();
            }
            else
            {
                if (m_MyGarage.VehiclesInGarage[indexOfVehicleInGarage] is ElectricVehicle)
                {
                    throw new ArgumentException("NOT a fuel vehicle");
                }

                FuelVehicle feulVehicle = m_MyGarage.VehiclesInGarage[indexOfVehicleInGarage] as FuelVehicle;
                if (feulVehicle.MaxFuelTankCapacity.Equals(feulVehicle.FuelLeft))
                {
                    Console.WriteLine(MessagesEnglish.k_FullTankMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1500);
                }
                else
                {
                    ValidLittersOfFuelToAdd = enterEnergyToFill(MessagesEnglish.k_RefuelAmountMessage);
                    fuelType = (eFuelTypes)displayEnumOptions(typeof(eFuelTypes), MessagesEnglish.k_GetFuelTypeMessage);
                    try
                    {
                        m_MyGarage.Refuel(i_LicencePlate, ValidLittersOfFuelToAdd, fuelType);
                        Console.WriteLine(MessagesEnglish.k_VehicleRefueledMessage);
                        Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                        Thread.Sleep(1000);
                    }
                    catch (ValueOutOfRangeException vore)
                    {
                        Console.WriteLine(vore.Message);
                        Thread.Sleep(3000);
                        Ex02.ConsoleUtils.Screen.Clear();
                        reFuel(i_LicencePlate);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                        Thread.Sleep(1500);
                        Ex02.ConsoleUtils.Screen.Clear();
                        reFuel(i_LicencePlate);
                    }
                }
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }
        private static void reCharge(string i_LicencePlate)
        {
            float ValidBatteryHours;
            int indexOfVehicleInGarage;

            if ((indexOfVehicleInGarage = m_MyGarage.CheckIfVehicleInGarage(i_LicencePlate)) == m_MyGarage.VehicleNotInGarage)
            {
                notRegisteredVehiclesMessages();
            }
            else
            {
                if (m_MyGarage.VehiclesInGarage[indexOfVehicleInGarage] is FuelVehicle)
                {
                    throw new ArgumentException("NOT an Electric vehicle");
                }

                ElectricVehicle electricVehicle = m_MyGarage.VehiclesInGarage[indexOfVehicleInGarage] as ElectricVehicle;
                if(electricVehicle.BatteryHourCapacity.Equals(electricVehicle.BatteryLeft))
                {
                    Console.WriteLine(MessagesEnglish.k_BatteryFullyChargedMessage);
                    Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                    Thread.Sleep(1500);
                }
                else
                {

                    ValidBatteryHours = enterEnergyToFill(MessagesEnglish.k_BatteryToChargeMessage);

                    try
                    {
                        m_MyGarage.Recharge(i_LicencePlate, ValidBatteryHours);
                        Console.WriteLine(MessagesEnglish.k_VehicleRechargedMessage);
                        Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
                        Thread.Sleep(1000);
                    }
                    catch (ValueOutOfRangeException vore)
                    {
                        Console.WriteLine(vore.Message);
                        Thread.Sleep(3000);
                        Ex02.ConsoleUtils.Screen.Clear();
                        reCharge(i_LicencePlate);
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

        private static void displayVehicleDetails(string i_LicencePlate)
        {

            if (m_MyGarage.CheckIfVehicleInGarage(i_LicencePlate) == m_MyGarage.VehicleNotInGarage)
            {
                notRegisteredVehiclesMessages();
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                m_MyGarage.PrintVehicleDetails(i_LicencePlate);
                Console.WriteLine(Environment.NewLine + MessagesEnglish.k_PressButtonToGoToMainMenuMessage);
                Console.ReadLine();
            }

            Thread.Sleep(1000);
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private static string enterLicencePlate()
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

        private static void exitProgram()
        {
            Console.WriteLine(MessagesEnglish.k_ExitMessage);
            Thread.Sleep(1500);
            Environment.Exit(0);
        }

        private static void notRegisteredVehiclesMessages()
        {
            Console.WriteLine(MessagesEnglish.k_VehicleNotRegisteredMessage);
            Thread.Sleep(1000);
            Console.WriteLine(MessagesEnglish.k_GoingBackToMainMenuMessage);
            Thread.Sleep(500);
        }

        // Energy can be fuel or battery
        private static float enterEnergyToFill(string i_EnergyMessage)
        {
            string energyToFill;
            float validEnergyInput;

            Console.WriteLine(i_EnergyMessage);
            energyToFill = Console.ReadLine();
            while (!InputValidation.IsValidFloatInput(energyToFill, out validEnergyInput))
            {
                Console.WriteLine(MessagesEnglish.k_InvalidInputMessage);
                Console.WriteLine(i_EnergyMessage);
                energyToFill = Console.ReadLine();
            }

            return validEnergyInput;
        }
    }
}
