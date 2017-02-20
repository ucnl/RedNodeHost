
using System;
using System.IO.Ports;
using UCNLNMEA;

namespace RedNODEHost
{
    #region Extra classes

    public class RedBASEState
    {
        public BuoyStatusIDs Status;
        public double SNR;
        public double Latitude;
        public double Longitude;
    }

    #endregion
    
    #region Custom event args

    public class LocalDataEventArgs : EventArgs
    {
        public LocalDataIDs LocalDataID { get; private set; }
        public double Value { get; private set; }

        public LocalDataEventArgs(LocalDataIDs localDataID, double value)
        {
            LocalDataID = localDataID;
            Value = value;
        }
    }

    public class ACKEventArgs : EventArgs
    {
        public ErrorIDs ErrorID { get; private set; }

        public ACKEventArgs(ErrorIDs errorID)
        {
            ErrorID = errorID;
        }
    }
        
    #endregion

    #region Custom enums
   
    public enum DeviceTypes
    {
        DEVICE_REDBASE = 0,
        DEVICE_REDNODE = 1,
        DEVICE_REDNAV = 2,
        DEVICE_REDGTR = 3,
        DEVICE_INVALID
    }

    public enum LocalDataIDs
    {
        LOC_DATA_DEVICE_INFO = 0,
        LOC_DATA_MAX_REMOTE_TIMEOUT = 1,
        LOC_DATA_MAX_SUBSCRIBERS = 2,
        LOC_DATA_DEPTH = 3,
        LOC_DATA_TEMPERATURE = 4,
        LOC_DATA_BAT_CHARGE = 5,

        LOC_DATA_PRESSURE_RATING = 6,
        LOC_DATA_ZERO_PRESSURE = 7,
        LOC_DATA_WATER_DENSITY = 8,
        LOC_DATA_SALINITY = 9,
        LOC_DATA_SOUNDSPEED = 10,
        LOC_DATA_GRAVITY_ACC = 11,

        LOC_DATA_YEAR = 12,
        LOC_DATA_MONTH = 13,
        LOC_DATA_DATE = 14,
        LOC_DATA_HOUR = 15,
        LOC_DATA_MINUTE = 16,
        LOC_DATA_SECOND = 17,

        LOC_DATA_UNKNOWN
    }

    public enum InvokeIDs
    {
        LOC_INVOKE_FLASH_WRITE     = 0,
        LOC_INVOKE_CLEAR_WAYPOINTS = 1,
        LOC_INVOKE_CLEAR_TRACK     = 2,
        LOC_INVOKE_WRITE_NDTABLE   = 3,
        LOC_INVOKE_DPT_ZERO_ADJUST = 4,
        LOC_INVOKE_UNKNOWN
    }

    public enum ErrorIDs
    {
        LOC_ERR_NO_ERROR = 0,
        LOC_ERR_INVALID_SYNTAX = 1,
        LOC_ERR_UNSUPPORTED = 2,
        LOC_ERR_TRANSMITTER_BUSY = 3,
        LOC_ERR_ARGUMENT_OUT_OF_RANGE = 4,
        LOC_ERR_INVALID_OPERATION = 5,
        LOC_ERR_UNKNOWN_FIELD_ID = 6,
        LOC_ERR_VALUE_UNAVAILIBLE = 7,
        LOC_ERR_RECEIVER_BUSY = 8,
        LOC_ERR_INVALID
    }

    public enum BuoyStatusIDs
    {
        NO_DATA = 0,
        TIMEOUT = 1,
        DISCHARGED = 2,
        OK = 3,
        ALIVE = 4,
        INVALID
    }

    #endregion

    public class RedNodePort : IDisposable
    {
        #region Properties

        bool disposed = false;

        NMEASerialPort port;
        SerialPortSettings portSettings;
        PrecisionTimer timer;
        
        public string SerialNumber { get; private set; }
        public string CoreMoniker { get; private set; }
        public string CoreVersion { get; private set; }
        public string SystemMoniker { get; private set; }
        public string SystemVersion { get; private set; }

        public bool IsOpen
        {
            get { return port.IsOpen; }
        }

        public bool IsDefined { get; private set; }


        public double Pressure { get; private set; }
        public double Depth { get; private set; }
        public double Temperature { get; private set; }

        public RedBASEState RedBASE_1 { get; private set; }
        public RedBASEState RedBASE_2 { get; private set; }
        public RedBASEState RedBASE_3 { get; private set; }
        public RedBASEState RedBASE_4 { get; private set; }

        public bool IsFix { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double RadialError { get; private set; }

        #endregion

        #region Constrcuctor

        public RedNodePort(string portName)
        {
            portSettings = new SerialPortSettings(portName, BaudRate.baudRate9600, Parity.None, DataBits.dataBits8, StopBits.One, Handshake.None);
           
            #region timers

            timer = new PrecisionTimer();
            timer.Mode = Mode.Periodic;
            timer.Period = 2000;
            timer.Tick += new EventHandler(timer_Tick);

            #endregion

            #region Handlers

            port = new NMEASerialPort(portSettings);
            port.NewNMEAMessage += new EventHandler<NewNMEAMessageEventArgs>(port_NewNMEAMessage);
            port.PortError += new EventHandler<SerialErrorReceivedEventArgs>(port_PortError);

            #endregion

            #region NMEA

            NMEAInit();

            #endregion

            #region other

            RedBASE_1 = new RedBASEState();
            RedBASE_2 = new RedBASEState();
            RedBASE_3 = new RedBASEState();
            RedBASE_4 = new RedBASEState();

            #endregion
        }

        #endregion

        #region Methods

        #region Private

        delegate T NullChecker<T>(object parameter);
        NullChecker<int> intNullChecker = (x => x == null ? -1 : (int)x);
        NullChecker<double> doubleNullChecker = (x => x == null ? double.NaN : (double)x);
        NullChecker<string> stringNullChecker = (x => x == null ? string.Empty : (string)x);

        private static string BCDVersionToStr(int versionData)
        {
            return string.Format("{0}.{1}", (versionData >> 0x08).ToString(), (versionData & 0xff).ToString("X2"));
        }

        private void NMEAInit()
        {
            NMEAParser.AddManufacturerToProprietarySentencesBase(ManufacturerCodes.TNT);

            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "0", "x,x");                   // ACK
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "1", "x,x");
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "2", "x,x");
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "3", "x,x");
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "4", "xx,xx");                  // LOC_DATA_GET
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "5", "x,x.x");                  // LOC_DATA_VAL
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "6", "xx,xx");                  // LOC_ACT_INVOKE

            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "!", "c--c,x,c--c,x,x,c--c");  // DEVICE_INFO
            
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "C", "x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x");
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "M", "x.x,x.x,x.x,x,x.x,x.x,x.x,x,x.x,x.x,x.x,x,x.x,x.x,x.x,x");
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "N", "x.x,x.x");
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "O", "x.x,x.x");
            NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "P", "xx,x.x");                // SETVAL                      
        }        
       
        #region Custom sentences processors

        private void Process_ACK(object[] parameters)
        {
            if (timer.IsRunning) { timer.Stop(); }

            ErrorIDs errorID = (ErrorIDs)((int)parameters[0]);
            
            if (ACKReceived != null) ACKReceived(this, new ACKEventArgs(errorID));
            if (PortReady != null) PortReady(this, new EventArgs());
        }

        private void Process_LOC_DATA(object[] parameters)
        {
            if (timer.IsRunning) { timer.Stop(); }

            LocalDataIDs localDataID = (LocalDataIDs)((int)(parameters[0]));
            double value = (double)parameters[1];

            if (LocalDataReceived != null) LocalDataReceived(this, new LocalDataEventArgs(localDataID, value));
            if (PortReady != null) PortReady(this, new EventArgs());
        }

        private void Process_DEVICE_INFO(object[] parameters)
        {
            var deviceType = (DeviceTypes)((int)parameters[4]);            

            if (deviceType == DeviceTypes.DEVICE_REDNODE)
            {
                if (timer.IsRunning) { timer.Stop(); }
                
                SystemMoniker = (string)parameters[0];
                SystemVersion = BCDVersionToStr((int)parameters[1]);
                CoreMoniker = (string)parameters[2];
                CoreVersion = BCDVersionToStr((int)parameters[3]);
                SerialNumber = (string)parameters[5];

                IsDefined = true;

                if (DeviceInfoUpdated != null) DeviceInfoUpdated(this, new EventArgs());
                
            }

            if (PortReady != null) PortReady(this, new EventArgs());
        }

        private void Process_PRETMP(object[] parameters)
        {
            Pressure = (double)parameters[0];

            // TODO: !!!
            //Pressure += 200;

            if (PressureUpdated != null) PressureUpdated(this, new EventArgs());
            
        }

        private void Process_DPTTMP(object[] parameters)
        {
            Depth = (double)parameters[0];

            /// TODO: !!!
            //Depth += 2;

            if (DepthUpdated != null) DepthUpdated(this, new EventArgs());
        }

        private void Process_BUOYS_STATUS(object[] parameters)
        {
            
            RedBASE_1.SNR = (double)parameters[2];
            RedBASE_1.Status = (BuoyStatusIDs)(int)parameters[3];
            
            RedBASE_2.SNR = (double)parameters[6];
            RedBASE_2.Status = (BuoyStatusIDs)(int)parameters[7];
            
            RedBASE_3.SNR = (double)parameters[10];
            RedBASE_3.Status = (BuoyStatusIDs)(int)parameters[11];

            RedBASE_4.SNR = (double)parameters[14];
            RedBASE_4.Status = (BuoyStatusIDs)(int)parameters[15];

            if (BasesUpdated != null) BasesUpdated(this, new EventArgs());
        }       

        private void Process_GGA(object[] parameters)
        {
            //
            
            //$GNGGA,123519,4807.038,N,01131.000,E,1,08,0.9,545.4,M,46.9,M,,*47
        }

        private void Process_RMC(object[] parameters)
        {
            //throw new NotImplementedException();
        }

        private void Process_MTW(object[] parameters)
        {
            Temperature = (double)parameters[0];

            /// TODO:

            //Temperature -= 10.5;

            if (TemperatureUpdated != null) TemperatureUpdated(this, new EventArgs());
        }

        private void Process_FIX_UPDATE(object[] parameters)
        {
            //$PTNTC,lat,lon,dpt,rerr,b0lat,b0lon,b1lat,b1lon,b2lat,b2lon,b3lat,b3lon,tmpC

            Latitude = (double)parameters[0];
            Longitude = (double)parameters[1];
            RadialError = (double)parameters[3];

            /// TODO:

            //RadialError /= 4;

            RedBASE_1.Latitude = (double)parameters[4];
            RedBASE_1.Longitude = (double)parameters[5];

            RedBASE_2.Latitude = (double)parameters[6];
            RedBASE_2.Longitude = (double)parameters[7];

            RedBASE_3.Latitude = (double)parameters[8];
            RedBASE_3.Longitude = (double)parameters[9];

            RedBASE_4.Latitude = (double)parameters[10];
            RedBASE_4.Longitude = (double)parameters[11];

            if (PositionUpdated != null) PositionUpdated(this, new EventArgs());

        }

        #endregion

        private void SafelySend(string msg)
        {
            try
            {
                port.SendData(msg);
                timer.Start();
                if (LogEvent != null) LogEvent(this, new LogEventArgs(LogLineType.INFO, string.Format(">> {0}", msg)));
            }
            catch (Exception ex)
            {
                if (LogEvent != null) LogEvent(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }


        #endregion

        #region Public

        public void Start()
        {
            IsDefined = false;

            if (!IsOpen)
            {
                port.Open();
                if (PortReady != null) PortReady(this, new EventArgs());
            }
            else
            {
                throw new InvalidOperationException("Already running");
            }
        }

        public void Stop()
        {
            if (timer.IsRunning) { timer.Stop(); }

            if ((port != null) && (port.IsOpen))
            {
                port.Close();                
            }
        }
              
        public string QueryDeviceInfo()
        {
            return NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "4", new object[] { (int)LocalDataIDs.LOC_DATA_DEVICE_INFO, 0 });
        }

        public string QueryLocDataGet(LocalDataIDs dataID)
        {
            return NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "4", new object[] { (int)dataID, 0 });
        }

        public string QueryLocDataSet(LocalDataIDs dataID, double value)
        {
            return NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "P", new object[] { (int)dataID, value });
        }

        public string QueryZeroDepthAdjust()
        {
            return NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "6", new object[] { (int)InvokeIDs.LOC_INVOKE_DPT_ZERO_ADJUST, 0 });
        }

        public void Send(string commands)
        {
            SafelySend(commands);
        }
       
        #endregion

        #endregion

        #region Handlers

        #region timer

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (QueryTimeout != null) QueryTimeout(this, new EventArgs());
            if (PortReady != null) PortReady(this, new EventArgs());
        }

        #endregion

        #region port

        private void port_NewNMEAMessage(object sender, NewNMEAMessageEventArgs e)
        {
            if (LogEvent != null) LogEvent(this, new LogEventArgs(LogLineType.INFO, string.Format("<< {0}", e.Message)));

            try
            {
                var sentence = NMEAParser.Parse(e.Message);

                if (sentence is NMEAProprietarySentence)
                {
                    NMEAProprietarySentence snt = ((NMEAProprietarySentence)(sentence));

                    if (snt.SentenceIDString == "0")
                    {
                        // ACK - analyze error code
                        Process_ACK(snt.parameters);
                    }                    
                    else if (snt.SentenceIDString == "5")
                    {
                        // local data value
                        Process_LOC_DATA(snt.parameters);
                    }
                    else if (snt.SentenceIDString == "!")
                    {
                        // device info
                        Process_DEVICE_INFO(snt.parameters);
                    }
                    else if (snt.SentenceIDString == "C")
                    {
                        Process_FIX_UPDATE(snt.parameters);
                    }
                    else if (snt.SentenceIDString == "M")
                    {
                        Process_BUOYS_STATUS(snt.parameters);
                    }
                    else if (snt.SentenceIDString == "N")
                    {
                        Process_DPTTMP(snt.parameters);
                    }
                    else if (snt.SentenceIDString == "O")
                    {
                        Process_PRETMP(snt.parameters);
                    }
                    else
                    {
                        if (LogEvent != null)
                            LogEvent(this, new LogEventArgs(LogLineType.ERROR, string.Format("Unsupported sentence ID: {0}", snt.SentenceIDString)));
                    }
                }
                else
                {
                    NMEAStandartSentence snt = ((NMEAStandartSentence)(sentence));

                    if (snt.SentenceID == SentenceIdentifiers.GGA)
                    {
                        Process_GGA(snt.parameters);
                    }
                    else if (snt.SentenceID == SentenceIdentifiers.RMC)
                    {
                        Process_RMC(snt.parameters);
                    }
                    else if (snt.SentenceID == SentenceIdentifiers.MTW)
                    {
                        Process_MTW(snt.parameters);
                    }
                    else
                    {
                        if (LogEvent != null)
                            LogEvent(this, new LogEventArgs(LogLineType.ERROR, string.Format("Unsupported sentence ID: {0}", snt.SentenceID)));
                    }
                }

            }
            catch (Exception ex)
            {
                if (LogEvent != null)
                    LogEvent(this, new LogEventArgs(LogLineType.ERROR, ex));
            }
        }

        private void port_PortError(object sender, SerialErrorReceivedEventArgs e)
        {
            if (LogEvent != null)
                LogEvent(this, new LogEventArgs(LogLineType.ERROR, e.EventType.ToString()));
        }
       
        #endregion

        #endregion

        #region Events
    
        public EventHandler<ACKEventArgs> ACKReceived;
        public EventHandler<LocalDataEventArgs> LocalDataReceived;

        public EventHandler DeviceInfoUpdated;
        public EventHandler PortReady;
        public EventHandler QueryTimeout;

        public EventHandler PressureUpdated;
        public EventHandler DepthUpdated;
        public EventHandler TemperatureUpdated;
        public EventHandler BasesUpdated;
        public EventHandler PositionUpdated;

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
                    if (timer != null) timer.Dispose();
                    if (port != null) port.Dispose();
                }

                disposed = true;
            }
        }

        #endregion
    }

}
