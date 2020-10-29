using LostEngine.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box2DX.Collision;
using Box2DX.Dynamics;
using Box2DX.Common;

namespace LostEngine.Engine
{
    class Canvas : Form{ public Canvas() { this.DoubleBuffered = true; } }
    public abstract class LostEngine
    {
        private Vector2 ScreenSize = new Vector2(640, 480);
        private string GameName = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        private static List<Shape2D> Shapes = new List<Shape2D> ();
        public static List<Sprite2D> Sprites = new List<Sprite2D>();

        public System.Drawing.Color BackgroundColor = System.Drawing.Color.Black;

        public Vector2 CameraScale = new Vector2(1,1);
        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;

        AABB worldAABB = new AABB
        {
            UpperBound = new Vec2(2000, 2000),
            LowerBound = new Vec2(-2000, -20000)
        };
        Vec2 gravity = new Vec2(0.0f, 10.0f);
        public static World world = null;

        public LostEngine(Vector2 ScreenSize, string GameName) 
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.GameName = GameName;

            Window = new Canvas();
            Window.Size = new System.Drawing.Size((int)this.ScreenSize.X,(int)this.ScreenSize.Y + SystemInformation.CaptionHeight);
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Window.Text = this.GameName;
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.KeyPress += Window_KeyPress;
            Window.FormClosing += Window_FormClosing;


            world = new World(worldAABB, gravity, false);

            OnLoad();
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();


            Application.Run(Window);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }

        private void Window_KeyPress(object sender, KeyPressEventArgs e) { GetKeyPress(e); }

        private void Window_KeyUp(object sender, KeyEventArgs e) { GetKeyUp(e); }

        private void Window_KeyDown(object sender, KeyEventArgs e) { GetKeyDown(e); }

        public static void RegisterShape(Shape2D shape) { Shapes.Add(shape); }
        public static void RemoveShape(Shape2D shape) { Shapes.Remove(shape); }

        public static void RegisterSprite(Sprite2D sprite) { Sprites.Add(sprite); }
        public static void RemoveSprite(Sprite2D sprite)  {  Sprites.Remove(sprite); }
        void GameLoop() 
        {
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(10);
                }
                catch { Log.Error("Window not found... Waiting..."); };
            }
        }
        float timeStep = 1.0f / 60.0f;
        int velocityIterations = 8;
        int positionIterations = 1;
        private void Renderer(object sender, PaintEventArgs e)
        {
            world.Step(timeStep, velocityIterations, positionIterations);

            Graphics graphics = e.Graphics;
            graphics.Clear(BackgroundColor);


            graphics.TranslateTransform(CameraPosition.X, CameraPosition.Y);
            graphics.RotateTransform(CameraAngle);
            graphics.ScaleTransform(CameraScale.X,CameraScale.Y);
            foreach (Shape2D shape in Shapes)
            {
                graphics.FillRectangle(new SolidBrush(shape.Color), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
            }
            foreach (Sprite2D sprite in Sprites)
            {
                if(!sprite.isRef)
                    graphics.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
        public abstract void GetKeyPress(KeyPressEventArgs e);

    }
}
