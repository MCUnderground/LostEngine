using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
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
        public bool isRef = false;
        BodyDef bodyDef;
        Body body;

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
        public Sprite2D(string Directory)
        {
            this.isRef = true;
            this.Directory = Directory;

            Image temp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(temp);
            Sprite = sprite;

            Log.Info($"(Sprite2D) {Name} has been created.");
            LostEngine.RegisterSprite(this);
        }
        public Sprite2D(Vector2 Position, Vector2 Scale, string Name, Sprite2D refrence)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Name = Name;

            Sprite = refrence.Sprite;

            Log.Info($"(Sprite2D) {Name} has been created.");
            LostEngine.RegisterSprite(this);
        }
        public void CreateStatic()
        {
            bodyDef = new BodyDef();
            bodyDef.Position = new Vec2(this.Position.X, this.Position.Y);

            body = LostEngine.world.CreateBody(bodyDef);
            PolygonDef shapeDef = new PolygonDef();

            shapeDef.SetAsBox(1.0f, 1.0f);

            body.CreateShape(shapeDef);
        }

        public void CreateDynamic()
        {
            bodyDef = new BodyDef();
            bodyDef.Position = new Vec2(this.Position.X, this.Position.Y);
            body = LostEngine.world.CreateBody(bodyDef);

            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox(Scale.X, Scale.Y);

            shapeDef.Density = 1.0f;

            shapeDef.Friction = 0.3f;

            body.CreateShape(shapeDef);
            body.SetMassFromShapes();
        }

        public void AddForce(Vector2 force)
        {
            body.SetLinearVelocity(new Vec2(force.X, force.Y));
        }

        public void UpdatePosition()
        {
            this.Position.X = body.GetPosition().X;
            this.Position.Y = body.GetPosition().Y;
        }
        public bool IsColliding(Sprite2D a, Sprite2D b)
        {
            if (a.Position.X < b.Position.X + b.Scale.X &&
                a.Position.X + a.Scale.X > b.Position.X &&
                a.Position.Y < b.Position.Y + b.Scale.Y &&
                a.Position.Y + a.Scale.Y > b.Position.Y)
            {
                return true;
            }
            return false;
        }

        public bool IsColliding(string name)
        {
            foreach (Sprite2D b in LostEngine.Sprites)
                if (b.Name == name)
                {
                   if (Position.X < b.Position.X + b.Scale.X &&
                        Position.X + Scale.X > b.Position.X &&
                        Position.Y < b.Position.Y + b.Scale.Y &&
                        Position.Y + Scale.Y > b.Position.Y)
                    {
                        return true;
                    }
                }
            return false;
        }
        public void Destroy()
        {

            Log.Info($"(Sprite2D) {Name} has been destroyed.");
            LostEngine.RemoveSprite(this);
        }
    }
}
