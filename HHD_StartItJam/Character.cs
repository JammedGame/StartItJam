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
        /*public static DrawnSceneObject Create(Scene2D CScene)
        {
            return Character.CreateCharacter(CScene);
        }
        private static DrawnSceneObject CreateCharacter(Scene2D CScene)
        {
            EFXInterface EFX = new EFXInterface();
            DrawnSceneObject Char = (DrawnSceneObject)EFX.Load("Data/knight.efx");
            ((Sprite)Char.Visual).SetSpriteSet("Idle");
            ((Sprite)Char.Visual).Paint = Color.White;
            Char.Visual.Scale = new Vertex(250, 250, 0);
            Char.Visual.Translation = new Vertex(960 - 125, 540 - 125, 0);

            CScene.AddSceneObject(Char);

            return Char;
        }*/

        public static DrawnSceneObject Create(Scene2D CScene)
        {
            return Character.CreateCharacter(CScene);
        }
        private static DrawnSceneObject CreateCharacter(Scene2D Scene)
        {


            SpriteSet WalkR = new SpriteSet("WalkR");
            for (int i = 0; i < 27; i++) WalkR.Sprite.Add(ResourceManager.Images["Dwalk" + i]);
            SpriteSet WalkL = new SpriteSet("WalkL");
            for (int i = 0; i < 27; i++) WalkL.Sprite.Add(ResourceManager.Images["DwalkL" + i]);

            SpriteSet JumpL = new SpriteSet("JumpR");
            for (int i = 0; i < 13; i++) JumpL.Sprite.Add(ResourceManager.Images["jump" + i]);
            SpriteSet JumpR = new SpriteSet("JumpL");
            for (int i = 1; i < 14; i++) JumpR.Sprite.Add(ResourceManager.Images["jumpL" + i]);

            //SpriteSet AttR = new SpriteSet("AttR");
            //for (int i = 0; i < 2; i++) AttR.Sprite.Add(ResourceManager.Images["attR" + i]);
            //SpriteSet AttL = new SpriteSet("AttL");
            //for (int i = 0; i < 2; i++) AttL.Sprite.Add(ResourceManager.Images["attL" + i]);

            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(WalkR);
            CharSprite.SpriteSets.Add(WalkL);
            CharSprite.SpriteSets.Add(JumpL);
            CharSprite.SpriteSets.Add(JumpR);

            CharSprite.Scale = new Vertex(250, 250, 0);
            CharSprite.Translation = new Vertex(960 - 125, 540 - 125, 0);
                       
            DrawnSceneObject Char = new DrawnSceneObject("Death", CharSprite);

            Scene.AddSceneObject(Char);

            return Char;
        }
    }
}

