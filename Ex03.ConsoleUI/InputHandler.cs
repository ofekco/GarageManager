using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;

namespace ConsoleUI
{
    public static class InputHandler
    {
        public static int GetOptionFromMenu(int i_NumberOfOptions)
        {
            int option = int.Parse(Console.ReadLine());

            if (option > i_NumberOfOptions || option <= 0)
            {
                throw new ValueOutOfRangeException(1, i_NumberOfOptions, "option number is illegal");
            }

            return option;
        }

        public static string GetLicenseNumber()
        {
            string licenseNumber = Console.ReadLine();
            if(!licenseNumber.All(char.IsLetterOrDigit))
            {
                throw new FormatException("Wrong license number. Please enter license number contains only letters or digits");
            }

            return licenseNumber;
        }

        public static string GetPhoneNumber()
        {
            string phoneNumber = Console.ReadLine();
            if (!phoneNumber.All(char.IsDigit))
            {
                throw new FormatException("Wrong phone number format. Please enter a number contains only digits");
            }

            return phoneNumber;
        }

        public static string GetStringForName()
        {
            string name = Console.ReadLine();
            if (!name.All(char.IsLetter))
            {
                throw new FormatException("Wrong name format. Please enter a name contains only letters");
            }

            return name;
        }

        public static FuelVehicle.eFuelType GetFuelType()
        {
            int fuelTypeInt = int.Parse(Console.ReadLine());
            FuelVehicle.eFuelType fuelType = (FuelVehicle.eFuelType)fuelTypeInt;
            if (fuelType != FuelVehicle.eFuelType.Octan95 && fuelType != FuelVehicle.eFuelType.Octan96 
                && fuelType != FuelVehicle.eFuelType.Octan98 && fuelType != FuelVehicle.eFuelType.Soler)
            {
                throw new Exception("Wrong fuel type! please choose between the legal fuel types.");
            }

            return fuelType;
        }
    }
}
