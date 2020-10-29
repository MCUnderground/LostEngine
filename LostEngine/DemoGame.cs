using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LostEngine.Engine;
using LostEngine.Engine.Utils;

namespace LostEngine.Demo
{
    class DemoGame : Engine.LostEngine
    {

        Sprite2D player;
        bool up = false;
        bool down = false;
        bool right = false;
        bool left = false;
        Sprite2D ground;

        Vector2 lastPosition = Vector2.Zero();

        string[,] Map =
        {
            {"g","g","g","g","g","g","g","g","g","g","g"},
            {".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","."},
            {"g","g","g","g","g","g","g","g","g","g","g"},
        };

        public DemoGame() : base(new Vector2(500, 500), "Demo Game") { }

        public override void OnLoad()
        {
            Log.Normal("Game loaded...");
            player = new Sprite2D(new Vector2(100, 100), new Vector2(50,100), "player", "Env/Object/Sign_2");
            player.CreateDynamic();
            ground = new Sprite2D("Env/Tiles/18");
            CameraScale = new Vector2(1f, 1f);

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), "ground", ground).CreateStatic(); ;
                }
            }

            
        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {
            player.UpdatePosition();
            if (left)
                player.AddForce(new Vector2(-100, 0));
            if(right)
                player.AddForce(new Vector2(100, 0));
            if (up)
                player.AddForce(new Vector2(0, -100));
            if (down)
                player.AddForce(new Vector2(0, 100));
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                left = true;
            if (e.KeyCode == Keys.W)
                up = true;
            if (e.KeyCode == Keys.S)
                down = true;
            if (e.KeyCode == Keys.D)
                right = true;
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                left = false;
            if (e.KeyCode == Keys.W)
                up = false;
            if (e.KeyCode == Keys.S)
                down = false;
            if (e.KeyCode == Keys.D)
                right = false;
        }

        public override void GetKeyPress(KeyPressEventArgs e)
        {
            
        }
    }
}
