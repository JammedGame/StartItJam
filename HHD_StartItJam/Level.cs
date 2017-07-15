﻿using Engineer.Engine;
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
        public static bool[,] FloorTaken = new bool[5, 100];
        public static void Create(Scene2D CScene)
        {
            //DrawnSceneObject Back = CreateStaticSprite("Back", ResourceManager.Images["Back"], new Vertex(0, 0, 0), new Vertex(1920, 900, 0));
            //CScene.AddSceneObject(Back);
            DrawnSceneObject Surface = CreateStaticSprite("Surface", ResourceManager.Images["Surface"], new Vertex(0, 900, 0), new Vertex(1920, 1000, 0), true);
            CScene.AddSceneObject(Surface);
            CreateRoom(CScene, 1, 4, 0, new int[] { 1, 0 }, new bool[] { false, true, false, false});
            CreateRoom(CScene, 6, 3, 0, new int[] { 0, 1 }, new bool[] {true, false, false});
            CreateRoom(CScene, 1, 3, 1, new int[] { 0, 2 }, null);
            CreateRoom(CScene, 4, 5, 1, new int[] { 2, 0 }, null);

            CreateRoom(CScene, 12, 3, 0, new int[] { 1, 0 }, new bool[] { false, false, true});
            CreateRoom(CScene, 15, 3, 0, new int[] { 0, 0 }, new bool[] { true, false, false });
            CreateRoom(CScene, 18, 2, 0, new int[] { 0, 1 }, null);
            CreateRoom(CScene, 12, 3, 1, new int[] { 0, 0 }, new bool[] { false, true, false });
            CreateRoom(CScene, 15, 3, 1, new int[] { 0, 1 }, new bool[] { false, true, false });
            CreateRoom(CScene, 11, 5, 2, new int[] { 0, 2 }, null);
            CreateRoom(CScene, 16, 3, 2, new int[] { 2, 0 }, null);

            DrawnSceneObject Coin = CreateStaticSprite("Coin", ResourceManager.Images["Surface"], new Vertex(450, 720, 0), new Vertex(50, 50, 0));
            Coin.Data["Coin"] = true;
            CScene.AddSceneObject(Coin);

            DrawnSceneObject Coin2 = CreateStaticSprite("Coin", ResourceManager.Images["Surface"], new Vertex(510, 720, 0), new Vertex(50, 50, 0));
            Coin2.Data["Coin"] = true;
            CScene.AddSceneObject(Coin2);

            DrawnSceneObject Coin3 = CreateStaticSprite("Coin", ResourceManager.Images["Surface"], new Vertex(570, 720, 0), new Vertex(50, 50, 0));
            Coin3.Data["Coin"] = true;
            CScene.AddSceneObject(Coin3);

            HealthBar HB = HealthBar.Create();
            CScene.Data["HealthBar"] = HB;
            CScene.AddSceneObject(HB);
        }
        public static void CreateRoom(Scene2D CScene, int XLocation, int Length, int Level, int[] Enterances, bool[] Stairs)
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
                DrawnSceneObject LeftDoor = CreateStaticSprite("LeftDoor", ResourceManager.Images["Door0L"], new Vertex(Location - 250, Level * (-600) + 300 + 100, 0), new Vertex(250, 450, 0));
                CScene.AddSceneObject(LeftDoor);
            }
            DrawnSceneObject LeftWall = CreateStaticSprite("LeftWall", ResourceManager.Images["Wall"], new Vertex(Location, Level * (-600) + 300, 0), new Vertex(30, 550, 0), Enterances[0] == 0, Collision2DType.Rectangular);
            LeftWall.Data["XCollision"] = true;
            CScene.AddSceneObject(LeftWall);
            if (Enterances[1] == 1)
            {
                DrawnSceneObject RightDoor = CreateStaticSprite("RightDoor", ResourceManager.Images["Door0R"], new Vertex(Location + Length * 300, Level * (-600) + 300 + 100, 0), new Vertex(250, 450, 0));
                CScene.AddSceneObject(RightDoor);
            }
            DrawnSceneObject RightWall = CreateStaticSprite("RightWall", ResourceManager.Images["Wall"], new Vertex(Location + Length * 300 - 30, Level * (-600) + 300, 0), new Vertex(30, 550, 0), Enterances[1] == 0, Collision2DType.Rectangular);
            RightWall.Data["XCollision"] = true;
            CScene.AddSceneObject(RightWall);
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
            DrawnSceneObject BackWall = CreateStaticSprite("BackWall", ResourceManager.Images["BackWall"], new Vertex(Location.X, Location.Y + 50, 0), new Vertex(300, 550, 0));
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
