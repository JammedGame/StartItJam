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
    enum EnemyMove
    {
        None = 0,
        Left = 1,
        Right = 2
    }
    class Cowboy : Enemy
    {
        private static int Sid = 0;
        private int _AtkTimer = 0;
        private int id;
        private int MoveSpeed;
        private int _Sight;
        private int _LeftAreaWalk;
        private int _RightAreaWalk;
        private int _AttackRadius;
        private EnemyMove _Move;
        private Vertex _OriginalLocation;
        private DrawnSceneObject _Enemy;
        public DrawnSceneObject Enemy
        {
            get { return _Enemy; }
            set { _Enemy = value; }
        }
        public Cowboy(Scene2D CScene, DrawnSceneObject _Player,int x, int y, int MoveSpeed=1, int Sight = 700, int LeftAreaWalk=100, int RightAreaWalk=100, int AttackRadius = 200) : base(CScene, _Player)
        {
            _Enemy=CreateEnemy(CScene,x,y);
            this.MoveSpeed = MoveSpeed;
            this.id = Sid;
            this._Sight = Sight;
            this._LeftAreaWalk = LeftAreaWalk;
            this._RightAreaWalk = RightAreaWalk;
            this._OriginalLocation = _Enemy.Visual.Translation;
            this._AttackRadius = AttackRadius;
        }
        public override void Behavior()
        {
            if (_AtkTimer != 0) _AtkTimer--;
            if(_AtkTimer == 1)
            {
                _AtkTimer = 0;
                HealthBar.subHealth(5);
                if (HealthBar.empty())
                {
                    GameLogic.Create().RunMenu();
                }
            }
            if (_Move == EnemyMove.None) _Move = EnemyMove.Left;
            if (Math.Abs(_Player.Visual.Translation.X -_Enemy.Visual.Translation.X) <_AttackRadius && Math.Abs(_Player.Visual.Translation.Y - _Enemy.Visual.Translation.Y) < _AttackRadius)
            {
                if (_AtkTimer == 0)
                {
                    if (_Player.Visual.Translation.X < _Enemy.Visual.Translation.X)
                    {
                        ((Sprite)_Enemy.Visual).UpdateSpriteSet("AttL");
                        _AtkTimer = 20;
                    }
                    else if (_Player.Visual.Translation.X > _Enemy.Visual.Translation.X)
                    {
                        ((Sprite)_Enemy.Visual).UpdateSpriteSet("AttR");
                        _AtkTimer = 20;
                    }
                }
            }
            else if (Math.Abs(_Player.Visual.Translation.Y - _Enemy.Visual.Translation.Y) < _Sight && Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) < _Sight)
            {
                if (_Player.Visual.Translation.X < _Enemy.Visual.Translation.X) _Move = EnemyMove.Left;
                else _Move = EnemyMove.Right;
                Vertex Moved = Movement.Move.GlobalTrans;
                if (_Move == EnemyMove.Left) if ((_OriginalLocation.X - (_Player.Visual.Translation.X + Moved.X)) > this._LeftAreaWalk) _Move = EnemyMove.Right;
                else if (_Move == EnemyMove.Right) if (((_Player.Visual.Translation.X + Moved.X) - _OriginalLocation.X) > this._RightAreaWalk) _Move = EnemyMove.Left;
                if (_Move == EnemyMove.Left)
                {
                    ((Sprite)_Enemy.Visual).UpdateSpriteSet("WalkL");
                    MoveLeft();
                }
                else
                {
                    ((Sprite)_Enemy.Visual).UpdateSpriteSet("WalkR");
                    MoveRight();
                }
            }
            else
            {
                if (_Move == EnemyMove.Left)
                {
                    ((Sprite)_Enemy.Visual).UpdateSpriteSet("WalkL");
                    MoveLeft();
                    Vertex Moved = Movement.Move.GlobalTrans;
                    if ((_OriginalLocation.X - (_Enemy.Visual.Translation.X + Moved.X)) > this._LeftAreaWalk) _Move = EnemyMove.Right;
                }
                else if (_Move == EnemyMove.Right)
                {
                    ((Sprite)_Enemy.Visual).UpdateSpriteSet("WalkR");
                    MoveRight();
                    Vertex Moved = Movement.Move.GlobalTrans;
                    if (((_Enemy.Visual.Translation.X + Moved.X) - _OriginalLocation.X) > this._RightAreaWalk) _Move = EnemyMove.Left;
                }
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
            ((Sprite)_Enemy.Visual).UpdateSpriteSet("AttL");
        }
        public void AttackRight()
        {
            ((Sprite)_Enemy.Visual).UpdateSpriteSet("AttR");
        }
        public bool EnemyHit()
        {
            if (!this._Enemy.Active) return false;
            if (Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) < 75 && Math.Abs(_Player.Visual.Translation.Y - _Enemy.Visual.Translation.Y) < 75)
            {
                return true;
            }
            else return false;
        }
        public int  PlayerHit()
        {
            if (Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) < 100 && Math.Abs(_Player.Visual.Translation.Y - _Enemy.Visual.Translation.Y) < 100)
            {
                return id;
            }
            else return -1;
        }
        public DrawnSceneObject CreateEnemy(Scene2D Scene,int x,int y)
        {
            SpriteSet IdleR = new SpriteSet("IdleR");
            for(int i=0;i<1;i++) IdleR.Sprite.Add(ResourceManager.Images["Kaub1Walk"+(i+1)]);
            SpriteSet WalkR = new SpriteSet("WalkR");
            for (int i = 0; i < 9; i++) WalkR.Sprite.Add(ResourceManager.Images["Kaub1Walk" + (i + 1)]);
            SpriteSet AttR = new SpriteSet("AttR");
            for (int i = 0; i < 6; i++) AttR.Sprite.Add(ResourceManager.Images["Kaub1Att" + (i + 1)]);
            SpriteSet IdleL = new SpriteSet("IdleL");
            for (int i = 0; i < 1; i++) IdleL.Sprite.Add(ResourceManager.Images["Kaub1Walk" + (i + 1) + "Fliped"]);
            SpriteSet WalkL = new SpriteSet("WalkL");
            for (int i = 0; i < 9; i++) WalkL.Sprite.Add(ResourceManager.Images["Kaub1Walk" + (i + 1) + "Fliped"]);
            SpriteSet AttL = new SpriteSet("AttL");
            for (int i = 0; i < 6; i++) AttL.Sprite.Add(ResourceManager.Images["Kaub1Att" + (i + 1) + "Fliped"]);


            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(IdleR);
            CharSprite.SpriteSets.Add(WalkR);
            CharSprite.SpriteSets.Add(AttR);
            CharSprite.SpriteSets.Add(IdleL);
            CharSprite.SpriteSets.Add(WalkL);
            CharSprite.SpriteSets.Add(AttL);

            CharSprite.Scale = new Vertex(300, 300, 0);
            CharSprite.Translation = new Vertex(x , y , 0);
            
            DrawnSceneObject Char = new DrawnSceneObject("Cowboy", CharSprite);            
            Char.ID = "djape" + Sid++;

            Scene.AddSceneObject(Char);    
         
            return Char;
        }       
    }
}

