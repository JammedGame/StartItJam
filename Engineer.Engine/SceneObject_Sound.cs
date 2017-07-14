using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Engine
{
    public class SoundSceneObject : SceneObject
    {
        public SoundSceneObject() : base()
        {
            this.Type = SceneObjectType.SoundSceneObject;
        }
        public SoundSceneObject(string Name) : base(Name)
        {
            this.Type = SceneObjectType.SoundSceneObject;
        }
        public SoundSceneObject(SoundSceneObject SSO, Scene ParentScene) : base(SSO, ParentScene)
        {
            this.Type = SceneObjectType.SoundSceneObject;
        }
    }
}
