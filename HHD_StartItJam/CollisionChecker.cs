using Engineer.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    public class CollisionChecker
    {
        public static SceneObject CheckSceneCollision(Scene CurrentScene, SceneObject CurrentObject)
        {
            for(int i = 0; i < CurrentScene.Objects.Count; i++)
            {
                if (CurrentScene.Objects[i].ID == CurrentObject.ID) continue;
                if (CurrentScene.Objects[i].Data.ContainsKey("Collision") && (bool)CurrentScene.Objects[i].Data["Collision"] == true)
                {
                    if (((Sprite)CurrentObject.Visual).InCollision(CurrentScene.Objects[i].Visual, Engineer.Mathematics.Collision2DType.Rectangular))
                    {
                        return CurrentScene.Objects[i];
                    }
                }
            }
            return null;
        }
    }
}
