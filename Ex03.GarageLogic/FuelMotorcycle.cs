using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class FuelMotorcycle : FuelVehicle
    {
        private const float k_MaximunFuelAmount = 6;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_MaximunAirPressure = 30;
        private const int k_NumberOfWeels = 2;

        public FuelMotorcycle() : base(k_MaximunFuelAmount, k_FuelType)
        {
            m_Wheels = new Wheel[k_NumberOfWeels];
            CreateWheels(k_MaximunAirPressure);
            m_Features = new MotorcycleAdditionalFeatures();
        }
    }
}