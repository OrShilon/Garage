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
        public static void PrintVehiclesInGarage()
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

        private eCustomerRequest GarageOptionsForCustomer()
        {
            string messege = string.Format(@"1) I would like to check in a new vehicle
2) Display the current license plates that are in the garage
3) Change my vehicle status
4) Fill air in my vehicle wheels
5) Refuel my vehicle
6) Recharge my vehicle
7) Display my vehicle details
Type in the corresponding number to your visit purpose please");


            return eCustomerRequest.AddmitingNewCar;
        }
    }
}
