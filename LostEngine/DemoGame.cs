using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostEngine.Engine;
using LostEngine.Engine.Utils;

namespace LostEngine.Demo
{
    class DemoGame : Engine.LostEngine
    {
        Shape2D player;
        public DemoGame() : base(new Vector2(512, 512), "Demo Game") { }

        public override void OnLoad()
        {
            Log.Normal("Game loaded..");
            BackgroundColor = Color.Aquamarine;
            player = new Shape2D(new Vector2(50, 50), new Vector2(20,20), "player", Color.Red);
        }

        public override void OnDraw()
        {
            
        }

        public override void OnUpdate()
        {
        }
    }
}
