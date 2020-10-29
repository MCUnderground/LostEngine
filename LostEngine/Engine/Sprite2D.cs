using LostEngine.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostEngine.Engine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Name = "";
        public string Directory = "";
        public Bitmap Sprite = null;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Name, string Directory)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Name = Name;
            this.Directory = Directory;

            Image temp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(temp);
            Sprite = sprite;

            Log.Info($"(Sprite2D) {Name} has been created.");
            LostEngine.RegisterSprite(this);
        }
        public void Destroy()
        {

            Log.Info($"(Sprite2D) {Name} has been destroyed.");
            LostEngine.RemoveSprite(this);
        }
    }
}
