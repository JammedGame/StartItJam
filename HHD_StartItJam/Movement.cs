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
        public static int AImoveLeft = 0;
        public static int AImovRight = -1;
        private bool _BlockEvents = false;
        public bool _ADown;
        public bool _DDown;
        public bool _WDown;
        public bool _SDown;
        public bool _SpaceDown;
        private bool CollisionY = false;
        private bool CollisionXLeft=false;
        private bool CollisionXRight=false;
        private bool GravityOn = true;
        private int GravityAmount = 0;
        public bool Grab;
        private Vertex Trans;
        private DrawnSceneObject _Player;
        private Scene2D CScene;
        private List<Cowboy> Cowboys=new List<Cowboy>();        
     
        public Movement(DrawnSceneObject Player, Scene2D CScene)
        {
            this._Player = Player;
            this.Trans = Player.Visual.Translation;
            this.CScene = CScene;
            this.CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdate);
            
            Cowboy Cowboy0 = new Cowboy(this.CScene, this._Player,600,600);
            this.Cowboys.Add(Cowboy0);

            Cowboy Cowboy1 = new Cowboy(this.CScene, this._Player, 2000, 600);
            this.Cowboys.Add(Cowboy1);

            Cowboy Cowboy2 = new Cowboy(this.CScene, this._Player, 600, 0);
            this.Cowboys.Add(Cowboy2);

            Cowboy Cowboy3 = new Cowboy(this.CScene, this._Player, 2000, 0);
            this.Cowboys.Add(Cowboy3);

            Cowboy Cowboy4 = new Cowboy(this.CScene, this._Player, 3000, 600);
            this.Cowboys.Add(Cowboy4);


        }

        public void KeyPressEvent(Game G, EventArguments E)
        {
            if (_BlockEvents) return;
            //if (GameLogic.GameOver) return;
            if (E.KeyDown == KeyType.W) _WDown = true;
            if (E.KeyDown == KeyType.A)
            {
                ((Sprite)_Player.Visual).SetSpriteSet("Walk");
                _ADown = true;
            }
            if (E.KeyDown == KeyType.S)
            {
                _SDown = true;
            }
            if (E.KeyDown == KeyType.D)
            {
                ((Sprite)_Player.Visual).UpdateSpriteSet("Walk");
                _DDown = true;
            }

            if (E.KeyDown == KeyType.K)
            {
                ((Sprite)_Player.Visual).SetBackUpSpriteSet(0);
                ((Sprite)_Player.Visual).UpdateSpriteSet("Attack");
            }
        }
        public void KeyUpEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.W)
            {
                _WDown = false;
            }
            if (E.KeyDown == KeyType.A)
            {
                ((Sprite)_Player.Visual).UpdateSpriteSet("Idle");
                _ADown = false;
            }
            if (E.KeyDown == KeyType.S)
            {
                _SDown = false;
            }
            if (E.KeyDown == KeyType.D)
            {
                ((Sprite)_Player.Visual).UpdateSpriteSet("Idle");
                _DDown = false;
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
            if (E.KeyDown == KeyType.Space)
            {
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
                ((Sprite)(_Player.Visual)).UpdateSpriteSet(1);
                //AudioPlayer.PlaySound(AudioPlayer.Kre, false, 100);
            }

            List<DrawnSceneObject> Collisions = _Player.GetCollisionWithAny(CScene.getHavingData("Stairs"), Collision2DType.Vertical);
            if (Collisions.Count > 0)
            {
                if (_WDown)
                {
                    Trans = new Vertex(Trans.X, Trans.Y - 10, 0);
                    Grab = true;
                    _Player.Data["flying"] = false;
                }
                if (Math.Abs(Collisions[0].Visual.Translation.Y - _Player.Visual.Translation.Y) < 350)
                {
                    if (_SDown)
                    {
                        Trans = new Vertex(Trans.X, Trans.Y + 10, 0);
                        Grab = true;
                        _Player.Data["flying"] = false;
                    }
                }
            }

                       
              
                Gravity();
            CheckCollision();
            for(int i = 0; i < CScene.Objects.Count; i++)
            {
                if (CScene.Objects[i].Name == "Cowboy")
                {
                    int d = 2;
                }
                if (CScene.Objects[i].ID == _Player.ID) continue;
                if (CScene.Objects[i].Name == "Back") continue;
                if (CScene.Objects[i].Data.ContainsKey("Static")) continue;

                if (CScene.Objects[i].Name == "Surface") CScene.Objects[i].Visual.Translation = new Vertex(CScene.Objects[i].Visual.Translation.X + Trans.X, CScene.Objects[i].Visual.Translation.Y, 0);
                CScene.Objects[i].Visual.Translation = new Vertex(CScene.Objects[i].Visual.Translation.X - Trans.X, CScene.Objects[i].Visual.Translation.Y - Trans.Y, 0);
            }
            Runner.BlockDraw = false;
            this._BlockEvents = false;
            for (int i = 0; i < Cowboys.Count; i++)
            {
                Cowboys[i].Behavior();
                if (Cowboys[i].Hit())
                {
                    HealthBar HB = (HealthBar)CScene.Data["HealthBar"];
                    HB.subHealth(1);
                    if (HB.empty())
                    {
                        // End of game
                    }
                }
            }
        }

        /*List<DrawnSceneObject> coins = _Player.GetCollisionWithAny(CScene.getHavingData("Coin"), Collision2DType.Radius);


                foreach(DrawnSceneObject coin in coins){
                    if (!coin.Active) continue;
                    coin.Active = false;
                    HealthBar HB = (HealthBar)CScene.Data["HealthBar"];
                    HB.subHealth(20);
                    if (HB.empty())
                    {
                        // End of game
                    }
                }*/

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
            if (GravityOn)
            {
                Trans = new Vertex(Trans.X, Trans.Y + (float)Math.Sqrt(GravityAmount), 0);
                GravityAmount++;
            }
        }

        public void CheckCollision()
        {
            List<DrawnSceneObject> DSOS = (List<DrawnSceneObject>)CollisionChecker.CheckSceneCollision(CScene, (DrawnSceneObject)_Player);
            if (DSOS.Count > 0)
            {
                CollisionXLeft = false;
                CollisionXRight = false;

                bool XFound = false;
                bool XOnly = true;
                for (int i = 0; i < DSOS.Count; i++)
                {
                    if (DSOS[i].Data.ContainsKey("XCollision"))
                    {
                        XFound = true;
                        if (DSOS[i].Visual.Translation.X + DSOS[i].Visual.Scale.X/2 < _Player.Visual.Translation.X + _Player.Visual.Scale.X/2)
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
            else if(_Player.InCollisionWithAny(CScene.getHavingData("Stairs"), Collision2DType.Vertical) && Grab)
            {
                GravityOn = false;
            }
            else
            {
                if(!GravityOn) GravityAmount = 0;
                GravityOn = true;
                CollisionXLeft = false;
                CollisionXRight = false;
                CollisionY = false;
            }
        }
    }
}