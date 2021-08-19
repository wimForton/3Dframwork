using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameEngine
{

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
        private int frame;
        public int Frame
        {
            get { return this.frame; }

            set
            {
                if (value != this.frame)
                {
                    this.frame = value;
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
