using Engineer.Engine;
using Engineer.Engine.IO;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    public class Character
    {
        public static DrawnSceneObject Create(Scene2D CScene)
        {
            return Character.CreateCharacter(CScene);
        }
        private static DrawnSceneObject CreateCharacter(Scene2D CScene)
        {
            EFXInterface EFX = new EFXInterface();
            DrawnSceneObject Char = (DrawnSceneObject)EFX.Load("Data/knight.efx");
            ((Sprite)Char.Visual).SetSpriteSet("Idle");
            ((Sprite)Char.Visual).SetSpriteSet("Walk");
            ((Sprite)Char.Visual).SetSpriteSet("Attack");

            Char.Visual.Scale = new Vertex(250 , 250 , 0);
            Char.Visual.Translation = new Vertex(100,900-Char.Visual.Scale.Y, 0);           

            CScene.AddSceneObject(Char);
                        
            return Char;
        }       
    }
}
