using Ex03.GarageLogic;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class InputValidation
    {
        private const char k_NotFilterByStatus = '0';
        private const char k_FilterByStatus = '1';
        private const int k_NotDangerousMaterials = 0;
        private const int k_YesDangerousMaterials = 1;
        private const int k_InputLengthOne = 1;
        private const int k_CheckIfPositiveIntNumber = 0;
        private const float k_CheckIfPositiveFloatNumber = 0f;

        internal static bool IsValidEnumInput(string i_UserInput, int i_EnumLength, out int o_ValidUserInput)
        {
            int.TryParse(i_UserInput, out o_ValidUserInput);

            return o_ValidUserInput <= i_EnumLength && IsPositiveNumber(o_ValidUserInput);
        }

        internal static bool IsValidEngineVolume(string i_EngineVolume, out int o_ValidEngineVolume)
        {
            if (!int.TryParse(i_EngineVolume, out o_ValidEngineVolume))
            {
                throw new FormatException(MessagesEnglish.k_InvalidInputMessage);
            }
            return IsPositiveNumber(o_ValidEngineVolume);
        }

        internal static bool IsValidPhoneNumber(string i_PhoneNumber)
        {
            uint phoneNumber;

            return uint.TryParse(i_PhoneNumber, out phoneNumber);
        }

        internal static bool IsValidDangerousMaterialsInput(string i_IsDangerous, out bool o_DangerousMaterials)
        {
            int isDangerous;
            bool isValidInput = false;

            o_DangerousMaterials = false;
            if (!int.TryParse(i_IsDangerous, out isDangerous))
            {
                o_DangerousMaterials = false;
            }
            else
            {
                if (isDangerous == k_NotDangerousMaterials || isDangerous == k_YesDangerousMaterials)
                {
                    isValidInput = true;
                    if (isDangerous == k_YesDangerousMaterials)
                    {
                        o_DangerousMaterials = true;
                    }
                    else
                    {
                        o_DangerousMaterials = false;
                    }
                }
            }

            return isValidInput;
        }

        internal static bool IsEmptyInput(string i_UserInput)
        {
            return i_UserInput.Equals(string.Empty);
        }

        internal static bool IsValidFloatInput(string i_UserInput, out float o_ValidFloat)
        {
            return float.TryParse(i_UserInput, out o_ValidFloat) && IsPositiveNumber(o_ValidFloat);
        }

        internal static bool IsValidStatusFilterInput(string i_StatusInput)
        {
            bool isValidStatusFliter = false;

            if(i_StatusInput.Length == k_InputLengthOne)
            {
                if(i_StatusInput[0].Equals(k_NotFilterByStatus) || i_StatusInput[0].Equals(k_FilterByStatus))
                {
                    isValidStatusFliter = true;
                }
            }
            return isValidStatusFliter;
        }

        internal static bool IsPositiveNumber(int i_Number)
        {
            return i_Number > k_CheckIfPositiveIntNumber;
        }

        internal static bool IsPositiveNumber(float i_Number)
        {
            return i_Number > k_CheckIfPositiveFloatNumber;
        }

        internal static bool NotFilterByStatus(char i_UserInput)
        {
            return i_UserInput.Equals(k_NotFilterByStatus);
        }
    }
}
