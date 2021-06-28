using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        private const float k_MaximunBatteryTime = 1.8f;
        private const float k_MaximunAirPressure = 30;
        private const int k_NumberOfWeels = 2;

        public ElectricMotorcycle() : base(k_MaximunBatteryTime)
        {
            m_Wheels = new Wheel[k_NumberOfWeels];
            CreateWheels(k_MaximunAirPressure);
            m_Features = new MotorcycleAdditionalFeatures();
        }
    }
}



