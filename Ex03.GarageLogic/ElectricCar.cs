﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        internal string m_Color;
        internal int m_NumOfDoors;

        public ElectricCar(string i_VehicleModel, string i_LicencePlate, float i_EnergyLeft, float i_MaxBattery, string i_Color, byte i_NumOfWheels, int i_NumOfDoors) :
            base(i_VehicleModel, i_LicencePlate, i_EnergyLeft, i_MaxBattery, i_NumOfWheels)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + String.Format(@"Vehicle color is: {0}
Vehicle has {1} doors", m_Color, m_NumOfDoors);
        }
    }
}
