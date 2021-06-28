using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class TruckAdditionalFeatures : VehicleAdditionalFeatures
    {
        public TruckAdditionalFeatures()
        {
            m_VehicleParameters = new Dictionary<string, object>();
            m_VehicleParameters.Add("Is Carry Hazardous Materials (true/false)", typeof(bool));
            m_VehicleParameters.Add("Maximum Carry Whight", typeof(float));
        }

        public override void SetAdditionalFeatures(List<object> i_Parameters)
        {
            m_VehicleParameters["Is Carry Hazardous Materials (true/false)"] = i_Parameters[0];
            m_VehicleParameters["Maximum Carry Whight"] = float.Parse(i_Parameters[1].ToString());
        }
    }
}
