using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class VehicleAdditionalFeatures
    {
        protected Dictionary<string, object> m_VehicleParameters;

        public Dictionary<string, object> VehicleParameters
        {
            get
            {
                return m_VehicleParameters;
            }
        }

        public abstract void SetAdditionalFeatures(List<object> i_Parameters);
    }
}
