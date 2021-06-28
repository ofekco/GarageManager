using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class ElectricCar : ElectricVehicle
    {
        private const float k_MaximunBatteryTime = 3.2f;
        private const float k_MaximunAirPressure = 32;
        private const int k_NumberOfWeels = 4;

        public ElectricCar() : base(k_MaximunBatteryTime)
        {
            m_Wheels = new Wheel[k_NumberOfWeels];
            CreateWheels(k_MaximunAirPressure);
            m_Features = new CarAdditionalFeatures();
        }
    }
}
