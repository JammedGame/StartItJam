using Engineer.Engine;
using Engineer.Engine.IO;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{    
    class Enemy
    {
        protected Scene2D CScene;
        protected DrawnSceneObject _Player;
        
        public Enemy(Scene2D CScene, DrawnSceneObject _Player)
        {
            this.CScene = CScene;
            this._Player = _Player;
           
        }

        public virtual void Behavior()
        {          
        }
        
    }
}
