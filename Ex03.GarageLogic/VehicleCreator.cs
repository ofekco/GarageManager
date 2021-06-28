using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public static class VehicleCreator
    {
        public static string[] s_VehicleTypesMenu = {"Electric car", "Fuel car", "Electric motorcycle", "Fuel motorcycle", "Truck"};

        public static Vehicle CreateVehicle(int i_VehicleType)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case 0:
                    {
                        vehicle = new ElectricCar();
                        break;
                    }
                case 1:
                    {
                        vehicle = new FuelCar();
                        break;
                    }
                case 2:
                    {
                        vehicle = new ElectricMotorcycle();
                        break;
                    }
                case 3:
                    {
                        vehicle = new FuelMotorcycle();
                        break; 
                    }
                case 4:
                    {
                        vehicle = new Truck();
                        break;
                    }
            }

            return vehicle;
        }
    }
}
