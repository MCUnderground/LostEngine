using LostEngine.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostEngine.Engine
{
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Name = "";
        public Color Color = Color.White;

        public Shape2D(Vector2 Position, Vector2 Scale, string Name, Color Color)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Name = Name;
            this.Color = Color;

            Log.Info($"[Shape2D] {Name} has been created.");
            LostEngine.RegisterShape(this);
        }
        public void Destroy()
        {

            Log.Info($"[Shape2D] {Name} has been destroyed.");
            LostEngine.RemoveShape(this);
        }
    }
}
