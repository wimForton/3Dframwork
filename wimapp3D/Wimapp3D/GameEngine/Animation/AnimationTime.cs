using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameEngine
{
    //class AnimationTime
    //{
    //    public EventHandler TimeChanged;
    //    public static double Time { get; set; } = 0;
    //    public static double FramesPerSecond { get; set; } = 25;

    //    //public double Time
    //    //{
    //    //    get
    //    //    {
    //    //        return time;
    //    //    }
    //    //    set
    //    //    {
    //    //        time = value;
    //    //        //this.TimeChanged(this, EventArgs.Empty);
    //    //    }
    //    //}
    //}
    public sealed class AnimationTime : INotifyPropertyChanged
    {
        private static readonly AnimationTime instance = new AnimationTime();
        private AnimationTime() { }

        public static AnimationTime Instance
        {
            get
            {
                return instance;
            }
        }

        // notifying property
        private double time;
        public double Time
        {
            get { return this.time; }

            set
            {
                if (value != this.time)
                {
                    this.time = value;
                    NotifyPropertyChanged("MyProp");
                }
            }
        }


        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
