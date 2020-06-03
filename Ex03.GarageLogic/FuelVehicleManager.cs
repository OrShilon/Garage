using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal static class FuelVehicleManager
    {
        public static float GetFuelLeft(Vehicle i_vehicle)
        {
            return i_vehicle.m_FuelOrBatteryLeft;
            Console.WriteLine();
            Console.WriteLine();
        }

        public static float GetMaxFuelPossible(Vehicle i_vehicle)
        {
            return i_vehicle.m_MaxFuelOrBattery;
        }

        public static void Refuel(FuelVehicle i_FuelVehicle, float i_LittersToAdd, int i_FuelType)
        {
            switch(i_FuelVehicle.m_FuelType)
            {
                case eFuelTypes.Octan95:
                    //add fuel.....כככככככ//
                    break;
                case eFuelTypes.Octan96:
                    //add fuel.....
                    break;
                case eFuelTypes.Octan98:
                    //add fuel.....
                    break;
                case eFuelTypes.Soler:
                    //add fuel.....למה היה שם 2 עם אותו
                    break;
                default:
                    //not a match! return exeption that the type of fuel is not match
                    break;
            }
        }
        private static void AddFuel(FuelVehicle i_FuelVehicle, float i_LittersToAdd, int i_FuelType)
        {

            //need to check if i have enough space in my tank!!!
            i_FuelVehicle.m_FuelOrBatteryLeft += i_LittersToAdd;
        }
    }
}
