using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class FuelCar : FuelVehicle
    { 
        private const float k_CarMaximunFuelAmount = 45;
        private const eFuelType k_CarFuelType = eFuelType.Octan95;
        private const float k_MaximunAirPressure = 32;
        private const int k_NumberOfWeels = 4;

        public FuelCar() : base(k_CarMaximunFuelAmount, k_CarFuelType)
        {
            m_Wheels = new Wheel[k_NumberOfWeels];
            CreateWheels(k_MaximunAirPressure);
            m_Features = new CarAdditionalFeatures();
        }
    }
}
