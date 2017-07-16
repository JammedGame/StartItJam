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
using Engineer.Mathematics;
using System.Windows.Forms;

namespace HHD_StartItJam
{
    public class GameLogic
    {
        public static float _GlobalScale;
        private EFXInterface _EFX;
        private ExternRunner _Runner;
        private Game _Game;
        private Scene _Menu;
        private Scene2D _Current;
        private SceneLogic _Logic;
        private static GameLogic Instance;

        public static GameLogic Create()
        {
            if (Instance == null)
            {
                Instance = new GameLogic();
            }
            return Instance;
        }

        public GameLogic()
        {
            ResourceManager _ResMan = new ResourceManager();
            _ResMan.Init();
            this._EFX = new EFXInterface();
            this._Game = new Game();
            this._Runner = new ExternRunner(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, new GraphicsMode(32, 24, 0, 8), "Tekillah!");
            GameLogic._GlobalScale = _Runner.Height / 1080.0f;
            this._Runner.WindowState = OpenTK.WindowState.Normal;
            Engineer.Engine.Settings.GraphicsQuality = Quality.Medium;
            this._Current = new Scene2D("Test");
            this._Current.Transformation.Scale = new Vertex(_GlobalScale, _GlobalScale, 0);
            this._Game.Scenes.Add(this._Current);
            this._Logic = new SceneLogic(this._Current);
        }
        public void RunMenu()
        {
            Menu M = new Menu();
            //this._Runner.Init(this._Game, this._Current);
            this._Runner.Init(this._Game, M.CreateMenuScene(this._Runner));
           // this._Runner.Run();
        }
        public void RunGame()
        {
            Menu M = new Menu();
            this._Runner.Init(this._Game, _Current);
            this._Runner.Run();
        }

        public void Stop()
        {
            _Runner.Close();
        }
    }
}
