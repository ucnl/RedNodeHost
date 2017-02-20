
namespace RedNODEHost
{
    public class GeoPoint
    {
        public double Lat;
        public double Lon;

        public GeoPoint()
            : this(0, 0)
        {
        }

        public GeoPoint(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }
    }

    public class GeoPoint3D : GeoPoint
    {
        public double Depth;

        public GeoPoint3D()
            : base()
        {
            Depth = 0;
        }

        public GeoPoint3D(double lat, double lon, double dpt)
            : base(lat, lon)
        {
            Depth = dpt;
        }

    }

    public class GeoPoint3DE : GeoPoint3D
    {
        public double RadialError;

        public GeoPoint3DE()
            : base(0, 0, 0)
        {
            RadialError = 0;
        }

        public GeoPoint3DE(double lat, double lon, double dpt, double rerr)
            : base(lat, lon, dpt)
        {
            RadialError = rerr;
        }

    }

    public class TrackRecord
    {
        public double Second;
        public GeoPoint3DE Location;
        public GeoPoint Buoy0Location;
        public GeoPoint Buoy1Location;
        public GeoPoint Buoy2Location;
        public GeoPoint Buoy3Location;
    }
}
