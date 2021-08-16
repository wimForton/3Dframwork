using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameEngine
{
    public interface IAnimationControl
    {
        public AnimatableParameter MyAnimatableParameter { get; set; }
        public Grid AnimCtrlGrid { get; set; }
        public MySlider mySlider { get; set; }
        public double Value { get; set; }
        public Button SetKeyButton { get; set; }
        public virtual void UpdateControl(double inValue) { }
    }
}
