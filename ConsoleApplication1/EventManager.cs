using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using Engineer.Engine;

namespace Engineer.Runner
{
    public class EventManager
    {
        private Dictionary<string, EventDelegates> _Events;
        public void AddEvents(string Name, List<ScriptSceneObject> Events)
        {
            if (_Events == null) _Events = new Dictionary<string, EventDelegates>();
            EventDelegates EventDelegates = new EventDelegates(Events);
            _Events.Add(Name, EventDelegates);
        }
        public void Run(string Name, Game CurrentGame, EventArguments Args)
        {
            _Events[Name].Run(CurrentGame, Args);
        }
    }
    public class EventDelegates
    {
        private static string _ScriptHeader =       "using Engineer.Data;\n"+
                                                    "using Engineer.Engine;\n"+
                                                    "using Engineer.Mathematics;\n"+
                                                    "using System;\n"+
                                                    "using System.Collections.Generic;\n";
        private List<ScriptSceneObject> _Events;
        public EventDelegates(List<ScriptSceneObject> Events)
        {
            this._Events = Events;

        }
        public void Run(Game CurrentGame, EventArguments Args)
        {

        }
    }
}
