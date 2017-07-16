using Engineer.Engine;
using Engineer.Engine.IO;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{    
    class HealthBar : DrawnSceneObject
    {

        public int health;

        public HealthBar(string Name, DrawObject Visual) : base(Name, Visual) 
        {
            health = 100;
        }


        public static HealthBar Create()
        {
            Vertex Positon = new Vertex(10, 10, 0);
            Vertex Size = new Vertex(400, 50, 0);

            TileCollection BItmaps = new TileCollection(ResourceManager.Images["sombrero"]);

            Tile SomethingOnScene = new Tile();

            SomethingOnScene.Collection = BItmaps;

            SomethingOnScene.Translation = Positon;
            SomethingOnScene.Scale = Size;
            HealthBar HB = new HealthBar("HealthBar", SomethingOnScene);
            HB.Data["Static"] = true;
            return HB;
        }

        public void subHealth(int healths)
        {
            this.health -= healths;
            float width = (health / 100.0f) * 400; 
            Visual.Scale = new Vertex(width, Visual.Scale.Y, 0);

        }
       
        public bool empty()
        {
            return this.health <= 0;
        }

    }
}
