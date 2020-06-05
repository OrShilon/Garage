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
        public static void PrintVehiclesInGarage()
        {
            List<Vehicle> carsList = GarageManager.CarsList;
            if(carsList.Count != 0)
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
    }
}
