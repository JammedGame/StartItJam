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
        public SceneLogic(Scene2D CScene)
        {
            this._Scene = CScene;
            Level.Create(this._Scene);
        }
    }
}
