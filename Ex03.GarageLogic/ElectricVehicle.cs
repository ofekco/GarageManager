using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        protected float m_RemainingBatteryTime; //hours
        protected readonly float r_MaxBatteryTime; //hours

        protected ElectricVehicle(float i_MaxBatteryTime)
        {        
            r_MaxBatteryTime = i_MaxBatteryTime;
        }

        public override float CurrentEnergy
        {
            get { return m_RemainingBatteryTime; }
        }

        public float MaxBatteryTime
        {
            get { return r_MaxBatteryTime; }
        }

        public override void SetCurrentEnergy(float i_CurrentEnergy)
        {
            if(i_CurrentEnergy > r_MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, r_MaxBatteryTime, "Battery time is bigger than the maximun battery time");
            }
            else
            {
                m_RemainingBatteryTime = i_CurrentEnergy;
                SetRemainingEnergyPercentage(m_RemainingBatteryTime, r_MaxBatteryTime);
            }
        }

        public void ChargeBattery(float i_HoursToCharge)
        {
            if (i_HoursToCharge + m_RemainingBatteryTime > r_MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(0, r_MaxBatteryTime - m_RemainingBatteryTime, "you are trying to charge over the maximum battery time");
            }
            else
            {
                m_RemainingBatteryTime += i_HoursToCharge;
                SetRemainingEnergyPercentage(m_RemainingBatteryTime, r_MaxBatteryTime);
            }
        }
    }
}
