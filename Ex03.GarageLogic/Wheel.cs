using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public void SetCurrentAirPressure(float i_CurrentAirPressure)
        {
            if (i_CurrentAirPressure > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure, "Air pressure amount is bigger than the maximum");
            }
            else
            {
                m_CurrentAirPressure = i_CurrentAirPressure;
            }
        }

        public void InflateWheel(float i_AmountOfAirToAdd)
        {
            if (i_AmountOfAirToAdd + m_CurrentAirPressure > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure, "You are trying to inflate over the maximum");
            }
            else
            {
                m_CurrentAirPressure += i_AmountOfAirToAdd;
            }
        }
    }
}
