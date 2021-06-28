using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class MotorcycleAdditionalFeatures : VehicleAdditionalFeatures
    {
        public enum eLicenseTypes { A = 1, AA, B1, BB };

        public MotorcycleAdditionalFeatures()
        {
            m_VehicleParameters = new Dictionary<string, object>();
            m_VehicleParameters.Add("License Type", typeof(string));
            m_VehicleParameters.Add("Engine Volume", typeof(int));
        }

        public override void SetAdditionalFeatures(List<object> i_Parameters)
        {
            switch (i_Parameters[0])
            {
                case "A":
                    {
                        m_VehicleParameters["License Type"] = eLicenseTypes.A;
                        break;
                    }
                case "AA":
                    {
                        m_VehicleParameters["License Type"] = eLicenseTypes.AA;
                        break;
                    }
                case "B1":
                    {
                        m_VehicleParameters["License Type"] = eLicenseTypes.B1;
                        break;
                    }
                case "BB":
                    {
                        m_VehicleParameters["License Type"] = eLicenseTypes.BB;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("The license type you entered is not exist!");
                    }
            }

            m_VehicleParameters["Engine Volume"] = int.Parse(i_Parameters[1].ToString());
        }
    }
}
