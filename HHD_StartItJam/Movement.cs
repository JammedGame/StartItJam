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
        public bool _ADown;
        public bool _DDown;
        public bool _WDown;
        public bool _SDown;
        private bool CollisionX=false;
        private bool CollisionY=false;
        private bool GravityOn = true;

        private DrawnSceneObject _Player;
        private Scene2D CScene;

        //private List<DrawnSceneObject> _Colliders = new List<DrawnSceneObject>();        
        public Movement(DrawnSceneObject Player, Scene2D CScene)
        {
            this._Player = Player;
            this.CScene = CScene;
            this.CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdate);
            //this._Colliders = Colliders;           
        }

        public void KeyPressEvent(Game G, EventArguments E)
        {
            //if (GameLogic.GameOver) return;
            if (E.KeyDown == KeyType.W)
            {
                _WDown = true;
            }
            if (E.KeyDown == KeyType.A)
            {
                ((Sprite)_Player.Visual).UpdateSpriteSet("Walk");
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
                ((Sprite)_Player.Visual).BackUpSpriteSet = 0;
                ((Sprite)_Player.Visual).UpdateSpriteSet("Attack");
            }
        }
        public void KeyUpEvent(Game G, EventArguments E)
        {
            /*if (GameLogic.GameOver)
            {
                return;
            }*/
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

        }
        public void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.Escape)
            {
                
            }
            //if (GameLogic.GameOver) return;
            if (E.KeyDown == KeyType.Space)
            {
                if ((bool)(_Player.Data["flying"]) == false)
                {
                    _Player.Data["flying"] = true;
                    _Player.Data["skokBrojac"] = 40;
                    ((Sprite)(_Player.Visual)).UpdateSpriteSet(1);
                    //AudioPlayer.PlaySound(AudioPlayer.Kre, false, 100);
                }
            }
        }

        public void GameUpdate(Game G, EventArguments E)
        {
            if ((int)_Player.Data["skokBrojac"] > 0)
            {
                this._Player.Visual.Translation = new Vertex(this._Player.Visual.Translation.X, this._Player.Visual.Translation.Y - (int)_Player.Data["skokBrojac"], 0);
                _Player.Data["skokBrojac"] = (int)_Player.Data["skokBrojac"] - 1;
            }
            WalkLeftRight();

            if (_WDown)
            {
                if (((Sprite)_Player.Visual).InCollision(_Player.Visual, (Collision2DType)CScene.Data["Stairs"]))
                {

                    _Player.Visual.Translation = new Vertex(_Player.Visual.Translation.X, _Player.Visual.Translation.Y - 10, 0);

                }
                /*
                    for (int i = 0; i < CScene.Objects.Count; i++)
                    {
                        if (CScene.Objects[i].ID == _Player.ID) continue;
                        if (CScene.Objects[i].Data.ContainsKey("Collision"))
                        {

                            if (((Sprite)_Player.Visual).InCollision(CScene.Objects[i].Visual, (Collision2DType)CScene.Objects[i].Data["Collision"])))
                            {

                            }
                        }
                    }
                    */
            }
            else
            {
                Gravity();
            }

            CheckCollision();
        }

        public void WalkLeftRight()
        {
            if (_ADown)
            {
                _Player.Visual.Translation = new Vertex(_Player.Visual.Translation.X - (10 * GameLogic._GlobalScale), _Player.Visual.Translation.Y, 0);
            }
            if (_DDown)
            {
                _Player.Visual.Translation = new Vertex(_Player.Visual.Translation.X + (10 * GameLogic._GlobalScale), _Player.Visual.Translation.Y, 0);
            }

        }

        public void Gravity()
        {
            if (GravityOn)
            {
                _Player.Visual.Translation = new Vertex(_Player.Visual.Translation.X, _Player.Visual.Translation.Y + 10, 0);
            }
        }

        public void CheckCollision()
        {
            DrawnSceneObject DSO = (DrawnSceneObject)CollisionChecker.CheckSceneCollision(CScene, (DrawnSceneObject)_Player);
            if (DSO != null)
            {
                CollisionX = true;
                CollisionY = true;
                GravityOn = false;
                _Player.Data["flying"] = false;
            }
            else GravityOn = true;
        }
    }
}
