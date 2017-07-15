using Engineer.Engine;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    public class CollisionChecker
    {
        public static List<DrawnSceneObject> CheckSceneCollision(Scene CurrentScene, SceneObject CurrentObject)
        {
            List<DrawnSceneObject> Collides = new List<DrawnSceneObject>();
            for(int i = 0; i < CurrentScene.Objects.Count; i++)
            {
                if (CurrentScene.Objects[i].ID == CurrentObject.ID) continue;
                if (CurrentScene.Objects[i].Data.ContainsKey("Collision"))
                {
                    if (((Sprite)CurrentObject.Visual).InCollision(CurrentScene.Objects[i].Visual, (Collision2DType)CurrentScene.Objects[i].Data["Collision"]))
                    {
                        Collides.Add((DrawnSceneObject)CurrentScene.Objects[i]);
                    }
                }
            }
            return Collides;
        }
    }
}
