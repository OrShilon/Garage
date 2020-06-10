using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eVehicleStatus
    {
      [Description("in repair")]
      InRepair = 1,
      Fixed,
      Payed
    }
}
