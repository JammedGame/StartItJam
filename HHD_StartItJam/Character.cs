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

            SpriteSet Up = new SpriteSet("Up");
            for (int i = 0; i < 3; i++) Up.Sprite.Add(ResourceManager.Images["up" + i]);

            SpriteSet DattL = new SpriteSet("DattL");
            for (int i = 0; i < 5; i++) DattL.Sprite.Add(ResourceManager.Images["Dwalk" + i]);

            SpriteSet DattR = new SpriteSet("DattR");
            for (int i = 0; i < 5; i++) DattR.Sprite.Add(ResourceManager.Images["Dwalk" + i]);

            SpriteSet ScytheR = new SpriteSet("ScytheR");
            for (int i = 0; i < 27; i++) ScytheR.Sprite.Add(ResourceManager.Images["scythe" + i]);

            SpriteSet ScytheL = new SpriteSet("ScytheL");
            for (int i = 0; i < 27; i++) ScytheL.Sprite.Add(ResourceManager.Images["scytheL" + i]);
            
            SpriteSet AttR = new SpriteSet("AttR");
            for (int i = 0; i < 5; i++) AttR.Sprite.Add(ResourceManager.Images["ScAttR" + i]);
            SpriteSet AttL = new SpriteSet("AttL");
            for (int i = 0; i < 5; i++) AttL.Sprite.Add(ResourceManager.Images["ScAtt" + i]);

            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(WalkR);
            CharSprite.SpriteSets.Add(WalkL);
            CharSprite.SpriteSets.Add(JumpL);
            CharSprite.SpriteSets.Add(JumpR);
            CharSprite.SpriteSets.Add(Up);

            Sprite ScytheSprite = new Sprite();
            ScytheSprite.SpriteSets.Add(ScytheL);
            ScytheSprite.SpriteSets.Add(ScytheR);
            ScytheSprite.SpriteSets.Add(AttR);
            ScytheSprite.SpriteSets.Add(AttL);
            
            DrawnSceneObject Char = new DrawnSceneObject("Death", CharSprite);

            Char.Data["ScytheSprite"] = ScytheSprite;            

            DrawnSceneObject Sc = new DrawnSceneObject("Scythe", ScytheSprite);      
           
            Char.Visual.Scale = new Vertex(250, 250, 0);
            Char.Visual.Translation = new Vertex(960 - 125, 540 - 125, 0);

            Sc.Visual.Scale = new Vertex(250, 250, 0);
            Sc.Visual.Translation = new Vertex(960 - 125, 540 - 125, 0);
               

            Char.Data["Scythe"] = Sc;           
                        
            Scene.AddSceneObject(Char);
            Scene.AddSceneObject(Sc);            

            return Char;
        }
        public static void UpdateScythe(DrawnSceneObject Char)
        {
            ((DrawnSceneObject)Char.Data["Scythe"]).Visual.Translation = new Vertex(Char.Visual.Translation.X, Char.Visual.Translation.Y, 0);            
        }
            
    }
}

