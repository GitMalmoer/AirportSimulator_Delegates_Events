using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Assignment_2_ApuAnimalPark.Objects.ListManager;

namespace AirportSimulator_MarcinJunka
{
    public class Airplane
    {
        public string FlightId { get; set; }
        public string Name { get; set; }

        public string Destination { get; set; }
        public double FlightTime { get; set; }
        public bool CanLand { get; set; }
        public DateTime LocalTime { get; set; }

        private DispatcherTimer dispatcherTimer;

        public event EventHandler<AirplaneEventArgs> SentToRunaway;
        public event EventHandler<AirplaneEventArgs> TakeOff;
        public event EventHandler<AirplaneEventArgs> Landed;

        public void SendToRunaway()
        {

        }

        public void OnSentToRunaway(AirplaneEventArgs e)
        {
            SentToRunaway?.Invoke(this,e);
        }

        public void OnLanding(AirplaneEventArgs e)
        {
            if (CanLand)
            {
                Landed?.Invoke(this, e);
            }
        }

        public void OnTakeOff(AirplaneEventArgs e)
        {
            TakeOff?.Invoke(this, e);
            SetupTimer(); // this triggers dispatchertimer_tick which triggers Onlanding when its finish.
        }

        public void SetupTimer()
        {
            //MessageBox.Show("Timer started");
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            CanLand = true;
            TimeSpan duration = TimeSpan.FromSeconds(FlightTime);

            // AFTER x seconds it stops the timer and trigers the onloading event
            DateTime landingTime = DateTime.Now;
            TimeSpan elapsed = landingTime - LocalTime; // this makes accuracy a little better

            if (elapsed >= duration)
            {
                dispatcherTimer.Stop();
                OnLanding(new AirplaneEventArgs(landingTime.ToString(), "landing"));
            }
        }

        public override string ToString()
        {
            string strOut = string.Format("{0}, {1} heading for {2}",Name,FlightId,Destination);
            return strOut;
        }
    }
}
