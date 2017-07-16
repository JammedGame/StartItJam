using Engineer.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHD_StartItJam
{
    class Sound
    {
        private Dictionary<string, SoundSceneObject> Sounds;
        private Dictionary<string, bool> Looping;
        // When clicking left and right is starting sound, then after both keys are up it should
        // stop playing, that is why it is counting number of starts and number of stops
        private Dictionary<string, int> StackedTriggers;


        private static Sound Inst;

        public static Sound Instance()
        {
            if (Inst == null)
            {

                Sound S = new Sound();
                S.Add("walk", "Data/walk.wav", true);
                S.Add("swing", "Data/swing.wav");
                S.Add("kill", "Data/kill.wav");
                S.Add("bgd", "Data/bgd.wav", true);


                Inst = S;
            }
            return Inst;
        }

        public Sound()
        {
            Sounds = new Dictionary<string, SoundSceneObject>();
            Looping = new Dictionary<string, bool>();
            StackedTriggers = new Dictionary<string, int>();



        }

        public void Add(string Name, string Path, bool loop = false)
        {
            Sounds.Add(Name, new SoundSceneObject(Path, "audio_" + Name));
            Looping.Add(Name, loop);
            StackedTriggers.Add(Name, 0);
        }

        public void Play(string Name, bool incremental = false)
        {
            SoundSceneObject Sound = Sounds[Name];
            if (Looping[Name])
            {
               if (!Sound.IsPlaying()) {
                    StackedTriggers[Name] = incremental ? (StackedTriggers[Name] + 1) : 1;
                    Sound.PlayLooped();
               }
            }
            else
            {
                Sound.Play();
                StackedTriggers[Name] = incremental ? (StackedTriggers[Name] + 1) : 1;

            }

        }

        public void PlayIncremental(string Name)
        {
            Play(Name, true);
        }

        public void Stop(string Name, bool instant = false)
        {
            StackedTriggers[Name] -= 1;
            if (StackedTriggers[Name] <= 0)
            {
                Sounds[Name].Stop();

            }

        }

    }
}
