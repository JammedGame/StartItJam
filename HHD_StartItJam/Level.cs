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
            //DrawnSceneObject Back = CreateStaticSprite("Back", ResourceManager.Images["Back"], new Vertex(0, 0, 0), new Vertex(1920, 900, 0));
            //CScene.AddSceneObject(Back);
            DrawnSceneObject Surface = CreateStaticSprite("Surface", ResourceManager.Images["Surface"], new Vertex(0, 900, 0), new Vertex(1920, 1000, 0), true);
            CScene.AddSceneObject(Surface);



            CreateRoom(CScene, 300, 4, 0);
            CreateRoom(CScene, 300, 3, 1);

            DrawnSceneObject Stairs = CreateStaticSprite("Stairs", ResourceManager.Images["Stairs"], new Vertex(450, 250, 0), new Vertex(120, 600, 0));
            Stairs.Data["Stairs"] = Stairs;
            CScene.AddSceneObject(Stairs);

            DrawnSceneObject Stairs2 = CreateStaticSprite("Stairs", ResourceManager.Images["Stairs"], new Vertex(850, 250, 0), new Vertex(120, 600, 0));
            Stairs2.Data["Stairs"] = Stairs2;
            CScene.AddSceneObject(Stairs2);
        }
        public static void CreateRoom(Scene2D CScene, int Location, int Length, int Level)
        {
            for (int i = 0; i < Length; i++)
            {
                CreateWallPart(CScene, new Vertex(Location + i * 300, Level * (-600) + 250, 0), Level);
            }
            DrawnSceneObject LeftWall = CreateStaticSprite("LeftWall", ResourceManager.Images["Wall"], new Vertex(Location, Level * (-600) + 300, 0), new Vertex(30, 550, 0), true, Collision2DType.Vertical);
            CScene.AddSceneObject(LeftWall);
            DrawnSceneObject RightWall = CreateStaticSprite("RightWall", ResourceManager.Images["Wall"], new Vertex(Location + Length * 300 - 30, Level * (-600) + 300, 0), new Vertex(30, 550, 0), true, Collision2DType.Vertical);
            CScene.AddSceneObject(RightWall);
        }
        public static void CreateWallPart(Scene CScene, Vertex Location, int Level)
        {
            DrawnSceneObject Ceiling = CreateStaticSprite("Ceiling", ResourceManager.Images["Ceiling"], Location, new Vertex(300, 50, 0), true);
            CScene.AddSceneObject(Ceiling);
            if (Level == 0)
            {
                DrawnSceneObject Floor = CreateStaticSprite("Floor", ResourceManager.Images["Floor"], new Vertex(Location.X, Location.Y + 600, 0), new Vertex(300, 50, 0), true);
                CScene.AddSceneObject(Floor);
            }
            DrawnSceneObject BackWall = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall"], new Vertex(Location.X, Location.Y + 50, 0), new Vertex(300, 550, 0));
            CScene.AddSceneObject(BackWall);
        }
        public static DrawnSceneObject CreateStaticSprite(string Name, Bitmap Image, Vertex Positon, Vertex Size, bool Collision = false, Collision2DType ColType = Collision2DType.Focus)
        {
            Positon = new Vertex(Positon.X , Positon.Y  , 0);
            Size = new Vertex(Size.X , Size.Y , 0);

            TileCollection BItmaps = new TileCollection(Image);

            Tile SomethingOnScene = new Tile();

            SomethingOnScene.Collection = BItmaps;

            SomethingOnScene.Translation = Positon;
            SomethingOnScene.Scale = Size;
            DrawnSceneObject Static = new DrawnSceneObject(Name, SomethingOnScene);
            if (Collision) Static.Data["Collision"] = ColType;
            return Static;
        }
    }
}
