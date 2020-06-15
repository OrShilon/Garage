using Ex03.GarageLogic;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class InputValidation
    {
        internal const char k_ZeroChar = '0';
        internal const char k_OneChar = '1';
        internal const int k_ZeroInt = 0;
        internal const float k_ZeroFloat = 0f;
        internal const int k_OneInt = 1;

        public static bool IsValidEnumInput(string i_UserInput, int i_EnumLength, out int i_ValidUserInput)
        {
            int.TryParse(i_UserInput, out i_ValidUserInput);

            return i_ValidUserInput <= i_EnumLength && isPositiveNumber(i_ValidUserInput);
        }

        public static bool IsValidEngineVolume(string i_EngineVolume, out int i_ValidEngineVolume)
        {
            if (!int.TryParse(i_EngineVolume, out i_ValidEngineVolume))
            {
                throw new FormatException(MessagesEnglish.k_InvalidInputMessage);
            }
            return isPositiveNumber(i_ValidEngineVolume);
        }

        public static bool IsValidPhoneNumber(string i_PhoneNumber)
        {
            uint phoneNumber;

            return uint.TryParse(i_PhoneNumber, out phoneNumber);
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
                if (isDangerous == k_ZeroInt || isDangerous == k_OneInt)
                {
                    isValidInput = true;
                    if (isDangerous == k_OneInt)
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
            return float.TryParse(i_UnerInput, out o_ValidFloat) && isPositiveNumber(o_ValidFloat);
        }

        public static bool IsValidStatusFilterInput(string i_StatusInput)
        {
            bool isValidStatusFliter = false;

            if(i_StatusInput.Length == k_OneInt)
            {
                if(i_StatusInput[0].Equals(k_ZeroChar) || i_StatusInput[0].Equals(k_OneChar))
                {
                    isValidStatusFliter = true;
                }
            }
            return isValidStatusFliter;
        }

        private static bool isPositiveNumber(int i_Number)
        {
            return i_Number > k_ZeroInt;
        }

        private static bool isPositiveNumber(float i_Number)
        {
            return i_Number > k_ZeroFloat;
        }

    }
}
