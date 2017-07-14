using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineer.Engine
{
    public enum Quality
    {
        Low = 1,
        Medium = 2,
        High = 4,
    }
    public class Settings
    {
        public static string Version = "0.0.0.1a";
        public static Quality GraphicsQuality = Quality.High;
    }
}
