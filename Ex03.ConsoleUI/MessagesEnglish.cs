﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class MessagesEnglish
    {
        internal const string k_GarageHelloMessage = "Hello and welcome to Or and Amit's garage.{0}";
        internal const string k_GarageOptionsMessage = @"What is the purpose of your visit?
1. I would like to check in a new vehicle
2. Display the current license plates that are in the garage
3. Change my vehicle status
4. Fill air in my vehicle wheels
5. Refuel my vehicle
6. Recharge my vehicle
7. Display my vehicle details
8. Exit program
Type in the corresponding number to your visit purpose please";

        internal const string k_InvalidInputMessage = "Invalid input.";
        internal const string k_LicensePlateRequestMessage = "please enter your licence plate:";
        internal const string k_VehicleIsRegistered = "Your vehicle is already in our garage!";
        internal const string k_GetNameMessage = "Please enter you name:";
        internal const string k_GetPhoneNumberMessage = "Please enter your phone number:";
        internal const string k_GetVehicleTypeMessage = "Please enter your vehicle type:"; 
        internal const string k_GetVehicleModelMessage = "Please enter your vehicle's model:";
        internal const string k_GetFuelOrEnergyLeftMessage = "Please enter how much {0} left in your {1} (maximum {2}):";
        internal const string k_BatteryMessage = "battery";
        internal const string k_FuelMessage = "fuel";
        internal const string k_GetColorMessage = "Please enter the color of your car:"; 
        internal const string k_GetNumDoorsMessage = "Please enter the number of doors your car has:";
        internal const string k_ExitMessage = "Exit program...";
        internal const string k_VehicleNotRegisteredMessage = "Sorry the given vehicle is not in the garage."; 
        internal const string k_BatteryToChargeMessage = "Please enter how much battery to fill:";
        internal const string k_RefuelAmountMessage = "Please enter how much fuel to fill:";
        internal const string k_GetNewStatusMessage = "What would you like the new status to be?";
        internal const string k_GetFuelTypeMessage = "Please enter your vehicle's fuel type.";
        internal const string k_FilterByStatusMessage = "Do you want to filter by status? Enter 1 for yes, 0 for no";
        internal const string k_GetStatusFilterMessage = "Please enter the wanted status to display";
        internal const string k_GetWheelMakerMessage = "Please enter the name of your wheel maker:";
        internal const string k_GetCurrentAirPressureMessage = "Please enter the currnet air pressure in your wheels (maximum {0}):";
        internal const string k_AirPressureAboveMAximumMessage = "You have entered a number above the maximum air pressure";
        internal const string k_DisplayEnumLine = "{0}. {1}";
        internal const string k_NotAnEnumType = "The input type is not an enum";
        internal const string k_ChooseVehicleMessage = "Type in the corresponding number to your vehicle please.";
        internal const string k_GetCargoVolumeMessage = "Please enter your truck's cargo volume:";
        internal const string k_DangerMaterialMessage = "Is there any dangerous materials in your truck? Enter 1 for yes, 0 for no.";
        internal const string k_GetEngineVolumeMessage = "Please enter your engine's volume (in Cc):";
        internal const string k_GetLicenseTypeMessage = "Please enter your licence type:";
        internal const string k_PressButtonToGoToMainMenuMessage = "Press any button to go back to the main menu.";
        internal const string k_GoingBackToMainMenuMessage = "going back to the main menu.";
        internal const string k_FullTankMessage = "Your vehicle's tank is full.";
        internal const string k_BatteryFullyChargedMessage = "Your vehicle's battery is fully charged";
        internal const string k_MaxAirPressureInWheelsMessage = "Your vehicle's wheels already have max air pressure";
        internal const string k_VehicleRefueledMessage = "Your vehicle has been refueled.";
        internal const string k_VehicleRechargedMessage = "Your vehicle has been recharged.";
        internal const string k_WheelsInflatedMessage = "Your car wheels have been inflated properly. ";
        internal const string k_StatusChangedMessage = "Your status has been changed. ";
        internal const string k_AddedNewCarMessage = "Your vehicle has been added properly.";
    }
}