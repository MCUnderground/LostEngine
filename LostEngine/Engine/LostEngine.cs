using LostEngine.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private static List<Sprite2D> Sprites = new List<Sprite2D>();

        public Color BackgroundColor = Color.Black;

        public LostEngine(Vector2 ScreenSize, string GameName) 
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.GameName = GameName;

            Window = new Canvas();
            Window.Size = new System.Drawing.Size((int)this.ScreenSize.X,(int)this.ScreenSize.Y);
            Window.Text = this.GameName;
            Window.Paint += Renderer;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        public static void RegisterShape(Shape2D shape)
        {
            Shapes.Add(shape);
        }
        public static void RemoveShape(Shape2D shape)
        {
            Shapes.Remove(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            Sprites.Add(sprite);
        }
        public static void RemoveSprite(Sprite2D sprite)
        {
            Sprites.Remove(sprite);
        }
        void GameLoop() 
        {
            OnLoad();
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
        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(BackgroundColor);

            foreach (Shape2D shape in Shapes)
            {
                graphics.FillRectangle(new SolidBrush(shape.Color), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
            }
            foreach (Sprite2D sprite in Sprites)
            {
                graphics.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
    }
}
