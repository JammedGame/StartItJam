using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace Engineer.Runner
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            using (var MainWindow = new ScriptedRunner(800, 600, new GraphicsMode(32, 24, 0, 8), "Engineer Runner"))
            {
                MainWindow.Run();
            }
        }
    }
}
