//#define FixedPipeline
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Engineer.Mathematics;
using Engineer.Data;
using Engineer.Draw;
using Engineer.Draw.OpenGL;
using Engineer.Draw.OpenGL.FixedGL;
using Engineer.Draw.OpenGL.GLSL;
using Engineer.Engine;
using CSScriptLibrary;
using OpenTK.Input;

namespace Engineer.Runner
{
    public class ScriptedRunner : Runner
    {
        private EventManager _Events;
        private List<EventManager> _ObjectEvents;
        public ScriptedRunner(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title)
        {
        }
        protected override void PrepareEvents()
        {
            _Events = new EventManager();
            for (int i = 0; i < _CurrentScene.Events.EventList.Count; i++)
            {
                _Events.AddEvents(_CurrentScene.Events.EventList[i].ID, _CurrentScene.Events.EventList[i].Events);
            }
            _ObjectEvents = new List<EventManager>();
            for (int i = 0; i < _CurrentScene.Objects.Count; i++)
            {
                _ObjectEvents.Add(new EventManager());
                for (int j = 0; j < _CurrentScene.Objects[i].Events.EventList.Count; j++)
                {
                    _ObjectEvents[i].AddEvents(_CurrentScene.Objects[i].Events.EventList[j].ID, _CurrentScene.Objects[i].Events.EventList[j].Events);
                }
            }
        }
        protected override void CallEvents(string EventName, EventArguments Args)
        {
            _Events.Run(EventName, _CurrentGame, Args);
        }
        protected override void CallObjectEvents(int Index, string EventName, EventArguments Args)
        {
            _ObjectEvents[Index].Run(EventName, _CurrentGame, Args);
        }
    }
}
