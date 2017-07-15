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
        private Runner _Runner;
        private DrawnSceneObject _Player;
        //private List<DrawnSceneObject> _Colliders = new List<DrawnSceneObject>();        
        public Movement(Runner NewRunner, DrawnSceneObject Player)
        {
            this._Runner = NewRunner;
            this._Player = Player;
            //this._Colliders = Colliders;           
        }

        public void KeyPressEvent(Game G, EventArguments E)
        {
            //if (GameLogic.GameOver) return;
            if (E.KeyDown == KeyType.W) _WDown = true;
            if (E.KeyDown == KeyType.A) _ADown = true;
            if (E.KeyDown == KeyType.S) _SDown = true;
            if (E.KeyDown == KeyType.D) _DDown = true;
        }
        public void KeyUpEvent(Game G, EventArguments E)
        {
            /*if (GameLogic.GameOver)
            {
                return;
            }*/
            if (E.KeyDown == KeyType.W) _WDown = false;
            if (E.KeyDown == KeyType.A) _ADown = false;
            if (E.KeyDown == KeyType.S) _SDown = false;
            if (E.KeyDown == KeyType.D) _DDown = false;
        }
        public void KeyDownEvent(Game G, EventArguments E)
        {
            if (E.KeyDown == KeyType.Escape)
            {
                _Runner.Close();
            }
            //if (GameLogic.GameOver) return;
            if (E.KeyDown == KeyType.Space)
            {
                if ((bool)(_Player.Data["flying"]) == false)
                {
                    _Player.Data["flying"] = true;
                    _Player.Data["skokBrojac"] = 20;
                    ((Sprite)(_Player.Visual)).UpdateSpriteSet(1);
                    //AudioPlayer.PlaySound(AudioPlayer.Kre, false, 100);
                }
            }
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
    }
}
