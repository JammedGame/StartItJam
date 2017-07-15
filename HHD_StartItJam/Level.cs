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
            CreateWallPart(CScene, new Vertex(300, 250, 0), 0);
            CreateWallPart(CScene, new Vertex(600, 250, 0), 0);
            CreateWallPart(CScene, new Vertex(900, 250, 0), 0);
            CreateWallPart(CScene, new Vertex(1200, 250, 0), 0);
            DrawnSceneObject LeftWall = CreateStaticSprite("LeftWall", ResourceManager.Images["Wall"], new Vertex(300, 300, 0), new Vertex(50, 550, 0));
            CScene.AddSceneObject(LeftWall);
            DrawnSceneObject RightWall = CreateStaticSprite("RightWall", ResourceManager.Images["Wall"], new Vertex(1450, 300, 0), new Vertex(50, 550, 0));
            CScene.AddSceneObject(RightWall);
        }
        public static void CreateWallPart(Scene CScene, Vertex Location, int Level)
        {
            DrawnSceneObject Ceiling = CreateStaticSprite("Ceiling", ResourceManager.Images["Ceiling"], Location, new Vertex(300, 50, 0), true);
            CScene.AddSceneObject(Ceiling);
            DrawnSceneObject Floor = CreateStaticSprite("Floor", (Level == 0)?ResourceManager.Images["Floor"]: ResourceManager.Images["Ceiling"], new Vertex(Location.X, Location.Y + 600, 0), new Vertex(300, 50, 0), true);
            CScene.AddSceneObject(Floor);
            DrawnSceneObject BackWall = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall"], new Vertex(Location.X, Location.Y + 50, 0), new Vertex(300, 550, 0));
            CScene.AddSceneObject(BackWall);
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
            Static.Data["Collision"] = Colidable;
            return Static;
        }
    }
}
