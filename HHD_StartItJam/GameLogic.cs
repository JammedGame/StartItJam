using Engineer.Engine;
using Engineer.Runner;
using OpenTK.Graphics;
using Engineer.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineer.Engine.IO;

namespace HHD_StartItJam
{
    public class GameLogic
    {
        private EFXInterface _EFX;
        private ExternRunner _Runner;
        private Game _Game;
        private Scene _Menu;
        private Scene _Current;
        public GameLogic()
        {
            ResourceManager _ResMan = new ResourceManager();
            _ResMan.Init();
            this._EFX = new EFXInterface();
            this._Game = new Game();
            this._Runner = new ExternRunner(1024, 600, new GraphicsMode(32, 24, 0, 8), "Death de sombre!");
            this._Runner.WindowState = OpenTK.WindowState.Normal;
            Engineer.Engine.Settings.GraphicsQuality = Quality.High;
            this._Current = new Scene2D("Test");
            this._Game.AddScene(this._Current);
            DrawnSceneObject Dragon = (DrawnSceneObject)this._EFX.Load("Data/Dragon.efx");
            this._Current.AddSceneObject(Dragon);
        }
        public void Run()
        {
            this._Runner.Init(this._Game, this._Current);
            this._Runner.Run();
        }
    }
}
