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
            ((Sprite)Char.Visual).Paint = Color.White;
            Char.Visual.Scale = new Vertex(250 , 250 , 0);
            Char.Visual.Translation = new Vertex(960 - 125, 540 - 125, 0);           

            CScene.AddSceneObject(Char);
            
             return Char;

            /*SpriteSet S1 = new SpriteSet("Idle", ResourceManager.Images["smrt1"]);
            SpriteSet S2 = new SpriteSet("Walk", ResourceManager.Images["smrt1"]);
            SpriteSet S3 = new SpriteSet("Attack", ResourceManager.Images["smrt1"]);
            S3.Sprite.Add(ResourceManager.Images["smrt1"]);
            S3.Sprite.Add(ResourceManager.Images["smrt2"]);
            S3.Sprite.Add(ResourceManager.Images["smrt2"]);
            S3.Sprite.Add(ResourceManager.Images["smrt3"]);
            S3.Sprite.Add(ResourceManager.Images["smrt3"]);
            S3.Sprite.Add(ResourceManager.Images["smrt4"]);
            S3.Sprite.Add(ResourceManager.Images["smrt4"]);
            SpriteSet S4 = new SpriteSet("Attack", ResourceManager.Images["smrt_penjac1"]);
            S4.Sprite.Add(ResourceManager.Images["smrt_penjac2"]);
            Sprite S = new Sprite();
            S.Scale = new Vertex(2f, 2f, 1);
            S.Translation = new Vertex(-0.666f, -0.666f, 0);
            S.Name = "Char";
            S.SpriteSets.Add(S1);
            S.SpriteSets.Add(S2);
            S.SpriteSets.Add(S3);
            S.SpriteSets.Add(S4);
            S.SetSpriteSet(0);
            S.Paint = Color.White;
            Sprite SS = new Sprite();
            SS.SubSprites.Add(S);
            SS.Paint = Color.FromArgb(0, 0, 0, 0);
            SS.Scale = new Vertex(250, 250, 0);
            SS.Translation = new Vertex(960 - 125, 540 - 125, 0);
            DrawnSceneObject DSO = new DrawnSceneObject("Player", SS);

            CScene.AddSceneObject(DSO);

            return DSO;*/
        }       
    }
}
