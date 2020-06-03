using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract class FuelVehicle : Vehicle
    {
        internal eFuelTypes m_FuelType;
    }

    enum eFuelTypes
    {
        Octan95,
        Octan96,//
        Octan98,
        Soler
    }
}
