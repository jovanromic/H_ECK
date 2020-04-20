using H_ECK.GameUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace H_ECK.GameElements
{
    public class Clock
    {
        public Timer Timer { get; set; }
        public int Seconds { get; set; }
        public IGameDisplay GameDisplay{get;set;}
        public Clock()
        {
            Timer = new Timer(1000);
            Timer.Elapsed += Timer_Elapsed;
            Timer.Enabled = true;
            Seconds = 300;
            GameDisplay = new ConsoleDisplay();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GameDisplay.DisplayTimeLeft(Seconds);          
            if (Seconds == 0)
            {
                GameDisplay.DisplayMessage("\nTime is up! Game over!");
                Environment.Exit(0);
            }
            Seconds--;
        }

        public void Start()
        {
            Timer.Start();
        }
        public void Reset()
        {
            Seconds = 300;
        }

    }
}
