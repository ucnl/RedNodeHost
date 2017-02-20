using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RedNODEHost
{
    /// <summary>
    /// Enum для режимов работы таймера
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// Таймер срабатывает один раз
        /// </summary>
        OneShot,
        /// <summary>
        /// Таймер срабатывает периодически
        /// </summary>
        Periodic
    };

    /// <summary>
    /// Диапазон для периода таймера
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TimerCaps
    {
        /// <summary>
        /// Минимальный период
        /// </summary>
        public int periodMin;
        /// <summary>
        /// Максимальный период
        /// </summary>
        public int periodMax;
    }

    /// <summary>
    /// Класс, реализующий точный системный таймер
    /// </summary>
    public sealed class PrecisionTimer : IComponent
    {
        private delegate void TimeProc(int id, int msg, int user, int param1, int param2);

        private delegate void EventRaiser(EventArgs e);

        [DllImport("winmm.dll")]
        private static extern int timeGetDevCaps(ref TimerCaps caps,
                                                 int sizeOfTimerCaps);

        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay,
                                               int resolution,
                                               TimeProc proc,
                                               int user,
                                               int mode);

        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);

        private const int TIMERR_NOERROR = 0;

        private int timerID;

        private volatile Mode mode;

        private volatile int period;

        private volatile int resolution;

        private TimeProc timeProcPeriodic;

        private TimeProc timeProcOneShot;

        private EventRaiser tickRaiser;

        private bool running = false;

        private volatile bool disposed = false;

        private ISynchronizeInvoke synchronizingObject = null;

        private ISite site = null;

        private static TimerCaps caps;

        /// <summary>
        /// Событие "Таймер запущен"
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Событие "Таймер остановлен"
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Событие "Тик таймера"
        /// </summary>
        public event EventHandler Tick;

        static PrecisionTimer()
        {
            // Get multimedia timer capabilities.
            int hResult = timeGetDevCaps(ref caps, Marshal.SizeOf(caps));

            // TODO: if (hResult != 0) ...
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="container">Контейнер</param>
        public PrecisionTimer(IContainer container)
        {
            container.Add(this);
            Initialize();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public PrecisionTimer()
        {
            Initialize();
        }

        /// <summary>
        /// Деструктор класса
        /// </summary>
        ~PrecisionTimer()
        {
            if (IsRunning)
            {
                // Stop and destroy timer.
                int hResult = timeKillEvent(timerID);

                Debug.Assert(hResult == TIMERR_NOERROR);
            }
        }

        private void Initialize()
        {
            this.mode = Mode.Periodic;
            this.period = Capabilities.periodMin;
            this.resolution = 1;

            running = false;

            timeProcPeriodic = new TimeProc(TimerPeriodicEventCallback);
            timeProcOneShot = new TimeProc(TimerOneShotEventCallback);
            tickRaiser = new EventRaiser(OnTick);
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        public void Start()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Timer");
            }

            if (IsRunning)
            {
                return;
            }

            if (Mode == Mode.Periodic)
            {
                timerID = timeSetEvent(Period, Resolution, timeProcPeriodic, 0, (int)Mode);
            }
            else
            {
                timerID = timeSetEvent(Period, Resolution, timeProcOneShot, 0, (int)Mode);
            }

            if (timerID != 0)
            {
                running = true;

                if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
                {
                    SynchronizingObject.BeginInvoke(
                        new EventRaiser(OnStarted),
                        new object[] { EventArgs.Empty });
                }
                else
                {
                    OnStarted(EventArgs.Empty);
                }
            }
            else
            {
                throw new TimerException("Unable to start timer.");
            }
        }

        /// <summary>
        /// Остановка таймера
        /// </summary>
        public void Stop()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Timer");
            }

            if (!running)
            {
                return;
            }

            int result = timeKillEvent(timerID);

            Debug.Assert(result == TIMERR_NOERROR);

            running = false;

            if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
            {
                SynchronizingObject.BeginInvoke(
                    new EventRaiser(OnStopped),
                    new object[] { EventArgs.Empty });
            }
            else
            {
                OnStopped(EventArgs.Empty);
            }
        }

        private void TimerPeriodicEventCallback(int id, int msg, int user, int param1, int param2)
        {
            if (synchronizingObject != null)
            {
                synchronizingObject.BeginInvoke(tickRaiser, new object[] { EventArgs.Empty });
            }
            else
            {
                OnTick(EventArgs.Empty);
            }
        }

        private void TimerOneShotEventCallback(int id, int msg, int user, int param1, int param2)
        {
            if (synchronizingObject != null)
            {
                synchronizingObject.BeginInvoke(tickRaiser, new object[] { EventArgs.Empty });
                Stop();
            }
            else
            {
                OnTick(EventArgs.Empty);
                Stop();
            }
        }

        // Raises the Disposed event.
        private void OnDisposed(EventArgs e)
        {
            EventHandler handler = Disposed;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Raises the Started event.
        private void OnStarted(EventArgs e)
        {
            EventHandler handler = Started;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Raises the Stopped event.
        private void OnStopped(EventArgs e)
        {
            EventHandler handler = Stopped;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Raises the Tick event.
        private void OnTick(EventArgs e)
        {
            EventHandler handler = Tick;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Аксессор для маршалинга вызовов хэндлеров
        /// </summary>
        public ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                #region Require

                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }

                #endregion

                return synchronizingObject;
            }
            set
            {
                #region Require

                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }

                #endregion

                synchronizingObject = value;
            }
        }

        /// <summary>
        /// Период таймера
        /// </summary>
        public int Period
        {
            get
            {
                #region Require

                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }

                #endregion

                return period;
            }
            set
            {
                #region Require

                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                else if (value < Capabilities.periodMin || value > Capabilities.periodMax)
                {
                    throw new ArgumentOutOfRangeException("Period", value,
                        "Multimedia Timer period out of range.");
                }

                #endregion

                period = value;

                if (IsRunning)
                {
                    Stop();
                    Start();
                }
            }
        }

        /// <summary>
        /// Разрешение таймера (точность)
        /// </summary>
        public int Resolution
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }

                return resolution;
            }
            set
            {
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                else if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Resolution", value,
                        "timer resolution out of range.");
                }

                resolution = value;

                if (IsRunning)
                {
                    Stop();
                    Start();
                }
            }
        }

        /// <summary>
        /// Режим работы таймера
        /// </summary>
        public Mode Mode
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }

                return mode;
            }
            set
            {
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }

                mode = value;

                if (IsRunning)
                {
                    Stop();
                    Start();
                }
            }
        }

        /// <summary>
        /// Показывает, запущен таймера или нет
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return running;
            }
        }

        /// <summary>
        /// Возвращает диапазон для периода таймера
        /// </summary>
        public static TimerCaps Capabilities
        {
            get
            {
                return caps;
            }
        }

        /// <summary>
        /// Событие по освобождению ресурсов
        /// </summary>
        public event System.EventHandler Disposed;

        /// <summary>
        /// ISite
        /// </summary>
        public ISite Site
        {
            get
            {
                return site;
            }
            set
            {
                site = value;
            }
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            if (IsRunning)
            {
                Stop();
            }

            GC.SuppressFinalize(this);
            disposed = true;

            OnDisposed(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Класс, реализующий исключение от таймера
    /// </summary>
    public class TimerException : ApplicationException
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="message">Сообщение</param>
        public TimerException(string message)
            : base(message)
        {
        }
    }

}
