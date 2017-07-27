using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    class Movement
    {
        public static Movement Move;
        public static int AImoveLeft = 0;
        public static int AImovRight = -1;
        private bool _BlockEvents = false;
        public bool _ADown;
        public bool _DDown;
        public bool _WDown;
        public bool _SDown;
        public bool _SpaceDown;
        private bool _Attacking = false;
        private bool _OnStairs = false;
        private bool CollisionY = false;
        private bool CollisionXLeft = false;
        private bool CollisionXRight = false;
        private bool GravityOn = true;
        private int GravityAmount = 0;
        public bool Grab;
        public Vertex Trans;
        public Vertex GlobalTrans = new Vertex();
        private DrawnSceneObject _Player;
        private Scene2D CScene;
        public static List<Cowboy> Cowboys = new List<Cowboy>();

        public Movement(DrawnSceneObject Player, Scene2D CScene)
        {
            Movement.Move = this;
            this._Player = Player;
            _Player.Data["Orient"] = 1;
            Trans = Player.Visual.Translation;
            this.CScene = CScene;
            this.CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdate);
        }
        public void KeyPressEvent(Game G, EventArguments E)
        {
            if (_BlockEvents) return;
            //if (GameLogic.GameOver) return;                     
        }
        public void KeyUpEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.W)
            {
                _WDown = false;
            }
            if (E.KeyDown == KeyType.A)
            {
                if (!_OnStairs && !_DDown)
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("WalkL");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheL");
                    _Player.Data["Orient"] = 1;
                    Sound.Instance().Stop("walk");
                }
                else
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("WalkR");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheR");
                    _Player.Data["Orient"] = 0;
                }
                _ADown = false;
            }
            if (E.KeyDown == KeyType.S)
            {
                _SDown = false;
            }
            if (E.KeyDown == KeyType.D)
            {
                if (!_OnStairs && !_ADown)
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("WalkR");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheR");
                    _Player.Data["Orient"] = 0;
                    Sound.Instance().Stop("walk");                    
                }
                else
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("WalkL");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheL");
                    _Player.Data["Orient"] = 1;
                }
                _DDown = false;
            }
            if (E.KeyDown==KeyType.K)
            {
                _Attacking = false;
            }
            if (E.KeyDown == KeyType.Space)
            {
                _SpaceDown = false;
            }
        }
        public void KeyDownEvent(Game G, EventArguments E)
        {
            if (_BlockEvents) return;
            if (E.KeyDown == KeyType.Escape)
            {
            }
            if (E.KeyDown == KeyType.W)
            {
                _WDown = true;
            }
            if (E.KeyDown == KeyType.A)
            {                
                if (!_OnStairs)
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("WalkL");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheL");
                    Sound.Instance().PlayIncremental("walk");
                    _Player.Data["Orient"] = 1;
                }
                _ADown = true;               
            }
            if (E.KeyDown == KeyType.S)
            {
                _SDown = true;
            }
            if (E.KeyDown == KeyType.D)
            {                
                if (!_OnStairs)
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("WalkR");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheR");
                    Sound.Instance().PlayIncremental("walk");
                    _Player.Data["Orient"] = 0;
                }
                _DDown = true;               
            }
            if (E.KeyDown == KeyType.K)
            {
                Sound.Instance().Play("swing");
                _Attacking = true;
                if ((int)_Player.Data["Orient"] == 0)                {
                    ((Sprite)_Player.Visual).SetBackUpSpriteSet("WalkR");
                    ((Sprite)_Player.Visual).UpdateSpriteSet("DattR");
                    ((Sprite)_Player.Data["ScytheSprite"]).SetBackUpSpriteSet("ScytheR");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("AttR");
                }
                else
                {
                    ((Sprite)_Player.Visual).SetBackUpSpriteSet("WalkL");
                    ((Sprite)_Player.Visual).UpdateSpriteSet("DattL");
                    ((Sprite)_Player.Data["ScytheSprite"]).SetBackUpSpriteSet("ScytheL");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("AttL");
                }
                _Attacking = false;
                for (int i = 0; i < Cowboys.Count; i++)
                {
                    if (Cowboys[i].PlayerHit() != -1)
                    {
                        //CScene.Objects.Remove(Cowboys[i].Enemy);
                        if (!Cowboys[i].Enemy.Data.ContainsKey("Dead"))
                        {
                            Sound.Instance().Play("kill");
                        }
                        Cowboys[i].Enemy.Visual.Active = false;
                        Cowboys[i].Enemy.Data["Dead"] = true;
                        ((DrawnSceneObject)Cowboys[i].Enemy.Data["Whip"]).Active = false;
                    }
                }
            }
            if (E.KeyDown == KeyType.Space)
            {
                if ((int)_Player.Data["Orient"] == 1)
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("JumpL");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheL");
                }
                else
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("JumpR");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheR");
                }
                _SpaceDown = true;
            }
        }
        public void GameUpdate(Game G, EventArguments E)
        {
            if (_BlockEvents) return;
            this._BlockEvents = true;
            Runner.BlockDraw = true;
            Trans = new Vertex();
            if ((int)_Player.Data["skokBrojac"] > 0)
            {
                Trans = new Vertex(Trans.X, Trans.Y - (int)_Player.Data["skokBrojac"], 0);
                _Player.Data["skokBrojac"] = (int)_Player.Data["skokBrojac"] - 1;
                if (_Player.InCollisionWithAny(CScene.getHavingData("Stairs"), Collision2DType.Vertical))
                {
                    // Ako pipne merdevine
                    _Player.Data["skokBrojac"] = 0;
                    GravityAmount = 0;
                    GravityOn = false;
                }
            }
            WalkLeftRight();
            if (_SpaceDown && (bool)(_Player.Data["flying"]) == false)
            {
                Grab = false;
                GravityOn = true;
                _Player.Data["flying"] = true;
                _Player.Data["skokBrojac"] = 30;

                if ((int)_Player.Data["Orient"] == 1)
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("JumpL");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheL");
                }
                else
                {
                    ((Sprite)_Player.Visual).UpdateSpriteSet("JumpR");
                    ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheR");
                }
                //AudioPlayer.PlaySound(AudioPlayer.Kre, false, 100);
            }
            List<DrawnSceneObject> Collisions = _Player.GetCollisionWithAny(CScene.getHavingData("Stairs"), Collision2DType.Vertical);
            if (Collisions.Count > 0)
            {
                if (_WDown)
                {
                    _OnStairs = true;
                    Trans = new Vertex(Trans.X, Trans.Y - 10, 0);
                    Grab = true;
                    _Player.Data["flying"] = false;
                    ((Sprite)_Player.Visual).UpdateSpriteSet("Up");
                }
                if (Math.Abs(Collisions[0].Visual.Translation.Y - _Player.Visual.Translation.Y) < 350)
                {
                    if (_SDown)
                    {
                        _OnStairs = true;
                        Trans = new Vertex(Trans.X, Trans.Y + 10, 0);
                        Grab = true;
                        _Player.Data["flying"] = false;
                        ((Sprite)_Player.Visual).UpdateSpriteSet("Up");
                    }
                }
                else _OnStairs = false;
            }
            else _OnStairs = false;
            Gravity();
            CheckCollision();
            for (int i = 0; i < CScene.Objects.Count; i++)
            {
                if (CScene.Objects[i].Name == "Floor")
                {
                    if (((Sprite)_Player.Visual).InCollision(CScene.Objects[i].Visual, Collision2DType.Rectangular))
                    {
                        Trans = new Vertex(Trans.X, Trans.Y - ((_Player.Visual.Translation.Y + _Player.Visual.Scale.Y) - CScene.Objects[i].Visual.Translation.Y), 0);
                    }
                }
            }
            for (int i = 0; i < CScene.Objects.Count; i++)
            {
                if (CScene.Objects[i].ID == _Player.ID) continue;
                if (CScene.Objects[i].Name == "Back") continue;
                if (CScene.Objects[i].Data.ContainsKey("Static")) continue;
                if (CScene.Objects[i].Name == "Surface")
                {                    // Looping two surfaces to symulate infinite surface
                    DrawnSceneObject Surface2 = (DrawnSceneObject)CScene.Data["Surface2"];
                    DrawnSceneObject Surface = (DrawnSceneObject)CScene.Objects[i];
                    if (Surface.Visual.RightEdge >= 0 && Surface.Visual.RightEdge < 1920)
                    {
                        Surface2.Visual.LeftEdge = Surface.Visual.RightEdge;
                    }
                    if (Surface.Visual.LeftEdge >= 0 && Surface.Visual.LeftEdge < 1920)
                    {
                        Surface2.Visual.RightEdge = Surface.Visual.LeftEdge;
                    }
                    if (Surface.Visual.RightEdge < 0)
                    {
                        Surface.Visual.LeftEdge = Surface2.Visual.RightEdge;
                    }
                    if (Surface.Visual.LeftEdge > 1920)
                    {
                        Surface.Visual.RightEdge = Surface2.Visual.LeftEdge;
                    }
                }
                CScene.Objects[i].Visual.Translation = new Vertex(CScene.Objects[i].Visual.Translation.X - Trans.X, CScene.Objects[i].Visual.Translation.Y - Trans.Y, 0);
            }
            GlobalTrans = new Vertex(GlobalTrans.X + Trans.X, GlobalTrans.Y + Trans.Y, 0);
            for (int i = 0; i < Cowboys.Count; i++) Cowboys[i].Behavior();
            List<DrawnSceneObject> coins = _Player.GetCollisionWithAny(CScene.getHavingData("Coin"), Collision2DType.Radius);
            foreach (DrawnSceneObject coin in coins)
            {
                if (!coin.Active) continue;
                coin.Active = false;
                PowerUps PU = ((PowerUps)CScene.Data["PowerUps"]);
                PU.increasePotionCount();
            }
            Character.UpdateScythe(_Player);
            Runner.BlockDraw = false;
            this._BlockEvents = false;
            if (Math.Abs(_Player.Visual.Translation.X - ((DrawnSceneObject)CScene.Data["Banner"]).Visual.Translation.X) < 300)
            {
                if (PowerUps.Tequillas == 3)
                {
                    GameLogic.Create().RunMenu();
                }
            }
        }

        public void WalkLeftRight()
        {
            if (_ADown)
            {
                if (CollisionXLeft) return;
                Trans = new Vertex(Trans.X - 10, Trans.Y, 0);
            }
            if (_DDown)
            {
                if (CollisionXRight) return;
                Trans = new Vertex(Trans.X + 10, Trans.Y, 0);
            }
        }
        public void Gravity()
        {
            if (GravityOn)            {
                Trans = new Vertex(Trans.X, Trans.Y + (float)Math.Sqrt(GravityAmount), 0);
                GravityAmount++;
            }
        }
        public void CheckCollision()
        {
            List<DrawnSceneObject> DSOS = (List<DrawnSceneObject>)CollisionChecker.CheckSceneCollision(CScene, (DrawnSceneObject)_Player);
            if (DSOS.Count > 0)
            {
                if (_Attacking==true) {
                    if ((int)_Player.Data["Orient"] == 0)
                    {
                        ((Sprite)(_Player.Visual)).UpdateSpriteSet("WalkR");
                        ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheR");
                    }
                    else
                    {
                        ((Sprite)(_Player.Visual)).UpdateSpriteSet("WalkL");
                        ((Sprite)_Player.Data["ScytheSprite"]).UpdateSpriteSet("ScytheL");
                    }

                }

                CollisionXLeft = false;
                CollisionXRight = false;
                bool XFound = false;
                bool XOnly = true;
                for (int i = 0; i < DSOS.Count; i++)
                {
                    if (DSOS[i].Data.ContainsKey("XCollision"))
                    {
                        XFound = true;
                        if (DSOS[i].Visual.Translation.X + DSOS[i].Visual.Scale.X / 2 < _Player.Visual.Translation.X + _Player.Visual.Scale.X / 2)
                        {
                            CollisionXLeft = true;
                            CollisionXRight = false;
                        }
                        else
                        {
                            CollisionXLeft = false;
                            CollisionXRight = true;
                        }
                        CollisionY = false;
                    }
                    else XOnly = false;
                }
                if (!XFound || !XOnly)
                {
                    CollisionY = true;
                    GravityOn = false;
                    _Player.Data["flying"] = false;
                }
            }
            else if (_Player.InCollisionWithAny(CScene.getHavingData("Stairs"), Collision2DType.Vertical) && Grab)
            {
                GravityOn = false;
            }
            else
            {
                if (!GravityOn) GravityAmount = 0;
                GravityOn = true;
                CollisionXLeft = false;
                CollisionXRight = false;
                CollisionY = false;
            }
        }
    }
}