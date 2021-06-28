using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_RemainingEnergyPercentage;
        protected Wheel[] m_Wheels;
        protected VehicleAdditionalFeatures m_Features;

        public float RemainingEnergyPercentage
        {
            get
            {
                return m_RemainingEnergyPercentage;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
            set
            {
                m_LicenseNumber = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public abstract float CurrentEnergy
        {
            get;
        }

        public VehicleAdditionalFeatures Features
        {
            get { return m_Features; }
        }

        public Wheel[] Wheels
        {
            get { return m_Wheels; }
        }

        public void SetRemainingEnergyPercentage(float i_CurrentEnergyValue, float i_MaxEnergyValue)
        {
            m_RemainingEnergyPercentage = (i_CurrentEnergyValue / i_MaxEnergyValue) * 100;
        }

        public void CreateWheels(float i_MaximunAirPressure)
        {
            for (int i = 0; i < m_Wheels.Length; ++i)
            {
                m_Wheels[i] = new Wheel(i_MaximunAirPressure);
            }
        }

        public void SetWheelsDetails(string i_WheelsManufacturerName, float i_WheelsCurrentAirPressure)
        {
            for (int i = 0; i < m_Wheels.Length; ++i)
            {
                m_Wheels[i].SetCurrentAirPressure(i_WheelsCurrentAirPressure);
                m_Wheels[i].ManufacturerName = i_WheelsManufacturerName;
            }
        }

        public abstract void SetCurrentEnergy(float i_CurrentEnergy);
    }
}
