using Engineer.Engine;
using Engineer.Mathematics;
using Engineer.Runner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    public class Menu
    {
        private GameLogic Logic;
        private Scene2D PlayScene;
        public Scene CreateMenuScene(Runner Run)
        {
            Scene2D MenuScene = new Scene2D("Menu");
            MenuScene.BackColor = Color.FromArgb(41, 216, 238);

            DrawnSceneObject Back = Level.CreateStaticSprite("Back", ResourceManager.Images["Back"], new Vertex(0, 0, 0), new Vertex(Run.Width, Run.Height, 0), false);
            MenuScene.AddSceneObject(Back);

            

            SceneObject Play = Level.CreateStaticSprite("Play", ResourceManager.Images["Surface"], new Engineer.Mathematics.Vertex(200, 790, 0), new Engineer.Mathematics.Vertex(300, 120, 0), true);
            Play.Events.Extern.MouseClick += new GameEventHandler(this.PlayClickEvent);
            MenuScene.AddSceneObject(Play);
            SceneObject Exit = Level.CreateStaticSprite("Exit", ResourceManager.Images["Surface"], new Engineer.Mathematics.Vertex(1400, 790, 0), new Engineer.Mathematics.Vertex(300, 120, 0), true);
            Exit.Events.Extern.MouseClick += new GameEventHandler(this.ExitClickEvent);
            MenuScene.AddSceneObject(Exit);

            return MenuScene;
        }
        public void PlayClickEvent(Game G, EventArguments E)
        {
            GameLogic.Create().RunGame();

        }
        public void ResetPlay(Game G)
        {
            if (G.Scenes.Count > 1) G.Scenes.RemoveAt(1);

            Logic = new GameLogic();
            PlayScene = new Scene2D("Play Scene");
            PlayScene.BackColor = Color.FromArgb(0, 210, 127);
            Logic.RunGame();
            G.Scenes.Add(PlayScene);
        }
        public void ExitClickEvent(Game G, EventArguments E)
        {
            GameLogic.Create().Stop();
        }
    }
}
