using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eMenu
    {
        [Description("Add new Vehicle")]
        AddNewVehicle = 1,
        [Description("Display vehicles in garage")]
        DisplayVehiclesInGarage,
        [Description("Change car status")]
        ChangeCarStatus,
        [Description("Fill air")]
        FillAir,
        [Description("Refuel")]
        Refuel,
        [Description("Rcharge")]
        Recharge,
        [Description("Display vehicle details")]
        DisplayVehicleDetails
    }
}
