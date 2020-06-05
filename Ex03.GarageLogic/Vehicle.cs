using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        internal string m_VehicleModel;
        public string m_LicencePlate;
        internal float m_FuelOrBatteryLeft;
        internal float m_MaxFuelOrBattery;
        internal Wheel[] m_Wheels;
    }


}
