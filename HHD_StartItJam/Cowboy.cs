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
        private int id;
        private int MoveSpeed;
        private int _Sight;
        private int _LeftAreaWalk;
        private int _RightAreaWalk;
        private EnemyMove _Move;
        private Vertex _OriginalLocation;
        private DrawnSceneObject _Enemy;
        public DrawnSceneObject Enemy
        {
            get { return _Enemy; }
            set { _Enemy = value; }
        }
        public Cowboy(Scene2D CScene, DrawnSceneObject _Player,int x, int y, int MoveSpeed=1, int Sight = 700, int LeftAreaWalk=100, int RightAreaWalk=100) : base(CScene, _Player)
        {
            _Enemy=CreateEnemy(CScene,x,y);
            this.MoveSpeed = MoveSpeed;
            this.id = Sid;
            this._Sight = Sight;
            this._LeftAreaWalk = LeftAreaWalk;
            this._RightAreaWalk = RightAreaWalk;
            this._OriginalLocation = _Enemy.Visual.Translation;
        }
        public override void Behavior()
        {
            if (_Move == EnemyMove.None) _Move = EnemyMove.Left;
            if (Math.Abs(_Player.Visual.Translation.X -_Enemy.Visual.Translation.X)<75 && Math.Abs(_Player.Visual.Translation.Y - _Enemy.Visual.Translation.Y) < 75)
            {
                ((Sprite)_Enemy.Visual).UpdateSpriteSet("AttR");
            }
            else if (Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) < _Sight)
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
            else if (Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) > _Sight)
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
            /*if (Math.Abs(_Player.Visual.Translation.X - _Enemy.Visual.Translation.X) < 100 && Math.Abs(_Player.Visual.Translation.Y - _Enemy.Visual.Translation.Y) < 100)
            {
                return id;
            }
            else */return -1;
        }
        public DrawnSceneObject CreateEnemy(Scene2D Scene,int x,int y)
        {
            SpriteSet IdleR = new SpriteSet("IdleR");
            for(int i=0;i<2;i++) IdleR.Sprite.Add(ResourceManager.Images["idleR"+i]);
            SpriteSet WalkR = new SpriteSet("WalkR");
            for (int i = 0; i <21; i++) WalkR.Sprite.Add(ResourceManager.Images["walkR" + i]);
            SpriteSet AttR = new SpriteSet("AttR");
            for (int i = 0; i < 2; i++) AttR.Sprite.Add(ResourceManager.Images["attR" + i]);
            SpriteSet IdleL = new SpriteSet("IdleL");
            for (int i = 0; i < 2; i++) IdleL.Sprite.Add(ResourceManager.Images["idleL" + i]);
            SpriteSet WalkL = new SpriteSet("WalkL");
            for (int i = 0; i < 21; i++) WalkL.Sprite.Add(ResourceManager.Images["walkL" + i]);
            SpriteSet AttL = new SpriteSet("AttL");
            for (int i = 0; i < 2; i++) AttL.Sprite.Add(ResourceManager.Images["attL" + i]);


            Sprite CharSprite = new Sprite();
            CharSprite.SpriteSets.Add(IdleR);
            CharSprite.SpriteSets.Add(WalkR);
            CharSprite.SpriteSets.Add(AttR);
            CharSprite.SpriteSets.Add(IdleL);
            CharSprite.SpriteSets.Add(WalkL);
            CharSprite.SpriteSets.Add(AttL);

            CharSprite.Scale = new Vertex(250, 250, 0);
            CharSprite.Translation = new Vertex(x , y , 0);
            
            DrawnSceneObject Char = new DrawnSceneObject("Cowboy", CharSprite);            
            Char.ID = "djape" + Sid++;

            Scene.AddSceneObject(Char);    
         
            return Char;
        }       
    }
}

