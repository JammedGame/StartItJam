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
        //Function to get random number
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
        public static bool[,] FloorTaken = new bool[10, 100];
        public static void Create(Scene2D CScene)
        {
            //DrawnSceneObject Back = CreateStaticSprite("Back", ResourceManager.Images["Back"], new Vertex(0, 0, 0), new Vertex(1920, 900, 0));
            //CScene.AddSceneObject(Back);
            DrawnSceneObject Surface = CreateStaticSprite("Surface", ResourceManager.Images["Surface"], new Vertex(0, 900, 0), new Vertex(1920, 1000, 0), true);
            CScene.Data["Surface"] = Surface;
            CScene.AddSceneObject(Surface);

            DrawnSceneObject Surface2 = CreateStaticSprite("Surface2", ResourceManager.Images["Surface"], new Vertex(1920, 900, 0), new Vertex(1920, 1000, 0), true);
            CScene.Data["Surface2"] = Surface2;
            CScene.AddSceneObject(Surface2);

            CreateRoom(CScene, 6, 4, 0, new int[] { 1, 0 }, new bool[] { false, true, false, false }, 2);
            CreateRoom(CScene, 11, 3, 0, new int[] { 0, 1 }, new bool[] { true, false, false }, 1);
            CreateRoom(CScene, 6, 3, 1, new int[] { 0, 2 }, null, 1);
            CreateRoom(CScene, 9, 5, 1, new int[] { 2, 0 }, null , 3);
            CreateRoom(CScene, 6, 2, 2, new int[] { 0, 0 }, new bool[]{ true,false });

            CreateRoom(CScene, 17, 3, 0, new int[] { 1, 0 }, new bool[] { false, false, true }, 1);
            CreateRoom(CScene, 20, 3, 0, new int[] { 0, 0 }, new bool[] { true, false, false });
            CreateRoom(CScene, 23, 2, 0, new int[] { 0, 1 }, new bool[] { true, false });
            CreateRoom(CScene, 17, 3, 1, new int[] { 0, 0 }, new bool[] { false, true, false }, 1);
            CreateRoom(CScene, 20, 3, 1, new int[] { 0, 1 }, new bool[] { false, true, false }, 1);
            CreateRoom(CScene, 16, 5, 2, new int[] { 2, 2 }, null, 3);
            CreateRoom(CScene, 21, 3, 2, new int[] { 2, 2 }, null, 2);

            CreateRoom(CScene, 27, 1, 2, new int[] { 2, 0 }, new bool[] { true });
            CreateRoom(CScene, 27, 1, 3, new int[] { 0, 0 }, new bool[] { true });
            CreateRoom(CScene, 21, 4, 4, new int[] { 0, 2 }, null, 2);
            CreateRoom(CScene, 27, 1, 4, new int[] { 2, 0 }, null);

            CreateTequila(CScene, 6, 2);
            CreateTequila(CScene, 22, 0);
            CreateTequila(CScene, 21, 4);

            /*CreateAsset(CScene, 0, 6, 1);
            CreateAsset(CScene, 1, 7, 1);
            CreateAsset(CScene, 2, 8, 1);
            CreateAsset(CScene, 3, 9, 1);
            CreateAsset(CScene, 4, 10, 1);*/

            HealthBar.Create(CScene);

            PowerUps PU = new PowerUps(CScene);
            CScene.Data["PowerUps"] = PU;

            CScene.AddSceneObject(CreateStaticSprite("Asset", ResourceManager.Images["banner"], new Vertex(9000, 500, 0), new Vertex(200, 400, 0), true, Collision2DType.Focus));
        }
        public static void CreateAsset(Scene2D CScene, int Index, int XLocation, int Level)
        {
            DrawnSceneObject Asset = null;
            if (Index == 0) Asset = CreateStaticSprite("Asset", ResourceManager.Images["asset"+Index], new Vertex(XLocation * 300 + 100, 600 - Level * 600 + 200 - 40, 0), new Vertex(100, 100, 0), false, Collision2DType.Focus);
            else if (Index == 1) Asset = CreateStaticSprite("Asset", ResourceManager.Images["asset" + Index], new Vertex(XLocation * 300 + 50, 600 - Level * 600 - 30 + 50, 0), new Vertex(200, 250, 0), true, Collision2DType.Focus);
            else if (Index == 2) Asset = CreateStaticSprite("Asset", ResourceManager.Images["asset" + Index], new Vertex(XLocation * 300, 600 - Level * 600 - 40 + 100, 0), new Vertex(200, 200, 0), true, Collision2DType.Focus);
            else if (Index == 3) Asset = CreateStaticSprite("Asset", ResourceManager.Images["asset" + Index], new Vertex(XLocation * 300 + 25, 600 - Level * 600 - 280, 0), new Vertex(250, 500, 0), false, Collision2DType.Focus);
            else if (Index == 4) Asset = CreateStaticSprite("Asset", ResourceManager.Images["asset" + Index], new Vertex(XLocation * 300, 600 - Level * 600 - 30 + 100, 0), new Vertex(300, 200, 0), true, Collision2DType.Focus);
            CScene.AddSceneObject(Asset);
        }
        public static void CreateEnemies(Scene2D CScene, DrawnSceneObject Player)
        {
            CreateEnemy(CScene, Player, 0, 10, 1, 5, 1);
            CreateEnemy(CScene, Player, 0, 18, 2, 3, 0);
            CreateEnemy(CScene, Player, 0, 21, 1, 3, 0);
            CreateEnemy(CScene, Player, 0, 20, 4, 5, 2);
        }
        public static void CreateEnemy(Scene2D CScene, DrawnSceneObject Player, int Type, int XLocation, int Index, int Length, int Level)
        {
            Movement.Cowboys.Add(new Cowboy(CScene, Player, XLocation*300 - 50, 570 - Level*600, 2, 600, 300*Index, 300*(Length-Index) - 200, 300));
        }
        public static void CreateTequila(Scene2D CScene, int XLocation, int Level)
        {
            DrawnSceneObject Coin = CreateStaticSprite("Coin", ResourceManager.Images["tequila"], new Vertex(XLocation * 300 + 70, (Level-1) * (-600) + 80, 0), new Vertex(160, 200, 0));
            Coin.Data["Coin"] = true;
            CScene.AddSceneObject(Coin);
        }
        public static void CreateRoof(Scene2D CScene, int XLocation, int Length, int Level, int[] Edges, bool[] Stairs, int Assets = 0)
        {

        }
        public static void CreateRoom(Scene2D CScene, int XLocation, int Length, int Level, int[] Enterances, bool[] Stairs, int Assets = 0)
        {
            for(int i = 0; i < Length; i++)
            {
                FloorTaken[Level, XLocation + i] = true;
            }
            int Location = XLocation * 300;
            if (Enterances == null) Enterances = new int[]{0, 0};
            if (Stairs == null) Stairs = new bool[Length];
            for (int i = 0; i < Length; i++)
            {
                if(Level != 0 && !FloorTaken[Level-1, XLocation + i]) CreateWallPart(CScene, new Vertex(Location + i * 300, Level * (-600) + 250, 0), -1, Stairs[i]);
                else CreateWallPart(CScene, new Vertex(Location + i * 300, Level * (-600) + 250, 0), Level, Stairs[i]);
            }
            if(Enterances[0] == 1)
            {
                if (Level == 0)
                {
                    DrawnSceneObject Floor = CreateStaticSprite("Floor", ResourceManager.Images["Ceiling"], new Vertex(Location - 250, 850, 0), new Vertex(250, 50, 0), true, Collision2DType.Focus);
                    CScene.AddSceneObject(Floor);
                }
                DrawnSceneObject LeftDoor = CreateStaticSprite("LeftDoor", ResourceManager.Images["Door"+RandomNumber(0,3)+"L"], new Vertex(Location - 250, Level * (-600) + 300 + 100, 0), new Vertex(250, 450, 0));
                CScene.AddSceneObject(LeftDoor);
                DrawnSceneObject LeftWall = CreateStaticSprite("LeftWall", ResourceManager.Images["Wall" + RandomNumber(0, 2)], new Vertex(Location, Level * (-600) + 300, 0), new Vertex(30, 550, 0));
                CScene.AddSceneObject(LeftWall);
            }
            else if (Enterances[0] == 2)
            {
                DrawnSceneObject LeftWall = CreateStaticSprite("LeftWall", ResourceManager.Images["LightWall" + RandomNumber(0, 2)], new Vertex(Location, Level * (-600) + 300, 0), new Vertex(30, 550, 0));
                CScene.AddSceneObject(LeftWall);
            }
            else
            {
                DrawnSceneObject LeftWall = CreateStaticSprite("LeftWall", ResourceManager.Images["Wall" + RandomNumber(0, 2)], new Vertex(Location, Level * (-600) + 300, 0), new Vertex(30, 550, 0), Enterances[0] == 0, Collision2DType.Rectangular);
                LeftWall.Data["XCollision"] = true;
                CScene.AddSceneObject(LeftWall);
            }
            if (Enterances[1] == 1)
            {
                if (Level == 0)
                {
                    DrawnSceneObject Floor = CreateStaticSprite("Floor", ResourceManager.Images["Ceiling"], new Vertex(Location + Length * 300, 850, 0), new Vertex(250, 50, 0), true, Collision2DType.Focus);
                    CScene.AddSceneObject(Floor);
                }
                DrawnSceneObject RightWall = CreateStaticSprite("RightWall", ResourceManager.Images["Wall" + RandomNumber(0, 2)], new Vertex(Location + Length * 300 - 30, Level * (-600) + 300, 0), new Vertex(30, 550, 0));
                CScene.AddSceneObject(RightWall);
                DrawnSceneObject RightDoor = CreateStaticSprite("RightDoor", ResourceManager.Images["Door" + RandomNumber(0, 3) + "R"], new Vertex(Location + Length * 300, Level * (-600) + 300 + 100, 0), new Vertex(250, 450, 0));
                CScene.AddSceneObject(RightDoor);
            }
            else if (Enterances[1] == 2)
            {
                DrawnSceneObject RightWall = CreateStaticSprite("RightWall", ResourceManager.Images["LightWall" + RandomNumber(0, 2)], new Vertex(Location + Length * 300 - 30, Level * (-600) + 300, 0), new Vertex(30, 550, 0));
                CScene.AddSceneObject(RightWall);
            }
            else
            {
                DrawnSceneObject RightWall = CreateStaticSprite("RightWall", ResourceManager.Images["Wall" + RandomNumber(0, 2)], new Vertex(Location + Length * 300 - 30, Level * (-600) + 300, 0), new Vertex(30, 550, 0), Enterances[1] == 0, Collision2DType.Rectangular);
                RightWall.Data["XCollision"] = true;
                CScene.AddSceneObject(RightWall);
            }
            List<int> FreeIndexes = new List<int>();
            for (int i = 0; i < Stairs.Length; i++)
            {
                if (!Stairs[i]) FreeIndexes.Add(i);
            }
            for (int i = 0; i < Assets && FreeIndexes.Count > 0; i++)
            {
                int Num = RandomNumber(0, FreeIndexes.Count);
                CreateAsset(CScene, RandomNumber(0, 5), XLocation + FreeIndexes[Num], Level);
                FreeIndexes.RemoveAt(Num);
            }
        }
        public static void CreateWallPart(Scene CScene, Vertex Location, int Level, bool Stairs)
        {
            DrawnSceneObject Ceiling = CreateStaticSprite("Ceiling", ResourceManager.Images["Ceiling"], Location, new Vertex(300, 50, 0), true);
            CScene.AddSceneObject(Ceiling);
            if (Level == 0)
            {
                DrawnSceneObject Floor = CreateStaticSprite("Floor", ResourceManager.Images["Floor"], new Vertex(Location.X, Location.Y + 600, 0), new Vertex(300, 50, 0), true, Collision2DType.Rectangular);
                CScene.AddSceneObject(Floor);
            }
            else if (Level == -1)
            {
                DrawnSceneObject Floor = CreateStaticSprite("Floor", ResourceManager.Images["Ceiling"], new Vertex(Location.X, Location.Y + 600, 0), new Vertex(300, 50, 0), true, Collision2DType.Rectangular);
                CScene.AddSceneObject(Floor);
            }
            DrawnSceneObject BackWall = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall" + RandomNumber(0, 3)], new Vertex(Location.X, Location.Y + 50, 0), new Vertex(300, 550, 0));
            CScene.AddSceneObject(BackWall);

            if(Stairs)
            {
                DrawnSceneObject DStairs = CreateStaticSprite("Stairs", ResourceManager.Images["Stairs"], new Vertex(Location.X + 150 - 60, Location.Y, 0), new Vertex(120, 600, 0));
                DStairs.Data["Stairs"] = DStairs;
                CScene.AddSceneObject(DStairs);
            }
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
