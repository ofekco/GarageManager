using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class Truck : FuelVehicle
    {
        private const float k_MaximunFuelAmount = 120;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaximunAirPressure = 26;
        private const int k_NumberOfWeels = 16;

        public Truck() : base(k_MaximunFuelAmount, k_FuelType)
        {
            m_Wheels = new Wheel[k_NumberOfWeels];
            CreateWheels(k_MaximunAirPressure);
            m_Features = new TruckAdditionalFeatures();
        }
    }
}
