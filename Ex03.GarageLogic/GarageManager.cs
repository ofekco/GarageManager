using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public static class GarageManager
    {
        public class VehicleInTheGarage
        {
            public enum eVehicleStatus { inRepair = 0, Paidup, Fixed };
            private string m_OwnerName;
            private string m_OwnerPhoneNumber;
            private eVehicleStatus m_VehicleStatus;
            private Vehicle m_Vehicle;

            public VehicleInTheGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhoneNumber = i_OwnerPhoneNumber;
                m_Vehicle = i_Vehicle;
                m_VehicleStatus = eVehicleStatus.inRepair;
            }

            public string OwnerName
            {
                get
                {
                    return m_OwnerName;
                }
            }

            public string OwnerPhoneNumber
            {
                get
                {
                    return m_OwnerPhoneNumber;
                }
            }

            public Vehicle Vehicle
            {
                get
                {
                    return m_Vehicle;
                }
            }

            public eVehicleStatus VehicleStatus
            {
                get
                {
                    return m_VehicleStatus;
                }
                set
                {
                    m_VehicleStatus = value;
                }

            }
        }

        private static readonly Dictionary<string, VehicleInTheGarage>[] sr_AllVehiclesInTheGrage;

        static GarageManager()
        {
            int arraySize = Enum.GetNames(typeof(VehicleInTheGarage.eVehicleStatus)).Length;

            sr_AllVehiclesInTheGrage = new Dictionary<string, VehicleInTheGarage>[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                sr_AllVehiclesInTheGrage[i] = new Dictionary<string, VehicleInTheGarage>();
            }
        }

        public static void AddNewVehicle(string i_OwnerName, string i_OwnerPhone, Vehicle i_VehicleToAdd, string i_LicenseNumber, string i_ModelName, float i_CurrentEnergy, string i_WheelsManufacturerName, float i_WheelsCurrentAirPressure, List<object> i_VehicleAdditionalFeatures)
        {
            setParametersToVehicle(i_VehicleToAdd, i_LicenseNumber, i_ModelName, i_CurrentEnergy, i_WheelsManufacturerName, i_WheelsCurrentAirPressure, i_VehicleAdditionalFeatures);
            VehicleInTheGarage newVehicle = new VehicleInTheGarage(i_OwnerName, i_OwnerPhone, i_VehicleToAdd);
            sr_AllVehiclesInTheGrage[(int)VehicleInTheGarage.eVehicleStatus.inRepair].Add(newVehicle.Vehicle.LicenseNumber, newVehicle);
        }

        private static void setParametersToVehicle(Vehicle i_Vehicle, string i_LicenseNumber, string i_ModelName, float i_CurrentEnergy, string i_WheelsManufacturerName, float i_WheelsCurrentAirPressure, List<object> i_VehicleAdditionalFeatures)
        {
            i_Vehicle.LicenseNumber = i_LicenseNumber;
            i_Vehicle.ModelName = i_ModelName;
            i_Vehicle.SetCurrentEnergy(i_CurrentEnergy);
            i_Vehicle.SetWheelsDetails(i_WheelsManufacturerName, i_WheelsCurrentAirPressure);
            i_Vehicle.Features.SetAdditionalFeatures(i_VehicleAdditionalFeatures);
        }

        public static bool IsInTheGarage(string i_LicenseNumber)
        {
            bool isExist = false;

            foreach (Dictionary<string, VehicleInTheGarage> listByStastus in sr_AllVehiclesInTheGrage)
            {
                if (listByStastus.ContainsKey(i_LicenseNumber) == true)
                {
                    isExist = true;
                    break;
                }
            }

            if (isExist)
            {
                isExist = true;
            }

            return isExist;
        }

        public static string[] GetVehicleLicenseNumberList()
        {
            int index =0, resultListSize = sr_AllVehiclesInTheGrage[0].Count + sr_AllVehiclesInTheGrage[1].Count + sr_AllVehiclesInTheGrage[2].Count;
            string[] licenseNumberList = new string[resultListSize];
            for (int i=0; i<3; i++)
            {
                foreach(string licenseNumber in sr_AllVehiclesInTheGrage[i].Keys)
                {
                    licenseNumberList[index] = licenseNumber;
                    index++;
                }
            }

            return licenseNumberList;
        }

        public static string[] GetVehicleLicenseNumberList(int i_Status)
        {
            int index = 0;
            string[] licenseNumberList = new string[sr_AllVehiclesInTheGrage[i_Status - 1].Count];
            foreach(string licenseNumber in sr_AllVehiclesInTheGrage[i_Status - 1].Keys)
            {
                licenseNumberList[index] = licenseNumber;
                index++;
            }

            return licenseNumberList;
        }

        public static void ChangeVehicleStatus(string i_LicenseNumber, VehicleInTheGarage.eVehicleStatus i_NewStatus)
        {
            int oldStatus;
            VehicleInTheGarage vehicle;

            vehicle = GetVehicle(i_LicenseNumber);
            oldStatus = (int)vehicle.VehicleStatus;
            vehicle.VehicleStatus = i_NewStatus;

            //move vehicle to new status dictionary
            sr_AllVehiclesInTheGrage[(int)i_NewStatus - 1].Add(i_LicenseNumber, vehicle);
            sr_AllVehiclesInTheGrage[oldStatus].Remove(i_LicenseNumber);
        }

        public static VehicleInTheGarage GetVehicle(string i_LicenseNumber)
        {
            bool isExist = false;
            VehicleInTheGarage foundVehicle = null;

            foreach (Dictionary<string, VehicleInTheGarage> listByStastus in sr_AllVehiclesInTheGrage)
            {
                if (listByStastus.ContainsKey(i_LicenseNumber) == true)
                {
                    isExist = true;
                    foundVehicle = listByStastus[i_LicenseNumber];
                    break;
                }
            }

            if (!isExist)
            {
                throw new Exception("The vehicle is not exist in the garage!");
            }

            return foundVehicle;
        }

        public static void InflateWeelsToMax(string i_LicenseNumber)
        {
            VehicleInTheGarage foundVehicle = GetVehicle(i_LicenseNumber);
            foreach (Wheel wheel in foundVehicle.Vehicle.Wheels)
            {
                float amountOfAirToInflate = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                wheel.InflateWheel(amountOfAirToInflate);
            } 
        }

        public static float Reful(string i_LicenseNumber, FuelVehicle.eFuelType i_FuelType, float i_AmountOfFuel)
        {
            float fuelStatusAfterReful;
            VehicleInTheGarage foundVehicle = GetVehicle(i_LicenseNumber);
            
            FuelVehicle vehicleToReful = foundVehicle.Vehicle as FuelVehicle;
            if (vehicleToReful != null)
            {
                vehicleToReful.Refuel(i_AmountOfFuel, i_FuelType);
                fuelStatusAfterReful = vehicleToReful.RemainingEnergyPercentage;
            }
            else
            {
                throw new ArgumentException("You are trying to reful an electric vehicel!");
            }

            return fuelStatusAfterReful;
        }

        public static float Charge(string i_LicenseNumber, float i_AmountOfMinutesToCharge)
        {
            float batteryStatusAfterReful;
            VehicleInTheGarage foundVehicle = GetVehicle(i_LicenseNumber);
            ElectricVehicle vehicleToCharge = foundVehicle.Vehicle as ElectricVehicle;

            if(vehicleToCharge != null)
            {
                float hoursToCharge = i_AmountOfMinutesToCharge / 60;
                vehicleToCharge.ChargeBattery(hoursToCharge);
                batteryStatusAfterReful = vehicleToCharge.RemainingEnergyPercentage;
            }
            else
            {
                throw new ArgumentException("You are trying to charge a fuel vehicel!");
            }

            return batteryStatusAfterReful;
        }
    }
}
