using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class CarAdditionalFeatures : VehicleAdditionalFeatures
    {
        public enum eCarColor { Red = 1, White, Black, Silver };

        public enum eNumberOfDoors { TwoDoors = 2, ThreeDoor, FourDoors, FiveDoors };

        public CarAdditionalFeatures()
        {
            m_VehicleParameters = new Dictionary<string, object>();
            m_VehicleParameters.Add("Car color", typeof(string));
            m_VehicleParameters.Add("Number of doors", typeof(int));
        }
   
        public override void SetAdditionalFeatures(List<object> i_Parameters) 
        {
            switch (i_Parameters[0])
            {
                case "Red":
                case "red":
                case "RED":
                    {
                        m_VehicleParameters["Car color"] = eCarColor.Red;
                        break;
                    }
                case "White":
                case "WHITE":
                case "white":
                    {
                        m_VehicleParameters["License Types"] = eCarColor.White;
                        break;
                    }
                case "Black":
                case "BLACK":
                case "black":
                    {
                        m_VehicleParameters["Car color"] = eCarColor.Black;
                        break;
                    }
                case "Silver":
                case "SILVER":
                case "silver":
                    {
                        m_VehicleParameters["Car color"] = eCarColor.Silver;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("The color you entered is illegal");
                    }
            }

            int numberOfDoors = int.Parse(i_Parameters[1].ToString());
            if (numberOfDoors != 2 && numberOfDoors != 3 && numberOfDoors != 4 && numberOfDoors != 5)
            {
                throw new ValueOutOfRangeException(2, 5, "number of doors is illegal");
            }
            else
            {
                m_VehicleParameters["Number of doors"] = numberOfDoors;
            }
        }

    }
}
