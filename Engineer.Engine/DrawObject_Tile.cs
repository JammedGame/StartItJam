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
    [XmlInclude(typeof(TileCollection))]
    public class Tile : DrawObject
    {
        private bool _Modified;
        private int _CurrentIndex;
        private TileCollection _Collection;
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
        public TileCollection Collection { get => _Collection; set => _Collection = value; }
        public Tile() : base()
        {
            this._CurrentIndex = 0;
            this.Type = DrawObjectType.Tile;
            this.Scale = new Mathematics.Vertex(100, 100, 1);
        }
        public Tile(Tile T) : base(T)
        {
            this._CurrentIndex = 0;
            this.Collection = new TileCollection(T.Collection);   
        }
        public void SetIndex(int Index)
        {
            if (Index < this.Collection.TileImages.Count) this._CurrentIndex = Index;
        }
        public bool InCollision(DrawObject Collider, Collision2DType Type)
        {
            return Collision2D.Check(this.Translation, this.Scale, Collider.Translation, Collider.Scale, Type);
        }
        public int Index()
        {
            return _CurrentIndex;
        }
    }
    public class TileCollection
    {
        private List<Bitmap> _TileImages;
        [XmlIgnore]
        public List<Bitmap> TileImages
        {
            get
            {
                return _TileImages;
            }

            set
            {
                _TileImages = value;
            }
        }
        public TileCollection()
        {
            this._TileImages = new List<Bitmap>();
        }
        public TileCollection(Bitmap TileImage)
        {
            this._TileImages = new List<Bitmap>();
            this._TileImages.Add(TileImage);
        }
        public TileCollection(TileCollection TC)
        {
            this._TileImages = new List<Bitmap>(TC._TileImages);
        }
    }
}
