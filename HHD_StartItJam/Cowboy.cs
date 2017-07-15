using Engineer.Engine;
using Engineer.Engine.IO;
using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    class Cowboy : Enemy
    {
        private int MoveSpeed;
        private DrawnSceneObject _Enemy;

        public Cowboy(Scene2D CScene, DrawnSceneObject _Player, int MS=1) : base(CScene, _Player)
        {
            _Enemy=CreateEnemy(CScene);
            this.MoveSpeed = MS;
        }

        public override void Behavior()
        {
           
            if (Math.Abs(_Player.Visual.Translation.X -_Enemy.Visual.Translation.X)<50)
            {
                ((Sprite)_Enemy.Visual).UpdateSpriteSet("Attack");
            }
            else if (Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) < 700)
            {
                ((Sprite)_Enemy.Visual).UpdateSpriteSet("Walk");
                if (Movement.AImoveLeft < 700)
                {
                    MoveLeft();
                    Movement.AImoveLeft++;
                    if(Movement.AImoveLeft==700) Movement.AImovRight = 0;
                }
                else if (Movement.AImovRight <1000)
                {
                    MoveRight();
                    Movement.AImovRight++;
                     if (Movement.AImovRight == 1000) Movement.AImoveLeft = 0;
                 }
            }
            else if (Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) > 700)
            {
                ((Sprite)_Enemy.Visual).UpdateSpriteSet("Idle");
            }
        }
        public void MoveLeft()
        {   //dodati sprajt za levo kretanje
            _Enemy.Visual.Translation = new Vertex(_Enemy.Visual.Translation.X - this.MoveSpeed,_Enemy.Visual.Translation.Y,0);
        }
        public void MoveRight()
        {    //dodati sprajt za desno kretanje
            _Enemy.Visual.Translation = new Vertex(_Enemy.Visual.Translation.X + this.MoveSpeed, _Enemy.Visual.Translation.Y, 0);
        }
        public void AttackLeft()
        {
            ((Sprite)_Enemy.Visual).UpdateSpriteSet("AttackLeft");
        }
        public void AttackRight()
        {
            ((Sprite)_Enemy.Visual).UpdateSpriteSet("AttackRight");
        }


        public DrawnSceneObject CreateEnemy(Scene2D Scene)
        {
            EFXInterface EFX = new EFXInterface();
            DrawnSceneObject _Enemy = (DrawnSceneObject)EFX.Load("Data/knight.efx");
            _Enemy.Name = "Cowboy";
            ((Sprite)_Enemy.Visual).SetSpriteSet("Walk");
            ((Sprite)_Enemy.Visual).Paint = Color.Red;

            _Enemy.Visual.Scale = new Vertex(250, 250, 0);
            _Enemy.Visual.Translation = new Vertex(900, 600, 0);
            _Enemy.ID = "djape";
            Scene.AddSceneObject(_Enemy);

            return _Enemy;
        }
        public  DrawnSceneObject Create(Scene2D CScene)
        {
            return CreateEnemy(CScene);
        }
    }
}

