using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;
using System.Threading;

namespace ConsoleUI
{
    public static class ConsoleUI
    {
        public enum eGarageMenu { AddVehicle = 1, ShowLicenseNumbers, ChangeVehicleStatus, InflateWheels, Reful, Charge, ShowVehicleDetails, Exit };
        public static void MainMenu()
        {
            eGarageMenu userCohise = 0;
            while (userCohise != eGarageMenu.Exit)
            {
                Thread.Sleep(1500);
                Console.Clear();
                ShowMenu();
                try
                {
                    int option = InputHandler.GetOptionFromMenu(Enum.GetNames(typeof(eGarageMenu)).Length);
                    userCohise = (eGarageMenu)option;
                    Console.Clear();
                    switch (userCohise)
                    {
                        case eGarageMenu.AddVehicle:
                            {
                                addVehicleTotheGarage();
                                break;
                            }
                        case eGarageMenu.ShowLicenseNumbers:
                            {
                                showLicenseNumbers();
                                break;
                            }
                        case eGarageMenu.ChangeVehicleStatus:
                            {
                                changeStatus();
                                break;
                            }
                        case eGarageMenu.InflateWheels:
                            {
                                inflate();
                                break;
                            }
                        case eGarageMenu.Reful:
                            {
                                reful();
                                break;
                            }
                        case eGarageMenu.Charge:
                            {
                                chargeBatery();
                                break;
                            }
                        case eGarageMenu.ShowVehicleDetails:
                            {
                                showVehicleDetails();
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(4000);

                }
            }
        }

        private static string getLicenseNumber()
        {
            Console.WriteLine("Please enter the license number: ");
            return InputHandler.GetLicenseNumber();
        }

        public static void ShowMenu()
        {
            Console.WriteLine("GARAGE MANAGER MENU");
            Console.WriteLine();
            Console.WriteLine(@"Please choose one of the operations (enter the number of the operation): 
1.Add vehicle to the garage
2.Show the vehicles in the garage (license numbers)
3.Change vehicle status
4.Blow vehicle wheels
5.Reful (for fuel vehicles)
6.Charge electric vehicle batery
7.Show vehicle details
8.Exit");
        }
     
        private static int getVehicleType()
        {
            Console.WriteLine("Please Choose vehicle type from the types below: (enter the number of the type)");
            Console.WriteLine();
            for (int i=1; i <= VehicleCreator.s_VehicleTypesMenu.Length; ++i)
            {
                Console.WriteLine(i + ". " + VehicleCreator.s_VehicleTypesMenu[i-1]);
            }

            return InputHandler.GetOptionFromMenu(VehicleCreator.s_VehicleTypesMenu.Length);
        }

        private static void getVehicleDetails(out string o_ModelName, out float o_CurrentEnergy, out string o_WheelsManufacturerName, out float o_WheelsCurrentAirPressure)
        {
            Console.Clear();
            Console.WriteLine("Please fill the information below- ");
            Console.WriteLine("Vehicle model name: ");
            o_ModelName = InputHandler.GetStringForName();
            Console.WriteLine("Vehicle current energy(fuel in liters or battery in hours): ");
            o_CurrentEnergy = float.Parse(Console.ReadLine());
            Console.WriteLine("wheels manufacturer name: ");
            o_WheelsManufacturerName = InputHandler.GetStringForName();
            Console.WriteLine("wheels current air pressure: ");
            o_WheelsCurrentAirPressure = float.Parse(Console.ReadLine());
        }

        private static void addVehicleTotheGarage()
        {
            Vehicle newVehicle = null;
            string modelName, licenseNumber, wheelsManufacturerName, phoneNumber, ownerName;
            float currentEnergy, wheelsCurrentAirPressure;
            List<object> featuresList;

            licenseNumber = getLicenseNumber();

            if(GarageManager.IsInTheGarage(licenseNumber))
            {
                Console.WriteLine("This vehicle already exists in the garage! status changed to: InRepair.");
                GarageManager.ChangeVehicleStatus(licenseNumber, GarageManager.VehicleInTheGarage.eVehicleStatus.inRepair);
            }
            else
            {
                int vehicleTypeNumber = getVehicleType();
                newVehicle = VehicleCreator.CreateVehicle(vehicleTypeNumber - 1);
                getVehicleDetails(out modelName, out currentEnergy , out wheelsManufacturerName, out wheelsCurrentAirPressure);
                getOwnerDetails(out ownerName, out phoneNumber);
                featuresList = getVehicleAdditionalFeatures(newVehicle);
                GarageManager.AddNewVehicle(ownerName, phoneNumber, newVehicle, licenseNumber, modelName, currentEnergy, wheelsManufacturerName, wheelsCurrentAirPressure, featuresList);
                Console.WriteLine("Vehicle added successfully!");
            }
        }

        private static List<object> getVehicleAdditionalFeatures(Vehicle i_Vehicle)
        {
            List<object> parametersList = new List<object>();
            object input;

            foreach (KeyValuePair<string, object> param in i_Vehicle.Features.VehicleParameters)
            {
                Console.WriteLine(param.Key + ": ");
                input = Console.ReadLine();
                Convert.ChangeType(input, param.Value as Type);
                parametersList.Add(input);
            }

            return parametersList;
        }

        private static void getOwnerDetails(out string o_OwnerName, out string o_PhoneNumber)
        {
            Console.WriteLine("Please enter the vehicle owner name: ");
            o_OwnerName = InputHandler.GetStringForName();
            Console.WriteLine("Please enter the owner phone number: ");
            o_PhoneNumber = InputHandler.GetPhoneNumber();
        }

        private static void inflate()
        {
            string licenseNumber = getLicenseNumber();

            GarageManager.InflateWeelsToMax(licenseNumber);
            Console.WriteLine("Vehicle wheels successfully inflated");
        }

        private static void reful()
        {
            float amountOfFuel, newFuelStatus;
            string licenseNumber, fuelTypesMenu, msg;

            licenseNumber = getLicenseNumber();
            fuelTypesMenu = string.Format(@"What is the vehicle fuel type?
Press 0 to {0}
    95 to {1}
    96 to {2}
    98 t0 {3}", Enum.GetNames(typeof(FuelVehicle.eFuelType)));
            Console.WriteLine(fuelTypesMenu);

            FuelVehicle.eFuelType fuelType = InputHandler.GetFuelType();
            Console.WriteLine("Please enter the amount of fuel to reful: ");
            amountOfFuel = float.Parse(Console.ReadLine());
            newFuelStatus = GarageManager.Reful(licenseNumber, fuelType, amountOfFuel);

            msg = string.Format(@"The vehicle successfully refueled and the current fuel status is: {0}",
            newFuelStatus);
            Console.WriteLine(msg);
        }

        private static void chargeBatery()
        {
            string licenseNumber, msg;
            float amountOfMinitsToCharge, newBatteryStatus;

            licenseNumber = getLicenseNumber();
            Console.WriteLine("Please enter the amount of minutes you want to charge: ");
            amountOfMinitsToCharge = float.Parse(Console.ReadLine());
            newBatteryStatus = GarageManager.Charge(licenseNumber, amountOfMinitsToCharge);

            msg = string.Format(@"The vehicle successfully charged and the current battery status is: {0}%",
            newBatteryStatus);
            Console.WriteLine(msg);
        }

        private static void showVehicleDetails()
        {
            string licenseNumber, vehicleEnergyDetails, vehicleGenericDetails;
            GarageManager.VehicleInTheGarage vehicle;

            licenseNumber = getLicenseNumber();
            vehicle = GarageManager.GetVehicle(licenseNumber);
            vehicleGenericDetails = string.Format(@"Owner name: {0}
License number: {1}
Vehicle model name: {2}
Vehicle Status: {3}
Wheels manufacturer name: {4}, Wheels current air pressure: {5}
", vehicle.OwnerName, vehicle.Vehicle.LicenseNumber, 
vehicle.Vehicle.ModelName, vehicle.VehicleStatus, vehicle.Vehicle.Wheels[0].ManufacturerName, vehicle.Vehicle.Wheels[0].CurrentAirPressure);

            if (vehicle.Vehicle is FuelVehicle)
            {
                vehicleEnergyDetails = string.Format(@"Feul Type: {0}, Fuel status: {1}%", (vehicle.Vehicle as FuelVehicle).FuelType, (vehicle.Vehicle.RemainingEnergyPercentage));
            }
            else
            {
                vehicleEnergyDetails = string.Format(@"Battery status: {0}%", vehicle.Vehicle.RemainingEnergyPercentage);
            }

            Console.WriteLine(vehicleGenericDetails + vehicleEnergyDetails);

            foreach (KeyValuePair<string, object> param in vehicle.Vehicle.Features.VehicleParameters)
            {
                Console.WriteLine(param.Key + ": " + param.Value);
            }

            Console.WriteLine();
            Console.WriteLine("Enter to return to the menu");
            Console.ReadLine();
        }

        private static void showFilterByStatusMenu()
        {
            string statusMenu;

            statusMenu = string.Format(@"Please enter your choice:
Press 1 to show all {0} vehicles
Press 2 to show all {1} vehicles
Press 3 to show all {2} vehicles
Press 4 to show all the vehicles", Enum.GetNames(typeof(GarageManager.VehicleInTheGarage.eVehicleStatus)));
            Console.WriteLine(statusMenu);
        }

        private static void showLicenseNumbers()
        {
            showFilterByStatusMenu();

            int choise = InputHandler.GetOptionFromMenu(4);
            string[] licenseNumbersList;

            if(choise == 4)
            {
                licenseNumbersList = GarageManager.GetVehicleLicenseNumberList();
            }
            else
            {
                licenseNumbersList = GarageManager.GetVehicleLicenseNumberList(choise);
            }

            Console.Clear();
            foreach(string licenseNumber in licenseNumbersList)
            {
                Console.WriteLine(licenseNumber);
            }

            Console.WriteLine("Enter to return to the menu");
            Console.ReadLine();
        }
        
        private static void changeStatus()
        {
            string licenseNumber, statusOptions;

            licenseNumber = getLicenseNumber();

            statusOptions = string.Format(@"Please enter your choice:
Press 1 to show all {0} vehicles
Press 2 to show all {1} vehicles
Press 3 to show all {2} vehicles", Enum.GetNames(typeof(GarageManager.VehicleInTheGarage.eVehicleStatus)));
            Console.WriteLine(statusOptions);

            int newStatus = InputHandler.GetOptionFromMenu(3);
            GarageManager.ChangeVehicleStatus(licenseNumber, (GarageManager.VehicleInTheGarage.eVehicleStatus)newStatus);

            Console.WriteLine("Status changed");
        }
    }
}
