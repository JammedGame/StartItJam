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
        private int GravityAmount = 0;
        private Vertex Trans;
        private DrawnSceneObject _Player;
        private Scene2D CScene;
     
        public Movement(DrawnSceneObject Player, Scene2D CScene)
        {
            this._Player = Player;
            this.Trans = Player.Visual.Translation;
            this.CScene = CScene;
            this.CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdate);         
        }

        public void KeyPressEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.W) _WDown = true;
            if (E.KeyDown == KeyType.A)
            {
                ((Sprite)_Player.Visual).UpdateSpriteSet("Walk");
                _ADown = true;
            }
            if (E.KeyDown == KeyType.S) _SDown = true;
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

        }
        public void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.Escape)
            {
                
            }
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
            Trans = new Vertex();
            if ((int)_Player.Data["skokBrojac"] > 0)
            {
                Trans = new Vertex(Trans.X, Trans.Y - (int)_Player.Data["skokBrojac"], 0);
                _Player.Data["skokBrojac"] = (int)_Player.Data["skokBrojac"] - 1;
            }
            Gravity();
            WalkLeftRight();
            CheckCollision();
            for(int i = 0; i < CScene.Objects.Count; i++)
            {
                if (CScene.Objects[i].ID == _Player.ID) continue;
                if (CScene.Objects[i].Name == "Back") continue;
                //CScene.Objects[i].Visual.Translation = new Vertex(CScene.Objects[i].Visual.Translation.X - Trans.X, CScene.Objects[i].Visual.Translation.Y - Trans.Y, 0);
                
            }
            _Player.Visual.Translation = new Vertex(_Player.Visual.Translation.X + Trans.X, _Player.Visual.Translation.Y + Trans.Y, 0);
            CScene.Transformation.Translation = new Vertex(CScene.Transformation.Translation.X - Trans.X*1.0f/2, CScene.Transformation.Translation.Y - Trans.Y * 1.0f / 2, 0);
            //Tile Back = (Tile)((DrawnSceneObject)CScene.Data["Back"]).Visual;
            //Back.Translation = new Vertex(Back.Translation.X + Trans.X, Back.Translation.Y + Trans.Y, 0);
            Tile Surface = (Tile)((DrawnSceneObject)CScene.Data["Surface"]).Visual;
            Surface.Translation = new Vertex(Surface.Translation.X + Trans.X, Surface.Translation.Y, 0);
        }

        public void WalkLeftRight()
        {
            if (_ADown)
            {
                Trans = new Vertex(Trans.X - 10, Trans.Y, 0);
            }
            if (_DDown)
            {
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
            DrawnSceneObject DSO = (DrawnSceneObject)CollisionChecker.CheckSceneCollision(CScene, (DrawnSceneObject)_Player);
            if (DSO != null)
            {
                CollisionX = true;
                CollisionY = true;
                GravityOn = false;
                _Player.Data["flying"] = false;
                //Trans = new Vertex(Trans.X, DSO.Visual.Translation.Y - _Player.Visual.Scale.Y, 0);
            }
            else
            {
                if(!GravityOn) GravityAmount = 0;
                GravityOn = true;
            }
        }
    }
}
