using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Engineer.Engine
{
    public class SoundSceneObject : SceneObject
    {
        private bool _Looped;
        private bool Playing;
        private string _Path;
        private MediaPlayer _Player;
        private EventHandler _LoopHandler;
        public string Path
        {
            get
            {
                return _Path;
            }

            set
            {
                _Path = value;
                this._Player = new System.Windows.Media.MediaPlayer();
                this._Player.Open(new Uri(Path, UriKind.Relative));
            }
        }
        public SoundSceneObject() : base()
        {
            this.Type = SceneObjectType.SoundSceneObject;
            this._Looped = false;
            this._LoopHandler = new EventHandler(this.Ended);
        }
        public SoundSceneObject(string Path, string Name) : base(Name)
        {
            this.Type = SceneObjectType.SoundSceneObject;
            this.Name = Name;
            this._Looped = false;
            this._Player = new System.Windows.Media.MediaPlayer();
            this._Player.Open(new Uri(Path, UriKind.Relative));
            this._LoopHandler = new EventHandler(this.Ended);
            this._Player.MediaEnded += this._LoopHandler;

        }
        public SoundSceneObject(SoundSceneObject SSO, Scene ParentScene) : base(SSO, ParentScene)
        {
            this.Type = SceneObjectType.SoundSceneObject;
            this._Looped = SSO._Looped;
            this._Player = SSO._Player;
            this._LoopHandler = new EventHandler(this.Ended);
            this._Player.MediaEnded += this._LoopHandler;

        }
        public void Play()
        {
            this._Player.Stop();
            this._Player.Play();
            this._Looped = false;
            this.Playing = true;
        }
        public void PlayLooped()
        {
            // If looped just continue
            // this._Player.Stop();
            this._Player.Play();
            this._Looped = true;
            this.Playing = true;

        }
        private void Ended(object sender, EventArgs e)
        {
            if (this._Looped)
            {
                this._Player.Stop();
                this._Player.Play();
            }
            else
            {
                Playing = false;
            }
        }
        public void Stop()
        {
            this.Playing = false;
            if (_Looped)
            {
                _Player.Pause();
            }
            else
            {
                _Player.Stop();
            }
        }
        public bool IsPlaying()
        {
            return Playing;
        }
    }
}
