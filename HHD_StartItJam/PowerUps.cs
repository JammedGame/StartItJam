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
    class PowerUps
    {
        public static int Tequillas = 0;
        public int potionCount;
        private List<DrawnSceneObject> Tequilas;

        public PowerUps(Scene Scene)
        {
            potionCount = 0;
            Tequilas = new List<DrawnSceneObject>();
            for (int i = 0; i < 3; i++)
            {
                Vertex Positon = new Vertex(1720 - 100 * i, 10, 0);
                Vertex Size = new Vertex(80, 100, 0);

                TileCollection BItmaps = new TileCollection(ResourceManager.Images["tequila"]);

                Tile SomethingOnScene = new Tile();
                SomethingOnScene.Collection = BItmaps;
                SomethingOnScene.Translation = Positon;
                SomethingOnScene.Scale = Size;
                DrawnSceneObject PU = new DrawnSceneObject("TequilaPowerup", SomethingOnScene);
                PU.Data["Static"] = true;
                PU.Active = false;
                Scene.AddSceneObject(PU);
                Tequilas.Add(PU);
            }
            Scene.Events.Extern.TimerTick += new GameEventHandler(GameUpdate);

        }


        public void GameUpdate(Game G, EventArguments E)
        {
            // TODO: Animate flashing element
        }

        public void increasePotionCount()
        {
            potionCount += 1;
            Tequillas++;
            for (int i = 0; i < potionCount; i++)
            {
                Tequilas[i].Active = true;
            }

        }

        public bool hasPowerUp()
        {
            return potionCount == 3;
        }
    }
}
