using System;

namespace RedWaveNodeHost
{
    [Serializable]
    public class Point3DWE
    {
        public double Latitude;
        public double Longitude;
        public double Depth;
        public double RadialError;

        public Point3DWE()
            : this(0, 0, 0, 0)
        {
        }

        public Point3DWE(double lat, double lon, double dpt, double rerr)
        {
            Latitude = lat;
            Longitude = lon;
            Depth = dpt;
            RadialError = rerr;
        }
    }
}
