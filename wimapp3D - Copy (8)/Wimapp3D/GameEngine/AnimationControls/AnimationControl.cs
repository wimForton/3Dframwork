using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GameEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract class AnimationControl : IAnimationControl
    {
        [JsonProperty]
        public AnimatableParameter MyAnimatableParameter { get; set; }
        public Grid AnimCtrlGrid { get; set; }
        public MySlider mySlider { get; set; }
        public double Value { get; set; }
        public Button SetKeyButton { get; set; }
        public virtual void UpdateControl(double inValue) { }
    }
}
