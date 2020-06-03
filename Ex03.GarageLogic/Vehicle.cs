using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract class Vehicle
    {
        internal string m_VehicleModel;
        internal string m_LicencePlate;
        internal float m_FuelOrBatteryLeft;
        internal float m_MaxFuelOrBattery;//
        internal Wheel[] m_Wheels;
    }


}
