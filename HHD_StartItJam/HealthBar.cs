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
    class HealthBar
    {
        public static int health;
        private static List<DrawnSceneObject> _Sombreros;

        public static void Create(Scene CScene)
        {
            health = 100;
            _Sombreros = new List<DrawnSceneObject>();
            
            for(int i = 0; i < 5; i++)
            {
                TileCollection BItmaps = new TileCollection(ResourceManager.Images["sombrero"]);
                Tile SomethingOnScene = new Tile();
                SomethingOnScene.Collection = BItmaps;
                SomethingOnScene.Translation = new Vertex(10 + i * 110, 10, 0);
                SomethingOnScene.Scale = new Vertex(100, 100, 0);
                DrawnSceneObject DSO = new DrawnSceneObject("HealthBar", SomethingOnScene);
                DSO.Data["Static"] = true;
                _Sombreros.Add(DSO);
                CScene.AddSceneObject(DSO);
            }
        }

        public static void subHealth(int healths)
        {
            health -= healths;
            float width = (health / 100.0f) * 400;
            for (int i = 0; i < 5; i++) _Sombreros[i].Active = health > i * 20;

        }
       
        public static bool empty()
        {
            return health <= 0;
        }

    }
}
