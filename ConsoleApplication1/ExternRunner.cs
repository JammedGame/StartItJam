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
using OpenTK.Input;

namespace Engineer.Runner
{
    public class ExternRunner : Runner
    {
        private EventManager _Events;
        private List<EventManager> _ObjectEvents;
        public ExternRunner(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title)
        {
        }
        protected override void CallEvents(string EventName, EventArguments Args)
        {
            _CurrentScene.Events.Extern.Invoke(EventName, _CurrentGame, Args);
        }
        protected override void CallObjectEvents(int Index, string EventName, EventArguments Args)
        {
            _CurrentScene.Objects[Index].Events.Extern.Invoke(EventName, _CurrentGame, Args);
        }
    }
}
