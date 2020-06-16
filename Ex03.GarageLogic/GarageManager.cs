using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly List<Vehicle> r_AllVehiclesInGarage;
        private readonly Dictionary<string, eVehicleStatus> r_VehiclesInGarageStatus;
        internal const int k_NotInGarage = -1;
        internal const int k_MininumRangeValue = 0;
        internal const int k_InvalidStatusInput = 0;
        internal const float k_CarMaxAirPressure = 32f;
        internal const float k_MotorcycleMaxAirPressure = 30f;
        internal const float k_TruckMaxAirPressure = 28f;
        internal const int k_EmptyGarage = 0;
        private int m_CountInRepair;
        private int m_CountFixed;
        private int m_CountPayed;

        public GarageManager()
        {
            r_AllVehiclesInGarage = new List<Vehicle>();
            r_VehiclesInGarageStatus = new Dictionary<string, eVehicleStatus>();
            m_CountInRepair = 0;
            m_CountFixed = 0;
            m_CountPayed = 0;
        }

        public void AddVehicleToGarage(Vehicle i_Vehicle)
        {
            bool isInGarage = false;

            foreach(Vehicle vehicle in r_AllVehiclesInGarage)
            {
                if (vehicle.Equals(i_Vehicle))
                {
                    Console.WriteLine("Your vehicle has already been registered before.");
                    ChangeVehicleStatus(i_Vehicle.LicencePlate, eVehicleStatus.InRepair);
                    isInGarage = true;
                    break;
                }
            }

            if (!isInGarage)
            {
                r_VehiclesInGarageStatus.Add(i_Vehicle.LicencePlate, eVehicleStatus.InRepair);
                r_AllVehiclesInGarage.Add(i_Vehicle);
                m_CountInRepair++;
            }
        }

        public Dictionary<string, eVehicleStatus> VehiclesStatusDictionary
        {
            get
            {
                return r_VehiclesInGarageStatus;
            }
        }

        public List<Vehicle> VehiclesInGarage
        {
            get
            {
                return r_AllVehiclesInGarage;
            }
        }

        public void PrintVehiclesInGarage()
        {
            if (r_VehiclesInGarageStatus.Count() == k_EmptyGarage)
            {
                Console.WriteLine("Sorry but there are no vehicles in the garage currently");
            }
            else
            {
                foreach (KeyValuePair<string, eVehicleStatus> vehicle in r_VehiclesInGarageStatus)
                {
                    Console.WriteLine("License plate number: " + vehicle.Key + ", status: " + vehicle.Value);
                }
            }
        }

        public void PrintVehiclesInGarage(eVehicleStatus i_status)
        {
            if (convertEnunStatusToInt(i_status) <= k_InvalidStatusInput)
            {
                Console.WriteLine("Sorry but there are no vehicles with this status in the garage currently");
            }
            else
            {
                foreach (KeyValuePair<string, eVehicleStatus> vehicle in r_VehiclesInGarageStatus)
                {
                    if (vehicle.Value.Equals(i_status))
                    {
                        Console.WriteLine("License plate number: " + vehicle.Key + ", status: " + vehicle.Value);
                    }
                }
            }
        }

        private int convertEnunStatusToInt(eVehicleStatus i_VehicleStatus)
        {
            int convertedStatus;

            switch (i_VehicleStatus)
            {
                case eVehicleStatus.InRepair:
                    convertedStatus = m_CountInRepair;
                    break;
                case eVehicleStatus.Fixed:
                    convertedStatus = m_CountFixed;
                    break;
                case eVehicleStatus.Payed:
                    convertedStatus = m_CountPayed;
                    break;
                default:
                    convertedStatus = k_InvalidStatusInput;
                    break;
            }

            return convertedStatus;
        }

        public void ChangeVehicleStatus(string i_LicensePlateNumber, eVehicleStatus i_NewVehicleStatus)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);

            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }

            eVehicleStatus oldStatus = r_VehiclesInGarageStatus[i_LicensePlateNumber];
            if (!oldStatus.Equals(i_NewVehicleStatus))
            {
                switch (oldStatus)
                {
                    case eVehicleStatus.InRepair:
                        m_CountInRepair--;
                        break;
                    case eVehicleStatus.Fixed:
                        m_CountFixed--;
                        break;
                    case eVehicleStatus.Payed:
                        m_CountPayed--;
                        break;
                }

                switch (i_NewVehicleStatus)
                {
                    case eVehicleStatus.InRepair:
                        m_CountInRepair++;
                        break;
                    case eVehicleStatus.Fixed:
                        m_CountFixed++;
                        break;
                    case eVehicleStatus.Payed:
                        m_CountPayed++;
                        break;
                }

                r_VehiclesInGarageStatus[i_LicensePlateNumber] = i_NewVehicleStatus;
            } 
        }

        public void PrintVehicleDetails(string i_LicencePlate)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicencePlate);

            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }
            else
            {
                Console.WriteLine(r_AllVehiclesInGarage[vehicleLocation].ToString() + Environment.NewLine + "Vehicle status is: " + r_VehiclesInGarageStatus[i_LicencePlate]);
            }
        }

        public void FillAir(string i_LicensePlateNumber)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);

            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }

            Vehicle customerVehicle = r_AllVehiclesInGarage[vehicleLocation];
            float maxAirPressure = customerVehicle.Wheels[0].MaxAirPressure;
            customerVehicle.SetWheelPressure(maxAirPressure);
        }

        public void Refuel(string i_LicensePlateNumber, float i_HowMuchToFill, eFuelTypes i_FuelType)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);

            if(vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }

            FuelVehicle customerVehicle = r_AllVehiclesInGarage[vehicleLocation] as FuelVehicle;
            if(customerVehicle == null)
            {
                throw new ArgumentException("NOT a fuel vehicle");
            }

            float newFuelLeft = customerVehicle.FuelLeft + i_HowMuchToFill;
            float calculatedMaximumFuelCapacity = customerVehicle.MaxFuelTankCapacity - customerVehicle.FuelLeft;
            if (newFuelLeft > customerVehicle.MaxFuelTankCapacity)
            {
                throw new ValueOutOfRangeException(calculatedMaximumFuelCapacity, k_MininumRangeValue);
            }

            if (i_FuelType != customerVehicle.FuelType)
            {
                throw new ArgumentException("fuel type is NOT valid");
            }

            customerVehicle.FuelLeft = newFuelLeft;
        }

        public void Recharge(string i_LicensePlateNumber, float i_HowMuchToFill)
        {
            int vehicleLocation = CheckIfVehicleInGarage(i_LicensePlateNumber);

            if (vehicleLocation == k_NotInGarage)
            {
                throw new ArgumentException("Vehicle is NOT in garage");
            }

            ElectricVehicle customerVehicle = r_AllVehiclesInGarage[vehicleLocation] as ElectricVehicle;
            if (customerVehicle == null)
            {
                throw new ArgumentException("NOT an Electric vehicle");
            }

            float newBatteryLeft = customerVehicle.BatteryLeft + i_HowMuchToFill;
            float calculatedMaximumBatteryHours = customerVehicle.BatteryHourCapacity - customerVehicle.BatteryLeft;
            if (newBatteryLeft > customerVehicle.BatteryHourCapacity)
            {
                throw new ValueOutOfRangeException(calculatedMaximumBatteryHours, k_MininumRangeValue);
            }

            customerVehicle.BatteryLeft = newBatteryLeft;
        }

        // Checks if the given vehicle is in the garage. If so returns its location in the list, else not returns -1
        public int CheckIfVehicleInGarage(string i_LicensePlateNumber)
        {
            int vehicleLocation = k_NotInGarage;

            for(int i = 0; i < r_AllVehiclesInGarage.Count; i++)
            {
                if (r_AllVehiclesInGarage[i].LicencePlate == i_LicensePlateNumber)
                {
                    vehicleLocation = i;
                    break;
                }
            }

            return vehicleLocation;
        }

        public float CarMaxAirPressure
        {
            get
            {
                return k_CarMaxAirPressure;
            }
        }

        public float MotorcycleMaxAirPressure
        {
            get
            {
                return k_MotorcycleMaxAirPressure;
            }
        }

        public float TruckMaxAirPressure
        {
            get
            {
                return k_TruckMaxAirPressure;
            }
        }

        public int VehicleNotInGarage
        {
            get
            {
                return k_NotInGarage;
            }
        }
    }
}
