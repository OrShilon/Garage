using Ex03.GarageLogic;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public static class InputValidation
    {
        public static bool IsValidEnumInput(string i_UserInput, int i_EnumLength, out int i_ValidUserInput)
        {
            int.TryParse(i_UserInput, out i_ValidUserInput);

            return i_ValidUserInput <= i_EnumLength && i_ValidUserInput > 0;
        }

        public static bool IsValidVehicleChoice(string i_UserChoice)
        {
            bool isValidInput = false;

            if (i_UserChoice.Length != 1)
            {
                isValidInput = false;
            }
            else
            {
                //צריך לשנות את '0' ו '6' להיות קונסט כי אי אפשר לשים רק מספר
                if (i_UserChoice[0] > '0' && i_UserChoice[0] < '6')
                {
                    isValidInput = true;
                }
            }

            return isValidInput;
        }


        public static bool IsValidVehicleModel(string i_VehicleModel)
        {
            return !i_VehicleModel.Equals(string.Empty);
        }

        public static bool IsValidEngineVolume(string i_EngineVolume, out int i_ValidEngineVolume)
        {
            return int.TryParse(i_EngineVolume, out i_ValidEngineVolume) && i_ValidEngineVolume > 0;
        }

        public static bool IsValidPhoneNumber(string i_PhoneNumber)
        {
            int phoneNumber;
            return int.TryParse(i_PhoneNumber, out phoneNumber);
        }

        public static bool IsValidDangerousMaterialsInput(string i_IsDangerous, out bool i_DangerousMaterials)
        {
            int isDangerous;
            bool isValidInput = false;
            i_DangerousMaterials = false;
            if (!int.TryParse(i_IsDangerous, out isDangerous))
            {
                i_DangerousMaterials = false;
            }
            else
            {
                //need to change to const
                if (isDangerous == 0 || isDangerous == 1)
                {
                    isValidInput = true;
                    if (isDangerous == 1)
                    {
                        i_DangerousMaterials = true;
                    }
                    else
                    {
                        i_DangerousMaterials = false;
                    }
                }
            }

            return isValidInput;
        }

        public static bool IsEmptyInput(string i_UserInput)
        {
            return i_UserInput.Equals(string.Empty);
        }

        public static bool IsValidFloatInput(string i_UnerInput, out float o_ValidFloat)
        {
            return float.TryParse(i_UnerInput, out o_ValidFloat) && o_ValidFloat > 0;
        }

        public static bool IsValidStatusInput(string i_StatusInput)
        {
            return i_StatusInput[0].Equals('0') || i_StatusInput[0].Equals('1');
        }


    }
}
