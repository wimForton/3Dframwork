﻿using System;
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
    class Bend : RenderableGeo, IRenderableGeo
    {
        public double BendAmount { get; set; } = 0.0;
        public double TwistOffset { get; set; } = 0.0;
        public double BendLength { get; set; } = 1.0;

        public Bend(IRenderableGeo inObject = null) : base()
        {
            isRootGeoNode = false;
            Name = "Bend";
            PropertyGrid = new PropertyControllerGrid(Name);
            Button SaveButton = MyButton.CreateButton("Save Json file");
            SaveButton.Click += SaveButton_Click;
            PropertyGrid.ControlsStackPanel.Children.Add(SaveButton);
            Button KeyAll = MyButton.CreateButton("Key All");
            KeyAll.Click += KeyAll_Click;
            PropertyGrid.ControlsStackPanel.Children.Add(KeyAll);
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
                    new KeyFrameSlider(0, "Bend", BendAmount, -5, 5, 0.01),
                    new KeyFrameSlider(0, "Bend Length", BendLength, -5, 5, 0.01)
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
            BendAmount = AnimationControls[0].mySlider.Value;
            BendLength = AnimationControls[1].mySlider.Value;
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
                Points[i] = MakeBend(Points[i]);
            }
        }

        private Vector MakeBend(Vector invector)
        {
            //Vector dcut = new Vector(0.5, 0,0);
            Vector result = new Vector(0, 0, 0);
            double factor = BendAmount * Math.PI;// * BendScale

            ////Vector axis = set(0.8, 1, 0);
            Vector temp = new Vector(invector.X, invector.Y, invector.Z); //invector;
            //if(temp.X > BendLength)
            //{
            //    temp.X = BendLength;
            //}
            double theta = temp.X * factor;
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            if (factor != 0 && invector.X > 0)
            {
                result.X = -(temp.Y - 1.0 / factor) * sint;
                result.Y = (temp.Y - 1.0 / factor) * cost + 1.0 / factor;
                result.Z = temp.Z;
            }
            else
            {
                result = invector;
            }

            //result.X += cost * dcut.X;
            //result.Y += sint * dcut.X;
            //result.Z += dcut.Z;

            return result;
        }
    }
}
