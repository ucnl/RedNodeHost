using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UCNLKML;
using UCNLNMEA;

namespace RedNODEHost
{
    public partial class MainForm : Form
    {
        #region Invokers

        private void InvokeSetText(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke((MethodInvoker)delegate { ctrl.Text = text; });
            else
                ctrl.Text = text;
        }

        private void InvokeSetForeColor(Control ctrl, Color color)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke((MethodInvoker)delegate { ctrl.ForeColor = color; });
            else
                ctrl.ForeColor = color;
        }

        private void InvokeAddRNPoint(RedNODEHost.GeoPlot.GeoCoordinate2D newPoint)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.AddPoint(newPoint); });
            else
                geoPlot.AddPoint(newPoint);
        }

        private void InvokeAddGNSSPoint(RedNODEHost.GeoPlot.GeoCoordinate2D newPoint)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.AddGNSSPoint(newPoint); });
            else
                geoPlot.AddGNSSPoint(newPoint);
        }

        private void InvokeAddMarkedPoint(RedNODEHost.GeoPlot.GeoCoordinate2D newPoint)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.AddMarkedPoint(newPoint); });
            else
                geoPlot.AddMarkedPoint(newPoint);
        }



        #endregion

        #region Properties

        //bool isAutoboat = true;

        RedNodePort port;
        SettingsProviderXML<SettingsContainer> settingsProvider;

        GNSSReceiverWrapper gnssPort;

        string ownPath;
        string settingsFileName;

        bool isRestart = false;

        bool isDefined = false;
        bool isSalinityUpdated = false;
        bool isPressureRatingUpdated = false;

        int cnt = 0;

        int locHyst = 0;
        bool onceLocated = false;

        List<TrackRecord> track = new List<TrackRecord>();
        List<GeoPoint> markedPoints = new List<GeoPoint>();
        List<GeoPoint> gnssTrack = new List<GeoPoint>();

        TSLogProvider logger;

        SortedList<double, double> tempVsDepth = new SortedList<double, double>();
        SortedList<double, KeyValuePair<double, double>> tempVsPressure = new SortedList<double, KeyValuePair<double, double>>();

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();           

            #region log

            var ownDir = Path.GetDirectoryName(Application.ExecutablePath);

            logger = new TSLogProvider(Path.Combine(ownDir, 
                string.Format("{0}_{1}.log", Path.GetFileNameWithoutExtension(Application.ExecutablePath), GetTimeFileName(DateTime.Now))));

            #endregion

            #region settingsProvider

            ownPath = Path.GetDirectoryName(Application.ExecutablePath);

            settingsFileName = Path.ChangeExtension(Application.ExecutablePath, "settings");
            settingsProvider = new SettingsProviderXML<SettingsContainer>();
            settingsProvider.isSwallowExceptions = false;

            try
            {
                settingsProvider.Load(settingsFileName);
            }
            catch (Exception ex)
            {
                settingsProvider.Data.SetDefaults();
                logger.Write(ex);
            }

            logger.Write(settingsProvider.Data.ToString());

            #endregion

            #region Port

            port = new RedNodePort(settingsProvider.Data.RedNODEPortName);
            port.DeviceInfoUpdated += new EventHandler(port_DeviceInfoUpdated);
            port.ACKReceived += new EventHandler<ACKEventArgs>(port_ACK);
            port.LocalDataReceived += new EventHandler<LocalDataEventArgs>(port_LocalData);
            port.LogEvent += new EventHandler<LogEventArgs>(port_Log);
            port.PortReady += new EventHandler(port_Ready);
            port.QueryTimeout += new EventHandler(port_Timeout);

            port.PressureUpdated += new EventHandler(port_PressureUpdated);
            port.DepthUpdated += new EventHandler(port_DepthUpdated);
            port.TemperatureUpdated += new EventHandler(port_TemperatureUpdated);
            port.BasesUpdated += new EventHandler(port_BasesUpdated);
            port.PositionUpdated += new EventHandler(port_PositionUpdated);

            port.GGAEvent += new EventHandler<GGAEventArgs>(port_GGAEvent);
            port.RMCEvent += new EventHandler<RMCEventArgs>(port_RMCEvent);

            #endregion

            #region GeoPlot

            geoPlot.Init(settingsProvider.Data.NumberOfPointsToShow, 2);

            #endregion

            #region track


            #endregion

            #region gnssPort

            gnssPort = new GNSSReceiverWrapper(
                new SerialPortSettings(settingsProvider.Data.GNSSPortName,
                    BaudRate.baudRate9600,
                    System.IO.Ports.Parity.None, DataBits.dataBits8, System.IO.Ports.StopBits.One, System.IO.Ports.Handshake.None));

            gnssPort.GLLEvent += new EventHandler<GLLEventArgs>(gnssPort_GLL);

            #endregion
        }

        #endregion        

        #region Methods

        private string Angle2Str(double ang, bool isLat)
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

        private string GetTimeFileName(DateTime timeFix)
        {
            return string.Format("{0:00}-{1:00}-{2:00}",
                timeFix.Hour,
                timeFix.Minute,
                timeFix.Second);
        }

        private double GetDistance2D(double ownLat, double ownLon, double bLat, double bLon)
        {
            double dLat = (bLat - ownLat) * (Math.PI / 180.0);
            double dLon = (bLon - ownLon) * (Math.PI / 180.0);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                      Math.Cos(ownLat * (Math.PI / 180.0)) * Math.Cos(bLat * (Math.PI / 180.0)) *
                      Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return 6371000.0 * c;
        }

        /// <summary>
        /// Calculates speed of sound in water according to Wilson formula
        /// </summary>
        /// <param name="t">Water temperature in °C</param>
        /// <param name="p">Hydrostatic pressure in MPa</param>
        /// <param name="s">Salinity</param>
        /// <returns></returns>
        public static double CalcSoundSpeed(double t, double p, double s)
        {
            if ((t < -4) || (t > 50))
                throw new ArgumentOutOfRangeException("t");

            if ((p < 0.1) || (p > 100))
                throw new ArgumentOutOfRangeException("p");

            if ((s < 0) || (s > 40))
                throw new ArgumentOutOfRangeException("s");

            //temperature from -4° to 30°;
            //salinity from 0 to 37 per mille;
            //hydrostatic pressure from 0.1 MPa to 100 MPa. 
            // where c(S,T,P) - speed of sound, m/s; T - temperature, °C; S - salinity, per mille; P - hydrostatic pressure, MPa. 

            double c0 = 1449.14;
            double Dct = 4.5721 * t - 4.4532E-2 * t * t - 2.6045E-4 * t * t * t + 7.9851E-6 * t * t * t * t;
            double Dcs = 1.39799 * (s - 35) - 1.69202E-3 * (s - 35) * (s - 35);
            double Dcp = 1.63432 * p - 1.06768E-3 * p * p + 3.73403E-6 * p * p * p - 3.6332E-8 * p * p * p * p;
            double Dcstp = (s - 35) * (-1.1244E-2 * t + 7.7711E-7 * t * t + 7.85344E-4 * p -
                            1.3458E-5 * p * p + 3.2203E-7 * p * t + 1.6101E-8 * t * t * p) +
                            p * (-1.8974E-3 * t + 7.6287E-5 * t * t + 4.6176E-7 * t * t * t) +
                            p * p * (-2.6301E-5 * t + 1.9302E-7 * t * t) + p * p * p * (-2.0831E-7 * t);

            double result = c0 + Dct + Dcs + Dcp + Dcstp;

            return result;
        }

        private void ParseTrackFromFile(string fileName)
        {
            bool isOk = false;
            List<string> lines = new List<string>();

            try
            {
                lines.AddRange(File.ReadAllLines(fileName));
                isOk = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (isOk)
            {
                track.Clear();

                foreach (var item in lines)
                {
                    if (item.Contains("$PTNTC"))
                    {
                        int wspIdx = item.IndexOf(' ');
                        var timeStr = item.Substring(0, wspIdx);

                        var splits = timeStr.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        int hrs = Convert.ToInt32(splits[0]);
                        int mins = Convert.ToInt32(splits[1]);
                        double secs = Convert.ToDouble(splits[2], CultureInfo.InvariantCulture);
                        secs += mins * 60 + hrs * 3600;

                        int sIdx = item.IndexOf("$PTNTC");
                        var subStr = item.Substring(sIdx);

                        if (!subStr.EndsWith("\r\n"))
                            subStr = subStr + "\r\n";

                        try
                        {
                            var pSentence = NMEAParser.Parse(subStr);
                            if (pSentence is NMEAProprietarySentence)
                            {
                                var ps = (pSentence as NMEAProprietarySentence);
                                if ((ps.Manufacturer == ManufacturerCodes.TNT) && (ps.SentenceIDString == "C"))
                                {
                                    TrackRecord newRecord = new TrackRecord();

                                    newRecord.Second = secs;

                                    newRecord.Location = new GeoPoint3DE((double)ps.parameters[0],
                                        (double)ps.parameters[1],
                                        (double)ps.parameters[2],
                                        (double)ps.parameters[3]);

                                    newRecord.Buoy0Location = new GeoPoint((double)ps.parameters[4], (double)ps.parameters[5]);
                                    newRecord.Buoy1Location = new GeoPoint((double)ps.parameters[6], (double)ps.parameters[7]);
                                    newRecord.Buoy2Location = new GeoPoint((double)ps.parameters[8], (double)ps.parameters[9]);
                                    newRecord.Buoy3Location = new GeoPoint((double)ps.parameters[10], (double)ps.parameters[11]);

                                    if (newRecord.Location.RadialError <= settingsProvider.Data.RadialErrorThreshold) track.Add(newRecord);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                if (track.Count > 0)
                {
                    saveTrackToolStripMenuItem_Click(this, new EventArgs());
                }
            }
        }

        #endregion

        #region Handlers

        #region UI

        private void gnssConnectBtn_Click(object sender, EventArgs e)
        {
            if (gnssPort.IsOpen)
            {
                try
                {
                    gnssPort.Close();
                    gnssConnectBtn.Checked = false;
                }
                catch (Exception ex)
                {
                    logger.Write(ex);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    gnssPort.Open();
                    gnssConnectBtn.Checked = true;
                }
                catch (Exception ex)
                {
                    logger.Write(ex);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(serialNumberLbl.Text);
        }

        private void connectionBtn_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                try
                {
                    port.Stop();
                }
                catch (Exception ex)
                {
                    logger.Write(ex);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                connectionBtn.Checked = false;
                settingsBtn.Enabled = true;
                zeroDptAdjustBtn.Enabled = false;
            }
            else
            {
                try
                {
                    port.Start();

                    connectionBtn.Checked = true;
                    settingsBtn.Enabled = false;
                    zeroDptAdjustBtn.Enabled = true;

                }
                catch (Exception ex)
                {                    
                    logger.Write(ex);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            using (SettingsEditor sEditor = new SettingsEditor())
            {
                sEditor.Text = string.Format("{0} [Settings]", Assembly.GetExecutingAssembly().GetName().Name);
                sEditor.Value = settingsProvider.Data;

                if (sEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    settingsProvider.Data = sEditor.Value;

                    bool isSaved = false;
                    try
                    {
                        settingsProvider.Save(settingsFileName);
                        isSaved = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    if (isSaved)
                    {
                        if (MessageBox.Show("Settings saved. Restart application to apply new settings?", "Question",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            isRestart = true;
                            Application.Restart();
                        }
                    }
                }
            }
        }

        private void plotClearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all track data?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                geoPlot.Clear();
        }

        private void zeroDptAdjustBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Set current pressure as surface pressure?", "Question", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    port.Send(port.QueryZeroDepthAdjust());
                }
                catch (Exception ex)
                {
                    logger.Write(ex);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void markPointBtn_Click(object sender, EventArgs e)
        {
            if (onceLocated)
            {
                GeoPlot.GeoCoordinate2D newPoint = new GeoPlot.GeoCoordinate2D();
                newPoint.Latitude = port.Latitude;
                newPoint.Longitude = port.Longitude;
                geoPlot.AddMarkedPoint(newPoint);
                markedPoints.Add(new GeoPoint(port.Latitude, port.Longitude));
            }
        }

        private void saveTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sDialog = new SaveFileDialog())
            {
                sDialog.Title = "Choose filename to save data...";
                sDialog.FileName = GetTimeFileName(DateTime.Now);
                sDialog.Filter = "KML (*.kml)|*.kml";

                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //if (sDialog.FilterIndex == 1)
                    //{
                        #region Save to KML

                        KMLData data = new KMLData(sDialog.FileName, "Generated by RedNODE Host application");

                        List<KMLLocation> tPOwn = new List<KMLLocation>();
                        List<KMLLocation> tBuoy0 = new List<KMLLocation>();
                        List<KMLLocation> tBuoy1 = new List<KMLLocation>();
                        List<KMLLocation> tBuoy2 = new List<KMLLocation>();
                        List<KMLLocation> tBuoy3 = new List<KMLLocation>();
                        List<KMLLocation> gnss = new List<KMLLocation>();
                        

                        for (int i = 0; i < track.Count; i++)
                        {
                            tPOwn.Add(new KMLLocation(track[i].Location.Lon, track[i].Location.Lat, track[i].Location.Depth));
                            tBuoy0.Add(new KMLLocation(track[i].Buoy0Location.Lon, track[i].Buoy0Location.Lat, 0));
                            tBuoy1.Add(new KMLLocation(track[i].Buoy1Location.Lon, track[i].Buoy1Location.Lat, 0));
                            tBuoy2.Add(new KMLLocation(track[i].Buoy2Location.Lon, track[i].Buoy2Location.Lat, 0));
                            tBuoy3.Add(new KMLLocation(track[i].Buoy3Location.Lon, track[i].Buoy3Location.Lat, 0));
                        }

                        for (int i = 0; i < gnssTrack.Count; i++)
                            gnss.Add(new KMLLocation(gnssTrack[i].Lon, gnssTrack[i].Lat, 0));

                        for (int i = 0; i < markedPoints.Count; i++)
                            data.Add(new KMLPlacemark(string.Format("Marked point", i), "", false, false, new KMLLocation(markedPoints[i].Lon, markedPoints[i].Lat, 0)));


                        data.Add(new KMLPlacemark("Own", "RedWave RedNODE device track", tPOwn.ToArray()));
                        data.Add(new KMLPlacemark("RedBASE #1", "Buoy #1 track", tBuoy0.ToArray()));
                        data.Add(new KMLPlacemark("RedBASE #2", "Buoy #2 track", tBuoy1.ToArray()));
                        data.Add(new KMLPlacemark("RedBASE #3", "Buoy #3 track", tBuoy2.ToArray()));
                        data.Add(new KMLPlacemark("RedBASE #4", "Buoy #4 track", tBuoy3.ToArray()));

                        if (gnss.Count > 0)
                            data.Add(new KMLPlacemark("GNSS", "GNSS track", gnss.ToArray()));

                        try
                        {
                            TinyKML.Write(data, sDialog.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("Unable to save track to {0}: {1}", sDialog.FileName, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            logger.Write(ex);
                        }

                        #endregion
                    /*}
                    else if (sDialog.FilterIndex == 2)
                    {
                        #region Save to CSV

                        try
                        {
                            //
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("Unable to save track to {0}: {1}", sDialog.FileName, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        #endregion
                    }
                    else if (sDialog.FilterIndex == 3)
                    {
                        #region Save to GPX

                        #endregion
                    }
                     */
                }
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox())
            {
                aboutBox.ApplyAssembly(Assembly.GetExecutingAssembly());
                aboutBox.Weblink = "http://www.unavlab.com/";
                aboutBox.ShowDialog();
            }
        }

        private void saveTemperatureVsDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sDialog = new SaveFileDialog())
            {
                sDialog.Title = "Save temperature vs. depth";
                sDialog.DefaultExt = "csv";
                sDialog.Filter = "CSV (*.csv)|*.csv";
                sDialog.FileName = string.Format("{0}_tmp_vs_dpt", GetTimeFileName(DateTime.Now));

                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("Depth [m],Temperature [C]");
                        foreach (var item in tempVsDepth)
                        {
                            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:F03};{1:F01}\r\n", item.Key, item.Value);
                        }

                        File.WriteAllText(sDialog.FileName, sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Write(ex);
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void saveSoundSpeedVsDepthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sDialog = new SaveFileDialog())
            {
                sDialog.Title = "Save temperature vs. depth";
                sDialog.DefaultExt = "csv";
                sDialog.Filter = "CSV (*.csv)|*.csv";
                sDialog.FileName = string.Format("{0}_tmp_ss_press_vs_dpt", GetTimeFileName(DateTime.Now));

                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    double ss, p, t;

                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("Depth [m], Pressure [mBar], Temperature [C], Sound speed [m/s]");
                        foreach (var item in tempVsPressure)
                        {
                            p = item.Key / 10000.0;
                            if (p < 0.1) p = 0.1;
                            t = item.Value.Value;
                            ss = CalcSoundSpeed(t, p, settingsProvider.Data.Salinity);
                            sb.AppendFormat(CultureInfo.InvariantCulture, "{0:F03};{1:F02};{2:F01};{3:F02}\r\n",
                                item.Value.Key,
                                item.Key,                                 
                                item.Value.Value,
                                ss);
                        }

                        File.WriteAllText(sDialog.FileName, sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Write(ex);
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void clearMPBtn_Click(object sender, EventArgs e)
        {
            if (markedPoints.Count > 0)
            {
                if (MessageBox.Show("Clear all marked points?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    geoPlot.ClearMarkedPoints();
            }
        }

        private void clearGNSSBtn_Click(object sender, EventArgs e)
        {
            if (gnssTrack.Count > 0)
            {
                if (MessageBox.Show("Clear GNSS track?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    geoPlot.ClearGNSSPoints();
            }
        }

        private void parseFromAFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oDialog = new OpenFileDialog())
            {
                oDialog.Title = "Choose file to parse a track from...";
                oDialog.Filter = "logs (*.log)|*.log|any type (*.*)|*.*";

                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ParseTrackFromFile(oDialog.FileName);
                }
            }
        }

        private void parseFromLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParseTrackFromFile(logger.FileName);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (!isRestart) && (MessageBox.Show("Close application?", "Question", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (port.IsOpen) port.Stop();
            if (gnssPort.IsOpen) gnssPort.Close();

            logger.Write(string.Format("{0} {1}", LogLineType.INFO, "Closing..."));
            logger.FinishLog();           
        }

        #endregion

        #region port

        private void port_DeviceInfoUpdated(object sender, EventArgs e)
        {
            isDefined = true;
            InvokeSetText(systemInfoLbl, string.Format("{0} v{1}", port.SystemMoniker, port.SystemVersion));
            InvokeSetText(acCoreInfoLbl, string.Format("{0} v{1}", port.CoreMoniker, port.CoreVersion));
            InvokeSetText(serialNumberLbl, port.SerialNumber);
        }

        private void port_ACK(object sender, ACKEventArgs e)
        {
            //
        }

        private void port_LocalData(object sender, LocalDataEventArgs e)
        {
            switch (e.LocalDataID)
            {
                case LocalDataIDs.LOC_DATA_SALINITY:
                {
                    isSalinityUpdated = true;
                    InvokeSetText(salinityLbl, e.Value.ToString("F01", CultureInfo.InvariantCulture));
                    break;
                }
                case LocalDataIDs.LOC_DATA_PRESSURE_RATING:
                {
                    isPressureRatingUpdated = true;
                    InvokeSetText(pressureRatingLbl, e.Value.ToString("F00", CultureInfo.InvariantCulture));
                    break;
                }
                case LocalDataIDs.LOC_DATA_GRAVITY_ACC:
                {
                    InvokeSetText(gravityAccLbl, e.Value.ToString("F04", CultureInfo.InvariantCulture));
                    break;
                }
                case LocalDataIDs.LOC_DATA_SOUNDSPEED:
                {
                    InvokeSetText(soundSpeedLbl, e.Value.ToString("F02", CultureInfo.InvariantCulture));
                    break;
                }
                case LocalDataIDs.LOC_DATA_WATER_DENSITY:
                {
                    InvokeSetText(waterDensityLbl, e.Value.ToString("F02", CultureInfo.InvariantCulture));
                    break;
                }

            }
        }

        private void port_Log(object sender, LogEventArgs e)
        {
            logger.Write(string.Format("{0}: {1}", e.EventType, e.LogString));
        }

        private void port_Ready(object sender, EventArgs e)
        {
            //if (!isAutoboat)
            {
                if (!isDefined)
                    port.Send(port.QueryDeviceInfo());
                else if (!isSalinityUpdated)
                    port.Send(port.QueryLocDataSet(LocalDataIDs.LOC_DATA_SALINITY, settingsProvider.Data.Salinity));
                else if (!isPressureRatingUpdated)
                    port.Send(port.QueryLocDataGet(LocalDataIDs.LOC_DATA_PRESSURE_RATING));
            }
        }

        private void port_Timeout(object sender, EventArgs e)
        {
            logger.Write("Port timeout");
        }

        private void port_PressureUpdated(object sender, EventArgs e)
        {
            InvokeSetText(pressureLbl, port.Pressure.ToString("F02", CultureInfo.InvariantCulture));

            cnt++;

            if (cnt % 25 == 0)
            {
                port.Send(port.QueryLocDataGet(LocalDataIDs.LOC_DATA_SOUNDSPEED));
            }
            else if (cnt % 35 == 0)
            {
                port.Send(port.QueryLocDataGet(LocalDataIDs.LOC_DATA_WATER_DENSITY));
            }
            else if (cnt % 45 == 0)
            {
                port.Send(port.QueryLocDataGet(LocalDataIDs.LOC_DATA_GRAVITY_ACC));
            }

            if (++locHyst > 10)
            {
                InvokeSetText(latLbl, "- - -");
                InvokeSetText(lonLbl, "- - -");
                InvokeSetText(rerrLbl, "- - -");

            }

            double pressure = port.Pressure;
            double tmp = port.Temperature;

            if (tempVsPressure.ContainsKey(pressure)) 
            {
                tempVsPressure[pressure] = new KeyValuePair<double, double>(port.Depth, (tempVsPressure[pressure].Value + tmp) / 2.0);
            }
            else 
            { 
                tempVsPressure.Add(pressure, new KeyValuePair<double, double>(port.Depth, tmp)); 
            }

        }

        private void port_DepthUpdated(object sender, EventArgs e)
        {
            InvokeSetText(depthLbl, port.Depth.ToString("F02", CultureInfo.InvariantCulture));
        }

        private void port_TemperatureUpdated(object sender, EventArgs e)
        {
            InvokeSetText(temperatureLbl, port.Temperature.ToString("F01", CultureInfo.InvariantCulture));

            double dpt = port.Depth;
            double tmp = port.Temperature;

            if (tempVsDepth.ContainsKey(dpt)) { tempVsDepth[dpt] = (tempVsDepth[dpt] + tmp) / 2.0; }
            else { tempVsDepth.Add(dpt, tmp); }
        }

        private void port_BasesUpdated(object sender, EventArgs e)
        {
            InvokeSetText(base1Lbl, string.Format(CultureInfo.InvariantCulture, "{0:F01} dB / {1}", port.RedBASE_1.SNR, port.RedBASE_1.Status));
            if ((port.RedBASE_1.Status == BuoyStatusIDs.ALIVE) || (port.RedBASE_1.Status == BuoyStatusIDs.OK))
                InvokeSetForeColor(base1Lbl, Color.Green);
            else if (port.RedBASE_1.Status == BuoyStatusIDs.TIMEOUT)
                InvokeSetForeColor(base1Lbl, Color.Red);
            else InvokeSetForeColor(base1Lbl, Color.Black);

            InvokeSetText(base2Lbl, string.Format(CultureInfo.InvariantCulture, "{0:F01} dB / {1}", port.RedBASE_2.SNR, port.RedBASE_2.Status));
            if ((port.RedBASE_2.Status == BuoyStatusIDs.ALIVE) || (port.RedBASE_2.Status == BuoyStatusIDs.OK))
                InvokeSetForeColor(base2Lbl, Color.Green);
            else if (port.RedBASE_2.Status == BuoyStatusIDs.TIMEOUT)
                InvokeSetForeColor(base2Lbl, Color.Red);
            else InvokeSetForeColor(base2Lbl, Color.Black);

            InvokeSetText(base3Lbl, string.Format(CultureInfo.InvariantCulture, "{0:F01} dB / {1}", port.RedBASE_3.SNR, port.RedBASE_3.Status));
            if ((port.RedBASE_3.Status == BuoyStatusIDs.ALIVE) || (port.RedBASE_3.Status == BuoyStatusIDs.OK))
                InvokeSetForeColor(base3Lbl, Color.Green);
            else if (port.RedBASE_3.Status == BuoyStatusIDs.TIMEOUT)
                InvokeSetForeColor(base3Lbl, Color.Red);
            else InvokeSetForeColor(base3Lbl, Color.Black);

            InvokeSetText(base4Lbl, string.Format(CultureInfo.InvariantCulture, "{0:F01} dB / {1}", port.RedBASE_4.SNR, port.RedBASE_4.Status));
            if ((port.RedBASE_4.Status == BuoyStatusIDs.ALIVE) || (port.RedBASE_4.Status == BuoyStatusIDs.OK))
                InvokeSetForeColor(base4Lbl, Color.Green);
            else if (port.RedBASE_4.Status == BuoyStatusIDs.TIMEOUT)
                InvokeSetForeColor(base4Lbl, Color.Red);
            else InvokeSetForeColor(base4Lbl, Color.Black);
        }

        private void port_PositionUpdated(object sender, EventArgs e)
        {
            InvokeSetText(latLbl, Angle2Str(port.Latitude, true));
            InvokeSetText(lonLbl, Angle2Str(port.Longitude, false));

            InvokeSetText(rerrLbl, port.RadialError.ToString("F03", CultureInfo.InvariantCulture));

            InvokeSetText(depthLbl, port.Depth.ToString("F02", CultureInfo.InvariantCulture));

            if (port.RadialError <= settingsProvider.Data.RadialErrorThreshold)
            {
                InvokeSetForeColor(rerrLbl, Color.Green);

                GeoPlot.GeoCoordinate2D newPoint = new GeoPlot.GeoCoordinate2D();
                newPoint.Latitude = port.Latitude;
                newPoint.Longitude = port.Longitude;

                InvokeAddRNPoint(newPoint);

                TrackRecord trk = new TrackRecord();
                trk.Location = new GeoPoint3DE(port.Latitude, port.Longitude, port.Depth, port.RadialError);
                trk.Buoy0Location = new GeoPoint(port.RedBASE_1.Latitude, port.RedBASE_1.Longitude);
                trk.Buoy1Location = new GeoPoint(port.RedBASE_2.Latitude, port.RedBASE_2.Longitude);
                trk.Buoy2Location = new GeoPoint(port.RedBASE_3.Latitude, port.RedBASE_3.Longitude);
                trk.Buoy3Location = new GeoPoint(port.RedBASE_4.Latitude, port.RedBASE_4.Longitude);

                track.Add(trk);

            }
            else
                InvokeSetForeColor(rerrLbl, Color.Red);

            locHyst = 0;
            onceLocated = true;
        }

        private void port_GGAEvent(object sender, GGAEventArgs e)
        {
            /*
            if ((!double.IsNaN(e.Latitude)) && (!double.IsNaN(e.Longitude)))
            {
                InvokeSetText(gnssLatLbl, Angle2Str(e.Latitude, true));
                InvokeSetText(gnssLonLbl, Angle2Str(e.Longitude, false));

                GeoPlot.GeoCoordinate2D newPoint = new GeoPlot.GeoCoordinate2D();
                newPoint.Latitude = e.Latitude;
                newPoint.Longitude = e.Longitude;

                InvokeAddGNSSPoint(newPoint);

                gnssTrack.Add(new GeoPoint(e.Latitude, e.Longitude));

                double dist = GetDistance2D(port.Latitude, port.Longitude, e.Latitude, e.Longitude);
                InvokeSetText(lblgnssDiffLbl, dist.ToString("F03", CultureInfo.InvariantCulture));
            }
            */
        }

        private void port_RMCEvent(object sender, RMCEventArgs e)
        {
            /*
            if ((!double.IsNaN(e.Latitude)) && (!double.IsNaN(e.Longitude)))
            {
                InvokeSetText(gnssLatLbl, Angle2Str(e.Latitude, true));
                InvokeSetText(gnssLonLbl, Angle2Str(e.Longitude, false));

                GeoPlot.GeoCoordinate2D newPoint = new GeoPlot.GeoCoordinate2D();
                newPoint.Latitude = e.Latitude;
                newPoint.Longitude = e.Longitude;

                InvokeAddGNSSPoint(newPoint);

                gnssTrack.Add(new GeoPoint(e.Latitude, e.Longitude));

                double dist = GetDistance2D(port.Latitude, port.Longitude, e.Latitude, e.Longitude);
                InvokeSetText(lblgnssDiffLbl, dist.ToString("F03", CultureInfo.InvariantCulture));
            }
            */
        }

        #endregion                               

        #region gnssPort

        private void gnssPort_GLL(object sender, GLLEventArgs e)
        {
            if ((!double.IsNaN(e.Latitude)) && (!double.IsNaN(e.Longitude)))
            {
                InvokeSetText(gnssLatLbl, Angle2Str(e.Latitude, true));
                InvokeSetText(gnssLonLbl, Angle2Str(e.Longitude, false));

                GeoPlot.GeoCoordinate2D newPoint = new GeoPlot.GeoCoordinate2D();
                newPoint.Latitude = e.Latitude;
                newPoint.Longitude = e.Longitude;

                InvokeAddGNSSPoint(newPoint);

                gnssTrack.Add(new GeoPoint(e.Latitude, e.Longitude));

                double dist = GetDistance2D(port.Latitude, port.Longitude, e.Latitude, e.Longitude);
                InvokeSetText(lblgnssDiffLbl, dist.ToString("F03", CultureInfo.InvariantCulture));
            }
        }

        #endregion                                
                        
        #endregion        
    }
}
