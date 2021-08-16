using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameEngine
{
    class Twist : RenderableGeo, IRenderableGeo
    {
        public double TwistAmount { get; set; } = 0.0;
        public double TwistOffset { get; set; } = 0.0;

        public Twist(IRenderableGeo inObject = null) : base()
        {
            isRootGeoNode = false;
            Name = "Twist";
            NeedsInputObject = true;
            NeedsUpdate = true;
            if(inObject != null)
            {
                InputObject = inObject;
                InputObject.ChildGeoNodes.Add(this);
                Points = InputObject.Points.ToList();//make local
                UVs = inObject.UVs;
                Normals = inObject.Normals;
                Polygons = inObject.Polygons;
                NeedsUpdate = true;
            }
            else
            {
                NeedsUpdate = false;
            }
            if (AnimatableParameters == null)
            {
                AnimatableParameters = new List<AnimatableParameter>()
                {
                    new AnimatableParameter(0),
                    new AnimatableParameter(0)
                };
            }
            if (AnimationControls == null)
            {
                AnimationControls = new List<IAnimationControl>
                {
                    new KeyFrameSlider(0, "Twist", TwistAmount, -5, 5, 0.01),
                    new KeyFrameSlider(0, "Offset", TwistOffset, -5, 5, 0.01)
                };
                for (int i = 0; i < AnimationControls.Count; i++)
                {
                    PropertyGrid.ControlsStackPanel.Children.Add(AnimationControls[i].AnimCtrlGrid);
                    AnimationControls[i].mySlider.ValueChanged += Sliders_ValueChanged;
                    AnimationControls[i].SetKeyButton.Click += SetKeyButton_Click;
                }
            }
            if (GuiNode == null)
            {
                GuiNode = new NodeGuiElement(this);
                Wimapp3D.MainWindow.AppWindow.MainWindowCanvas.Children.Add(GuiNode);
            }
        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TwistAmount = AnimationControls[0].mySlider.Value;
            TwistOffset = AnimationControls[1].mySlider.Value;
            NeedsUpdate = true;
        }

        public override void OpenProportiesWindow()
        {
            if(Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.IndexOf(PropertyGrid) < 0)
            {
                Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.Add(PropertyGrid);
            }
        }

        public override void Update()
        {
            //CheckProportiesWindow();
            if (InputObject.OutputNeedsUpdate || NeedsUpdate)
            {
                BuildObject();
                MakeVaoList();
                OutputNeedsUpdate = true;
                NeedsUpdate = false;
            }
        }

        private void BuildObject()
        {
            Points = InputObject.Points.ToList();
            UVs = InputObject.UVs;
            Normals = InputObject.Normals;
            Polygons = InputObject.Polygons;
            Random myRandom = new Random(123);
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] = MakeTwist(Points[i]);
            }
        }

        private Vector MakeTwist(Vector vector)
        {
            Vector result = new Vector(0,0,0);
            result = Vector.GetEulerRotation(vector, 0, 0, (vector.Z + TwistOffset) * TwistAmount, "zxy");
            return result;
        }
    }
}
