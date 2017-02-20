using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace RedNODEHost
{
    public partial class GeoPlot : UserControl
    {
        #region Struct and enums

        public struct GeoCoordinate2D
        {
            public double Latitude;
            public double Longitude;
        }

        #endregion

        #region Properties

        int maxPoints = 10;
        FixedSizeLIFO<GeoCoordinate2D> gnssPoints;
        FixedSizeLIFO<GeoCoordinate2D> points;

        List<GeoCoordinate2D> markedPoints;

        FixedSizeLIFO<PointF> means;

        double lastMeanLon = double.NaN;
        double lastMeanLat = double.NaN;

        Pen pointPen = new Pen(Brushes.Blue, 2.0f);
        Pen gridPen = new Pen(Brushes.Black, 1.0f);
        Pen gPen = new Pen(Brushes.Green, 2.0f);
        Pen mPen = new Pen(Brushes.Red, 2.0f);

        //Accuracy2DEvaluator accEval;

        #endregion

        #region Constructor

        public GeoPlot()
        {
            InitializeComponent();

            // = new Accuracy2DEvaluator(short.MaxValue);                
        }

        #endregion

        #region Methods

        public string Angle2Str(double ang, bool isLat)
        {
            double angle = Math.Abs(ang);

            string cardinal;

            if (isLat)
            {
                if (ang < 0) cardinal = "S";
                else cardinal = "N";
            }
            else
            {
                if (ang < 0) cardinal = "W";
                else cardinal = "E";
            }

            int degree = (int)Math.Floor(angle);
            int minutes = (int)Math.Floor((angle - degree) * 60.0);
            double seconds = (angle - degree) * 3600 - minutes * 60.0;

            return string.Format(CultureInfo.InvariantCulture, "{0}{1}°{2}\'{3:F04}\"", cardinal, Math.Abs(degree), minutes, seconds);
        }        

        private PointF DegToMeters(double lon1, double lat1, double lon2, double lat2)
        {
            double rLat = ((lat1 + lat2) / 2.0f) * Math.PI / 180.0f;
            double meterPerDegLat = 111132.92f - 559.82f * Math.Cos(2.0f * rLat) + 1.175f * Math.Cos(4.0f * rLat);
            double meterPerDegLon = 111412.84f * Math.Cos(rLat) - 93.5f * Math.Cos(3.0f * rLat);

            PointF result = new PointF();

            result.X = (float)((lon1 - lon2) * meterPerDegLon);
            result.Y = (float)((lat1 - lat2) * meterPerDegLat);

            return result;
        }

        private float GetDistance2DM(double lon1, double lat1, double lon2, double lat2)
        {
            var result = DegToMeters(lon1, lat1, lon2, lat2);
            return (float)Math.Sqrt(result.X * result.X + result.Y * result.Y);
        }

        public void Init(int trajectoryLength, int trajectoriesCount)
        {
            maxPoints = trajectoryLength;
            points = new FixedSizeLIFO<GeoCoordinate2D>(maxPoints);
            means = new FixedSizeLIFO<PointF>(maxPoints);
            gnssPoints = new FixedSizeLIFO<GeoCoordinate2D>(maxPoints);
            markedPoints = new List<GeoCoordinate2D>();
        }

        public void AddPoint(GeoCoordinate2D newPoint)
        {
            points.Add(newPoint);
            //accEval.NewPoint(newPoint.Longitude, newPoint.Latitude, 0.0);
            Invalidate();
        }

        public void AddGNSSPoint(GeoCoordinate2D newPoint)
        {
            gnssPoints.Add(newPoint);
            Invalidate();
        }

        public void AddMarkedPoint(GeoCoordinate2D newPoint)
        {
            markedPoints.Add(newPoint);
            Invalidate();
        }

        public void Clear()
        {
            gnssPoints.Clear();
            points.Clear();
            markedPoints.Clear();
            means.Clear();
            //accEval = new Accuracy2DEvaluator(short.MaxValue);
            Invalidate();
        }

        public void ClearGNSSPoints()
        {
            gnssPoints.Clear();
        }

        public void ClearMarkedPoints()
        {
            markedPoints.Clear();
        }

        #endregion
        
        #region Handlers
        
        private void GeoPlot_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if ((points != null) && (!e.ClipRectangle.IsEmpty))
            {
                var items = points.ToArray();
                var gItems = gnssPoints.ToArray();
                var mItems = markedPoints.ToArray();

                if (items.Length > 1)
                {                 
                    double meanLong = 0.0;
                    double meanLatt = 0.0;                    

                    for (int i = 0; i < items.Length; i++)
                    {
                        meanLong += items[i].Longitude;
                        meanLatt += items[i].Latitude;
                    }

                    meanLong /= items.Length;
                    meanLatt /= items.Length;                    
                    
                    float farestXDelta = 0.0f;
                    float farestYDelta = 0.0f;
                    PointF[] deltas = new PointF[items.Length];
                    PointF[] gDeltas = new PointF[gItems.Length];
                    PointF[] mDeltas = new PointF[mItems.Length];
                    
                    double lastLat = items[0].Latitude;
                    double lastLon = items[0].Longitude;

                    for (int i = 0; i < items.Length; i++)
                    {
                        //deltas[i] = DegToMeters(meanLong, meanLatt, points[i].Longitude, points[i].Latitude);
                        deltas[i] = DegToMeters(lastLon, lastLat, items[i].Longitude, items[i].Latitude);

                        if (Math.Abs(deltas[i].X) > farestXDelta) farestXDelta = Math.Abs(deltas[i].X);
                        if (Math.Abs(deltas[i].Y) > farestYDelta) farestYDelta = Math.Abs(deltas[i].Y);
                    }

                    for (int i = 0; i < gItems.Length; i++)
                    {
                        gDeltas[i] = DegToMeters(lastLon, lastLat, gItems[i].Longitude, gItems[i].Latitude);

                        if (Math.Abs(gDeltas[i].X) > farestXDelta) farestXDelta = Math.Abs(gDeltas[i].X);
                        if (Math.Abs(gDeltas[i].Y) > farestYDelta) farestYDelta = Math.Abs(gDeltas[i].Y);
                    }

                    for (int i = 0; i < mItems.Length; i++)
                    {
                        mDeltas[i] = DegToMeters(lastLon, lastLat, mItems[i].Longitude, mItems[i].Latitude);

                        if (Math.Abs(mDeltas[i].X) > farestXDelta) farestXDelta = Math.Abs(mDeltas[i].X);
                        if (Math.Abs(mDeltas[i].Y) > farestYDelta) farestYDelta = Math.Abs(mDeltas[i].Y);
                    }


                    float farestDistM = (float)(Math.Max(farestXDelta, farestYDelta) * 1.1);

                    if (farestDistM < float.Epsilon)
                        farestDistM = 0.000001f;

                    float xScale = this.Width / (farestDistM * 2.0f);
                    float yScale = this.Height / (farestDistM * 2.0f);
                    float scale = (float)Math.Min(xScale, yScale);

                    e.Graphics.TranslateTransform(this.Width / 2.0f, this.Height / 2.0f);

                    float fDist = Math.Abs((float)farestDistM * 0.75f);                   
                    float fDistScaled = (float)(fDist * scale);

                    e.Graphics.DrawLine(gridPen, -fDistScaled, -4, -fDistScaled, 4);
                    e.Graphics.DrawLine(gridPen, fDistScaled, -4, fDistScaled, 4);
                    e.Graphics.DrawLine(gridPen, -4, -fDistScaled, 4, -fDistScaled);
                    e.Graphics.DrawLine(gridPen, -4, fDistScaled, 4, fDistScaled);

                    var fDistMLbl = fDist.ToString("0.0 m");
                    var fDistMLblSize = e.Graphics.MeasureString(fDistMLbl, this.Font);

                    e.Graphics.DrawString(fDistMLbl, this.Font, Brushes.Black,
                        -fDistScaled - fDistMLblSize.Width / 2, -fDistMLblSize.Height - 2);

                    e.Graphics.DrawString(fDistMLbl, this.Font, Brushes.Black,
                        fDistScaled - fDistMLblSize.Width / 2, -fDistMLblSize.Height - 2);

                    e.Graphics.DrawString(fDistMLbl, this.Font, Brushes.Black,
                        -fDistMLblSize.Width, -fDistScaled - fDistMLblSize.Height / 2);

                    e.Graphics.DrawString(fDistMLbl, this.Font, Brushes.Black,
                        -fDistMLblSize.Width, fDistScaled - fDistMLblSize.Height / 2);

                    float left = -this.Width / 2.0f;
                    float right = this.Width / 2.0f;
                    float top = -this.Height / 2.0f;
                    float bottom = this.Height / 2.0f;

                    e.Graphics.DrawLine(gridPen, left, 0.0f, right, 0.0f);
                    e.Graphics.DrawLine(gridPen, 0.0f, top, 0.0f, bottom);

                    if (!double.IsNaN(lastMeanLon))
                    {
                        var shift = DegToMeters(meanLong, meanLatt, lastMeanLon, lastMeanLat);
                        shift.X *= -scale;
                        shift.Y *= scale;
                        
                        means.Add(shift);
                    }

                    lastMeanLat = meanLatt;
                    lastMeanLon = meanLong;

                    //if (means.Count > 1)
                    //    e.Graphics.DrawLines(pointPen, means.ToArray());

                    for (int i = 0; i < deltas.Length; i++)
                    {
                        float xToDraw = -deltas[i].X * scale;
                        float yToDraw = deltas[i].Y * scale;

                        if (i == 0)
                        {
                            e.Graphics.DrawRectangle(pointPen, xToDraw - 8, yToDraw - 8, 16, 16);
                            e.Graphics.FillRectangle(pointPen.Brush, xToDraw - 8, yToDraw - 8, 16, 16);
                        }
                        else
                        {
                            e.Graphics.DrawRectangle(pointPen, xToDraw - 2, yToDraw - 2, 4, 4);
                            e.Graphics.FillRectangle(pointPen.Brush, xToDraw - 2, yToDraw - 2, 4, 4);
                        }
                    }

                    for (int i = 0; i < gDeltas.Length; i++)
                    {
                        float xToDraw = -gDeltas[i].X * scale;
                        float yToDraw = gDeltas[i].Y * scale;

                        if (i == 0)
                        {
                            e.Graphics.DrawRectangle(gPen, xToDraw - 4, yToDraw - 4, 8, 8);
                            e.Graphics.FillRectangle(gPen.Brush, xToDraw - 4, yToDraw - 4, 8, 8);
                        }
                        else
                        {
                            e.Graphics.DrawRectangle(gPen, xToDraw - 2, yToDraw - 2, 4, 4);
                            e.Graphics.FillRectangle(gPen.Brush, xToDraw - 2, yToDraw - 2, 4, 4);
                        }
                    }


                    for (int i = 0; i < mDeltas.Length; i++)
                    {
                        string mpStr = string.Format("MP #{0}", i);
                        var mpS = e.Graphics.MeasureString(mpStr, this.Font);

                        float xToDraw = -mDeltas[i].X * scale;
                        float yToDraw = mDeltas[i].Y * scale;

                        e.Graphics.FillRectangle(Brushes.Wheat, xToDraw - mpS.Width / 2.0f, yToDraw - 8 - mpS.Height, mpS.Width, mpS.Height);
                        e.Graphics.DrawString(mpStr, this.Font, Brushes.Red, xToDraw - mpS.Width / 2.0f, yToDraw - 8 - mpS.Height);

                       e.Graphics.DrawRectangle(mPen, xToDraw - 8, yToDraw - 8, 16, 16);
                       e.Graphics.FillRectangle(mPen.Brush, xToDraw - 8, yToDraw - 8, 16, 16);
                       
                    }

                    var latLabel = Angle2Str(meanLatt, true);
                    var latLabelSize = e.Graphics.MeasureString(latLabel, this.Font);
                    var lonLabel = Angle2Str(meanLong, false);
                    var lonLabelSize = e.Graphics.MeasureString(lonLabel, this.Font);

                    e.Graphics.DrawString(latLabel, this.Font, Brushes.Black, right - lonLabelSize.Width, 2.0f);
                    e.Graphics.DrawString(lonLabel, this.Font, Brushes.Black, 5.0f, top + 2.0f);
                }

            }                    
        }

        private void GeoPlot_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion
    }
}
