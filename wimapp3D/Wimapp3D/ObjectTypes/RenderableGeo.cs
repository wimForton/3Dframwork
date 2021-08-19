using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace GameEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract class RenderableGeo : IRenderableGeo
    {
        public int Id { get; set; } = -1;
        public static List<int> SelectedObjects { get; set; }
        [JsonProperty]
        public List<int> ChildGeoNodeIds { get; set; } = new List<int>();
        [JsonProperty]
        public string Name { get; set; } = "Unnamed";
        [JsonProperty]
        public Vector Position { get; set; } = new Vector(0, 0, 0);
        [JsonProperty]
        public Vector Rotation { get; set; } = new Vector(0, 0, 0);
        [JsonProperty]
        public Vector Scale { get; set; } = new Vector(1, 1, 1);
        [JsonProperty]
        public Vector GuiNodePosition { get; set; } = new Vector(10, 10, 10);
        [JsonProperty]
        public List<AnimatableParameter> AnimatableParameters { get; set; }
        public static int HighestId { get; set; } = -1;
        public static int OffsetId { get; set; } = 0;
        public IRenderableGeo InputObject { get; set; }
        [JsonProperty]
        public bool isRootGeoNode { get; set; } = false;
        public List<IRenderableGeo> ChildGeoNodes { get; set; } = new List<IRenderableGeo>();
        public static IRenderableGeo ChildLookingForGeoParent { get; set; }
        //[JsonProperty]
        public List<IAnimationControl> AnimationControls { get; set; }
        public NodeGuiElement GuiNode { get; set; }
        public PropertyControllerGrid PropertyGrid { get; set; }
        public List<Polygon> Polygons { get; set; } = new List<Polygon>();
        public List<Vector> Points { get; set; } = new List<Vector>();
        public List<Vector> UVs { get; set; } = new List<Vector>();
        public List<Vector> Normals { get; set; } = new List<Vector>();
        public List<Particle> myParticles { get; set; } = new List<Particle>();
        public bool NeedsUpdate { get; set; } = true;
        public bool OutputNeedsUpdate { get; set; } = true;
        public bool NeedsInputObject { get; set; } = false;
        public List<float> myVaoList = new List<float>();
        public float[] VaoArray;
        public abstract void Update();
        public virtual void UpdateVAO() { }
        public virtual void OpenProportiesWindow() { }
        public virtual void CheckProportiesWindow() { }
        public RenderableGeo() 
        {
            IRenderableGeo.HighestId++;
            Id = IRenderableGeo.HighestId + OffsetId;
            


            AnimationTime.Instance.PropertyChanged += Instance_PropertyChanged;
        }
        internal void SetKeyButton_Click(object sender, RoutedEventArgs e)
        {
            int myValue = (int)((Button)sender).Tag;
            AnimatableParameters[myValue].SetKeyAtFrame(AnimationControls[myValue].Value, AnimationTime.Instance.Frame);
        }
        internal void KeyAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < AnimatableParameters.Count; i++)
            {
                AnimatableParameters[i].SetKeyAtFrame(AnimationControls[i].Value, AnimationTime.Instance.Frame);
            }
        }
        internal void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AnimationControls != null)
            {
                for (int i = 0; i < AnimationControls.Count; i++)
                {
                    AnimationControls[i].UpdateControl(AnimatableParameters[i].GetValueAtFrame(AnimationTime.Instance.Frame));
                }
            }
        }

        internal void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            WriteJson();
        }

        public void MakeVaoList()
        {
            myVaoList.Clear();
            if (Polygons.Count > 0)
            {
                foreach (var poly in Polygons)//We use only triangles in our engine 
                {
                    VertexToVao(0, poly);
                    VertexToVao(1, poly);
                    VertexToVao(2, poly);
                    if (poly.Vertices.Count == 4)//create 2 triangles per quad in the right order (or 1triangle per triangle)
                    {
                        VertexToVao(2, poly);
                        VertexToVao(3, poly);
                        VertexToVao(0, poly);
                    }
                }
                MakeVaoArray();
            }
        }
        public virtual void MakeVaoArray()
        {
            VaoArray = myVaoList.ToArray();
        }
        public void VertexToVao(int inVertexIndex, Polygon poly)
        {
            //List<float> myVaoList = new List<float>();
            Vector myPoint = Points[poly.Vertices[inVertexIndex]];
            myVaoList.Add((float)myPoint.X);
            myVaoList.Add((float)myPoint.Y);
            myVaoList.Add((float)myPoint.Z);
            Vector myUV = UVs[poly.UVs[inVertexIndex]];
            myVaoList.Add((float)myUV.X);//OpenGL takes only 2 UV coordinates
            myVaoList.Add((float)myUV.Y);
            Vector myNormal = Normals[poly.Normals[inVertexIndex]];
            myVaoList.Add((float)myNormal.X);
            myVaoList.Add((float)myNormal.Y);
            myVaoList.Add((float)myNormal.Z);
            //Vector myColor = poly.Colors[inVertexIndex];
            //myVaoList.Add((float)myColor.X);
            //myVaoList.Add((float)myColor.Y);
            //myVaoList.Add((float)myColor.Z);
        }
        public float[] GetVaoArray()//We just return the list
        {
            return VaoArray;
        }
        public virtual void WriteJson()
        {

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            serializer.TypeNameHandling = TypeNameHandling.All;

            using (StreamWriter sw = new StreamWriter(@"H:\cursus_informatica\3Dapplication\jsonsinglenode.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }
    }
}
