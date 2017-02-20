
using System;
using System.Collections.Generic;
using System.IO.Ports;
using UCNLNMEA;

namespace RedNODEHost
{
    #region Utils

    public struct SatelliteData
    {
        #region Properties

        public int PRNNumber;
        public int Elevation;
        public int Azimuth;
        public int SNR;

        #endregion

        #region Constructor

        public SatelliteData(int prn, int elevation, int azimuth, int snr)
        {
            PRNNumber = prn;
            Elevation = elevation;
            Azimuth = azimuth;
            SNR = snr;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Format("PRN: {0:00}, Elevation: {1:00}°, Azimuth: {2:000}°, SNR: {3:00} dB", PRNNumber, Elevation, Azimuth, SNR);
        }

        #endregion
    }

    #endregion

    #region Custom eventArgs

    public class GLLEventArgs : EventArgs
    {
        #region Properties

        public TalkerIdentifiers TalkerID { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime TimeFix { get; private set; }

        #endregion

        #region Constructor

        public GLLEventArgs(TalkerIdentifiers talkerID, double lat, double lon, DateTime timeFix)
        {
            TalkerID = talkerID;
            Latitude = lat;
            Longitude = lon;
            TimeFix = timeFix;
        }

        #endregion
    }

    public class GGAEventArgs : EventArgs
    {
        #region Properties

        public TalkerIdentifiers TalkerID { get; private set; }
        public string GPSQualityIndicator { get; private set; }
        public DateTime TimeFix { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public int SatellitesInUse { get; private set; }
        public double PrecisionHorizontalDilution { get; private set; }
        public double AntennaAltitude { get; private set; }
        public double GeoidalSeparation { get; private set; }
        public int DifferentianReferenceStation { get; private set; }

        #endregion

        #region Constructor

        public GGAEventArgs(TalkerIdentifiers talkerID, string gpsQualityIndicator, DateTime timeFix, double lat, double lon, int satsInUse, double dohp, double antennaAlt, double gsep, int drs)
        {
            TalkerID = talkerID;
            GPSQualityIndicator = gpsQualityIndicator;
            TimeFix = timeFix;
            Latitude = lat;
            Longitude = lon;
            SatellitesInUse = satsInUse;
            PrecisionHorizontalDilution = dohp;
            AntennaAltitude = antennaAlt;
            GeoidalSeparation = gsep;
            DifferentianReferenceStation = drs;
        }

        #endregion
    }

    public class GSVEventArgs : EventArgs
    {
        #region Properties

        public TalkerIdentifiers TalkerID { get; private set; }
        public SatelliteData[] SatellitesData { get; private set; }

        #endregion

        #region Constructor

        public GSVEventArgs(TalkerIdentifiers taklerID, SatelliteData[] satsData)
        {
            TalkerID = taklerID;
            SatellitesData = satsData;
        }

        #endregion
    }

    public class VTGEventArgs : EventArgs
    {
        #region Properties

        public TalkerIdentifiers TalkerID { get; private set; }
        public double TrackTrue { get; private set; }
        public double TrackMagnetic { get; private set; }
        public double SpeedKmh { get; private set; }

        #endregion

        #region Constructor

        public VTGEventArgs(TalkerIdentifiers talkerID, double trackTrue, double trackMagnetic, double speedKmh)
        {
            TalkerID = talkerID;
            TrackTrue = trackTrue;
            TrackMagnetic = TrackMagnetic;
            SpeedKmh = speedKmh;
        }

        #endregion
    }

    public class RMCEventArgs : EventArgs
    {
        #region Properties

        public TalkerIdentifiers TalkerID { get; private set; }
        public DateTime TimeFix { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double SpeedKmh { get; private set; }
        public double TrackTrue { get; private set; }
        public double MagneticVariation { get; private set; }

        #endregion

        #region Constructor

        public RMCEventArgs(TalkerIdentifiers talkerID, DateTime timeFix, double lat, double lon, double speedKmh, double trackTrue, double mVar)
        {
            TalkerID = talkerID;
            TimeFix = timeFix;
            Latitude = lat;
            Longitude = lon;
            SpeedKmh = speedKmh;
            TrackTrue = trackTrue;
            MagneticVariation = mVar;
        }

        #endregion
    }

    #endregion

    public class GNSSReceiverWrapper : IDisposable
    {
        #region Properties

        NMEASerialPort port;
        private delegate void ProcessCommandDelegate(TalkerIdentifiers talkerID, object[] parameters);

        private Dictionary<UCNLNMEA.SentenceIdentifiers, ProcessCommandDelegate> cmdProcessor;

        delegate T NullChecker<T>(object parameter);
        NullChecker<int> intNullChecker = (x => x == null ? -1 : (int)x);
        NullChecker<double> doubleNullChecker = (x => x == null ? double.NaN : (double)x);
        NullChecker<string> stringNullChecker = (x => x == null ? string.Empty : (string)x);


        public SerialPortSettings PortSettings
        {
            get
            {
                return port.PortSettings;
            }
        }

        public bool IsOpen { get { return port.IsOpen; } }

        List<SatelliteData> fullSatellitesData;

        bool disposed = false;

        #endregion

        #region Constructor

        public GNSSReceiverWrapper(SerialPortSettings portSettings)
        {
            #region port initialization

            port = new NMEASerialPort(portSettings);

            port.PortError += new EventHandler<System.IO.Ports.SerialErrorReceivedEventArgs>(port_PortError);
            port.NewNMEAMessage += new EventHandler<NewNMEAMessageEventArgs>(port_NewMessage);

            #endregion

            #region parsers initialization

            cmdProcessor = new Dictionary<SentenceIdentifiers, ProcessCommandDelegate>()
            {
                { UCNLNMEA.SentenceIdentifiers.GGA, new ProcessCommandDelegate(ProcessGGA)},
                { UCNLNMEA.SentenceIdentifiers.GSV, new ProcessCommandDelegate(ProcessGSV)},
                { UCNLNMEA.SentenceIdentifiers.GLL, new ProcessCommandDelegate(ProcessGLL)},
                { UCNLNMEA.SentenceIdentifiers.RMC, new ProcessCommandDelegate(ProcessRMC)},
                { UCNLNMEA.SentenceIdentifiers.VTG, new ProcessCommandDelegate(ProcessVTG)}                
            };

            #endregion
        }

        #endregion

        #region Methods

        #region Public

        public void Open()
        {
            port.Open();
        }

        public void Close()
        {
            port.Close();
        }

        public void SendRawCommand(string command)
        {
            port.SendData(command);
        }

        #endregion

        #region Private

        private void ProcessGGA(TalkerIdentifiers talkerID, object[] parameters)
        {
            if (GGAEvent != null)
            {
                var gpsQualityIndicator = (string)parameters[5];
                if (gpsQualityIndicator != "Fix not availible")
                {
                    try
                    {
                        var timeFix = (DateTime)parameters[0];
                        var lat = doubleNullChecker(parameters[1]);
                        var lon = doubleNullChecker(parameters[3]);
                        var satellitesInUse = intNullChecker(parameters[6]);
                        var precisionHorizontalDilution = doubleNullChecker(parameters[7]);
                        var antennaAltitude = doubleNullChecker(parameters[8]);
                        var antennaAltitudeUnits = (string)parameters[9];
                        var geoidalSeparation = doubleNullChecker(parameters[10]);
                        var geoidalSeparationUnits = (string)parameters[11];

                        var differentialReferenceStation = intNullChecker(parameters[12]);

                        GGAEvent(this,
                            new GGAEventArgs(
                                talkerID,
                                gpsQualityIndicator,
                                timeFix,
                                lat,
                                lon,
                                satellitesInUse,
                                precisionHorizontalDilution,
                                antennaAltitude,
                                geoidalSeparation,
                                differentialReferenceStation));
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }

        private void ProcessGSV(TalkerIdentifiers talkerID, object[] paramters)
        {
            if (GSVEvent != null)
            {
                try
                {
                    List<SatelliteData> satellites = new List<SatelliteData>();

                    int totalMessages = (int)paramters[0];
                    int currentMessageNumber = (int)paramters[1];

                    int satellitesDataItemsCount = (paramters.Length - 3) / 4;

                    for (int i = 0; i < satellitesDataItemsCount; i++)
                    {
                        satellites.Add(
                            new SatelliteData(
                                intNullChecker(paramters[3 + 4 * i]),
                                intNullChecker(paramters[4 + 4 * i]),
                                intNullChecker(paramters[5 + 4 * i]),
                                intNullChecker(paramters[6 + 4 * i])));
                    }

                    if (currentMessageNumber == 1)
                        fullSatellitesData = new List<SatelliteData>();

                    fullSatellitesData.AddRange(satellites.ToArray());

                    if (currentMessageNumber == totalMessages)
                        GSVEvent(this, new GSVEventArgs(talkerID, fullSatellitesData.ToArray()));
                }
                catch
                {
                    //
                }
            }
        }

        private void ProcessGLL(TalkerIdentifiers talkerID, object[] parameters)
        {
            if (GLLEvent != null)
            {
                try
                {
                    if (parameters[5].ToString() != "Invalid")
                    {

                        var timeFix = (DateTime)parameters[4];

                        var lat = (double)parameters[0];
                        var latC = (Cardinals)Enum.Parse(typeof(Cardinals), (string)parameters[1]);
                        if (latC == Cardinals.South)
                            lat = -lat;

                        var lon = (double)parameters[2];
                        var lonC = (Cardinals)Enum.Parse(typeof(Cardinals), (string)parameters[3]);
                        if (lonC == Cardinals.West)
                            lon = -lon;

                        GLLEvent(this, new GLLEventArgs(talkerID, lat, lon, timeFix));
                    }
                }
                catch
                {
                    //
                }
            }
        }

        private void ProcessVTG(TalkerIdentifiers talkerID, object[] parameters)
        {
            if (VTGEvent != null)
            {
                try
                {
                    var trackTrue = doubleNullChecker(parameters[0]);
                    var trackMagnetic = doubleNullChecker(parameters[2]);
                    var speedKnots = doubleNullChecker(parameters[4]);
                    var skUnits = (string)parameters[5];
                    var speedKmh = doubleNullChecker(parameters[6]);
                    var sKmUnits = (string)parameters[7];


                    VTGEvent(this, new VTGEventArgs(talkerID, trackTrue, trackMagnetic, speedKmh));
                }
                catch
                {
                    //
                }
            }
        }

        private void ProcessRMC(TalkerIdentifiers talkerID, object[] parameters)
        {
            if (RMCEvent != null)
            {
                try
                {
                    if (parameters[1].ToString() != "Invalid")
                    {
                        var timeFix = (DateTime)parameters[0];

                        var lat = (double)parameters[2];
                        var latC = (Cardinals)Enum.Parse(typeof(Cardinals), (string)parameters[3]);
                        if (latC == Cardinals.South)
                            lat = -lat;

                        var lon = (double)parameters[4];
                        var lonC = (Cardinals)Enum.Parse(typeof(Cardinals), (string)parameters[5]);
                        if (lonC == Cardinals.West)
                            lon = -lon;

                        var groundSpeed = (double)parameters[6];
                        var courseOverGround = (double)parameters[7];

                        var dateTime = (DateTime)parameters[8];

                        var magneticVariation = doubleNullChecker(parameters[9]);

                        RMCEvent(this, new RMCEventArgs(talkerID, timeFix, lat, lon, NMEAParser.NM2Km(groundSpeed), courseOverGround, magneticVariation));
                    }
                }
                catch
                {
                    //
                }
            }
        }

        #endregion

        #endregion

        #region Handlers

        #region port

        private void port_PortError(object sender, SerialErrorReceivedEventArgs e)
        {
            if (PortError != null)
                PortError.Invoke(this, e);
        }

        private void port_NewMessage(object sender, NewNMEAMessageEventArgs e)
        {
            try
            {
                var result = NMEAParser.Parse(e.Message);

                if (result is NMEAStandartSentence)
                {
                    var sResult = (result as NMEAStandartSentence);
                    if (cmdProcessor.ContainsKey(sResult.SentenceID))
                        cmdProcessor[sResult.SentenceID].Invoke(sResult.TalkerID, sResult.parameters);
                }
            }
            catch (Exception ex)
            {
                if (LogEvent != null) LogEvent(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        #endregion

        #endregion

        #region Events

        public EventHandler<GLLEventArgs> GLLEvent;
        public EventHandler<GGAEventArgs> GGAEvent;
        public EventHandler<GSVEventArgs> GSVEvent;
        public EventHandler<VTGEventArgs> VTGEvent;
        public EventHandler<RMCEventArgs> RMCEvent;
        public EventHandler<SerialErrorReceivedEventArgs> PortError;

        public EventHandler<LogEventArgs> LogEvent;

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    port.Dispose();
                }

                disposed = true;
            }
        }

        #endregion
    }
}
