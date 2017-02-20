using System;
using System.Text;

namespace RedNODEHost
{
    [Serializable]
    public class SettingsContainer
    {
        #region Properties

        public string RedNODEPortName;             
        public double Salinity;
        public int NumberOfPointsToShow;
        public int RadialErrorThreshold;
        public string GNSSPortName;

        #endregion

        #region Constructor

        public SettingsContainer()
        {
            SetDefaults();
        }

        #endregion

        #region Methods

        public void SetDefaults()
        {
            RedNODEPortName = "COM1";
            Salinity = 0.0;
            NumberOfPointsToShow = 1000;
            RadialErrorThreshold = 25;
            GNSSPortName = "COM2";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Settings:\r\nREDNodePortName: {0}\r\n", RedNODEPortName);            
            sb.AppendFormat("Salinity: {0:F01} m/s\r\n", Salinity);
            sb.AppendFormat("NumberOfPointsToShow: {0}\r\n", NumberOfPointsToShow);
            sb.AppendFormat("RadialErrorThreshold: {0}\r\n", RadialErrorThreshold);
            sb.AppendFormat("GNSSPortName: {0}\r\n", GNSSPortName);

            return sb.ToString();
        }

        #endregion
    }
}
