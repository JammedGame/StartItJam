using Engineer.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    public class SceneLogic
    {
        private Scene2D _Scene;
        private Movement _Movement;
        private DrawnSceneObject _Player;
        public SceneLogic(Scene2D CScene)
        {
            this._Scene = CScene;
            Level.Create(this._Scene);
            _Player = Character.Create(CScene);
            CreateCharacter();
            this._Movement = new Movement(_Player, CScene);
            CScene.Events.Extern.TimerTick += new GameEventHandler(GameUpdateEvent);
            CScene.Events.Extern.KeyDown += new GameEventHandler(_Movement.KeyDownEvent);
            CScene.Events.Extern.KeyUp += new GameEventHandler(_Movement.KeyUpEvent);
            CScene.Events.Extern.KeyPress += new GameEventHandler(_Movement.KeyPressEvent);
        }
        private void CreateCharacter()
        {
            _Player.Data["Direction"] = 0;
            _Player.Data["Collision"] = true;
            _Player.Data["skokBrojac"] = 0;
            _Player.Data["padBrojac"] = 0;
            _Player.Data["colliding"] = true;
            _Player.Data["flying"] = false;
        }
        public void GameUpdateEvent(Game G, EventArguments E)
        {
            
        }
    }
}
