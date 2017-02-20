﻿using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace RedNODEHost
{
    public sealed class NMEASerialPort : NMEAPort, IDisposable
    {
        #region Properties

        bool disposed = false;
        int writeLock = 0;
        int readLock = 0;
        SerialPort serialPort;

        public SerialPortSettings PortSettings { get; private set; }

        #region NMEAPort

        public override bool IsOpen
        {
            get { return serialPort.IsOpen; }
        }

        #endregion

        #endregion

        #region Constructor

        public NMEASerialPort(SerialPortSettings portSettings)
            : base()
        {
            #region serialPort initialization

            if (portSettings == null)
                throw new ArgumentNullException("portSettings");

            PortSettings = portSettings;

            serialPort = new SerialPort(
                PortSettings.PortName,
                (int)PortSettings.PortBaudRate,
                PortSettings.PortParity,
                (int)PortSettings.PortDataBits,
                PortSettings.PortStopBits);

            serialPort.Handshake = portSettings.PortHandshake;
            serialPort.Encoding = Encoding.ASCII;
            serialPort.WriteTimeout = 1000;
            serialPort.ReadTimeout = 1000;

            serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(serialPort_ErrorReceived);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

            #endregion
        }

        #endregion

        #region Methods

        #region NMEAPort

        public override void Open()
        {
            while (Interlocked.CompareExchange(ref writeLock, 1, 0) != 0)
                Thread.SpinWait(1);

            while (Interlocked.CompareExchange(ref readLock, 1, 0) != 0)
                Thread.SpinWait(1);

            try
            {
                OnConnectionOpening();
                serialPort.Open();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Error occured during opening port {1}: {0}", ex.Message, serialPort.PortName));
            }
            finally
            {
                Interlocked.Decrement(ref readLock);
                Interlocked.Decrement(ref writeLock);
            }
        }

        public override void Close()
        {
            while (Interlocked.CompareExchange(ref writeLock, 1, 0) != 0)
                Thread.SpinWait(1);

            while (Interlocked.CompareExchange(ref readLock, 1, 0) != 0)
                Thread.SpinWait(1);

            try
            {
                OnConnectionClosing();
                serialPort.Close();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Error occured during closing port {1}: {0}", ex.Message, serialPort.PortName));
            }
            finally
            {
                Interlocked.Decrement(ref readLock);
                Interlocked.Decrement(ref writeLock);
            }
        }

        public override void SendData(string message)
        {
            while (Interlocked.CompareExchange(ref writeLock, 1, 0) != 0)
                Thread.SpinWait(1);

            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(message);
                serialPort.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Error {0} while writing in port {1}", ex.Message, serialPort.PortName));
            }
            finally
            {
                Interlocked.Decrement(ref writeLock);
            }
        }

        #endregion

        #endregion

        #region Handlers

        #region serialPort

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (Interlocked.CompareExchange(ref readLock, 1, 0) != 0)
                Thread.SpinWait(1);

            int bytesToRead = serialPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            serialPort.Read(buffer, 0, bytesToRead);

            Interlocked.Decrement(ref readLock);

            OnIncomingData(Encoding.ASCII.GetString(buffer));
        }

        private void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (PortError != null)
                PortError(this, e);
        }

        #endregion

        #endregion

        #region Events

        public EventHandler<SerialErrorReceivedEventArgs> PortError;

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
                    serialPort.Dispose();
                }

                disposed = true;
            }
        }

        #endregion
    }

}
