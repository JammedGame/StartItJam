using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HHD_StartItJam
{
    class Level
    {      

        public static void Create(Scene2D CScene)
        {


            DrawnSceneObject Back = CreateStaticSprite("Back", ResourceManager.Images["Back"], new Vertex(0, 0, 0), new Vertex(1920, 900, 0));
            CScene.AddSceneObject(Back);
            DrawnSceneObject Surface = CreateStaticSprite("Surface", ResourceManager.Images["Surface"], new Vertex(0, 900, 0), new Vertex(1920, 300, 0), true);
            CScene.AddSceneObject(Surface);
            DrawnSceneObject Floor = CreateStaticSprite("Floor", ResourceManager.Images["Floor"], new Vertex(300, 850, 0), new Vertex(1200, 50, 0), true);
            CScene.AddSceneObject(Floor);
            DrawnSceneObject Ceiling = CreateStaticSprite("Ceiling", ResourceManager.Images["Floor"], new Vertex(300, 250, 0), new Vertex(1200, 50, 0), true);
            CScene.AddSceneObject(Ceiling);
            DrawnSceneObject BackWall = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall"], new Vertex(300, 300, 0), new Vertex(300, 550, 0));
            CScene.AddSceneObject(BackWall);
            DrawnSceneObject BackWall2 = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall"], new Vertex(600, 300, 0), new Vertex(300, 550, 0));
            CScene.AddSceneObject(BackWall2);
            DrawnSceneObject BackWall3 = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall"], new Vertex(900, 300, 0), new Vertex(300, 550, 0));
            CScene.AddSceneObject(BackWall3);
            DrawnSceneObject BackWall4 = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall"], new Vertex(1200, 300, 0), new Vertex(300, 550, 0));
            CScene.AddSceneObject(BackWall4);
            DrawnSceneObject LeftWall = CreateStaticSprite("LeftWall", ResourceManager.Images["Wall"], new Vertex(300, 300, 0), new Vertex(50, 550, 0));
            CScene.AddSceneObject(LeftWall);
            DrawnSceneObject RightWall = CreateStaticSprite("RightWall", ResourceManager.Images["Wall"], new Vertex(1450, 300, 0), new Vertex(50, 550, 0));
            CScene.AddSceneObject(RightWall);
        }

        public static DrawnSceneObject CreateStaticSprite(string Name, Bitmap Image, Vertex Positon, Vertex Size, bool Colidable = false)
        {
            Positon = new Vertex(Positon.X , Positon.Y  , 0);
            Size = new Vertex(Size.X , Size.Y , 0);

            TileCollection BItmaps = new TileCollection(Image);

            Tile SomethingOnScene = new Tile();

            SomethingOnScene.Collection = BItmaps;

            SomethingOnScene.Translation = Positon;
            SomethingOnScene.Scale = Size;
            DrawnSceneObject Static = new DrawnSceneObject(Name, SomethingOnScene);
            Static.Data["Colision"] = Colidable;
            return Static;
        }
    }
}
