using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    enum eVehicles
    {
        Car = 1,
        [Description("Electric car")]
        ElectricCar,
        Motorcycle,
        [Description("Electric motorcycle")]
        ElectricMotorcycle,
        Truck,
    }
    
   

}
