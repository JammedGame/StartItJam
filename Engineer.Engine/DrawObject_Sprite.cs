using Engineer.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engineer.Engine
{
    [XmlInclude(typeof(SpriteSet))]
    public class Sprite : DrawObject
    {
        private bool _Modified;
        private int _CurrentIndex;
        private int _CurrentSpriteSet;
        private List<SpriteSet> _SpriteSets;
        private List<Sprite> _SubSprites;
        public bool Modified
        {
            get
            {
                return _Modified;
            }

            set
            {
                _Modified = value;
            }
        }
        [XmlIgnore]
        public List<SpriteSet> SpriteSets
        {
            get
            {
                return _SpriteSets;
            }

            set
            {
                _SpriteSets = value;
            }
        }
        [XmlIgnore]
        public List<Sprite> SubSprites
        {
            get
            {
                return _SubSprites;
            }

            set
            {
                _SubSprites = value;
            }
        }
        public Sprite() : base()
        {
            this._CurrentIndex = 0;
            this.Type = DrawObjectType.Sprite;
            this._SpriteSets = new List<SpriteSet>();
            this.Scale = new Mathematics.Vertex(100,100,1);
            this._SubSprites = new List<Sprite>();
        }
        public Sprite(Sprite S) : base(S)
        {
            this._CurrentIndex = 0;
            this._SpriteSets = new List<SpriteSet>();
            for (int i = 0; i < S._SpriteSets.Count; i++) this._SpriteSets.Add(new SpriteSet(S._SpriteSets[i]));
            this._SubSprites = new List<Sprite>();
            for(int i = 0; i <S.SubSprites.Count; i++)
            {
                _SubSprites.Add(new Sprite(S.SubSprites[i]));
            }
        }
        public List<Bitmap> CollectiveLists()
        {
            List<Bitmap> Lists = new List<Bitmap>();
            for(int i = 0; i < _SpriteSets.Count; i++)
            {
                Lists.AddRange(_SpriteSets[i].Sprite);
            }
            return Lists;
        }
        public void RaiseIndex()
        {
            _CurrentIndex++;
            if (_SpriteSets.Count <= 0) _CurrentIndex = -1;
            else if (_CurrentIndex >= _SpriteSets[_CurrentSpriteSet].Sprite.Count) _CurrentIndex = 0;
        }
        public void SetSpriteSet(int Index)
        {
            if (Index >= _SpriteSets.Count) return;
            _CurrentSpriteSet = Index;
            _CurrentIndex = 0;
        }
        public void SetSpriteSet(string Name)
        {
            for(int i = 0; i < this._SpriteSets.Count; i++)
            {
                if (this._SpriteSets[i].Name == Name) this.SetSpriteSet(i);
            }
        }
        public void UpdateSpriteSet(int Index)
        {
            if (Index != _CurrentSpriteSet) SetSpriteSet(Index);
        }
        public void UpdateSpriteSet(string Name)
        {
            for (int i = 0; i < this._SpriteSets.Count; i++)
            {
                if (this._SpriteSets[i].Name == Name) this.UpdateSpriteSet(i);
            }
        }
        public bool InCollision(DrawObject Collider, Collision2DType Type)
        {
            return Collision2D.Check(this.Translation, this.Scale, Collider.Translation, Collider.Scale, Type);
        }
        public int Index()
        {
            int Index = 0;
            for(int i = 0; i < _CurrentSpriteSet; i++)
            {
                Index += _SpriteSets[i].Sprite.Count;
            }
            Index += _CurrentIndex;
            return Index;
        }
        public int IO_CurrentSpriteSet
        { get => _CurrentSpriteSet; set => _CurrentSpriteSet = value; }
    }
    public class SpriteSet
    {
        private string _ID;
        private string _Name;
        private List<Bitmap> _Sprites;
        public string ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }
        [XmlIgnore]
        public List<Bitmap> Sprite
        {
            get
            {
                return _Sprites;
            }

            set
            {
                _Sprites = value;
            }
        }
        public SpriteSet()
        {
            this._ID = Guid.NewGuid().ToString();
            this._Name = this._ID;
            this._Sprites = new List<Bitmap>();
        }
        public SpriteSet(string Name)
        {
            this._ID = Guid.NewGuid().ToString();
            this._Name = Name;
            this._Sprites = new List<Bitmap>();
        }
        public SpriteSet(string Name, Bitmap SpriteImage)
        {
            this._ID = Guid.NewGuid().ToString();
            this._Name = Name;
            this._Sprites = new List<Bitmap>();
            this._Sprites.Add(SpriteImage);
        }
        public SpriteSet(SpriteSet SS)
        {
            this._ID = SS._ID;
            this._Name = SS._Name;
            this._Sprites = new List<Bitmap>(SS._Sprites);
        }
    }
}
