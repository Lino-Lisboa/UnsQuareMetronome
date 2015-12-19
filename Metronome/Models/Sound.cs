using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metronome.Models
{
    public class Sound
    {
        public string Name { get; set; }
        public string AudioFile { get; set; }


        public Sound(string name)
        {
            Name = name;
            AudioFile = String.Format("/Assets/Audios/{0}.wav", name);
        }        

    }

    class SoundManager
    {

        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            var allSounds = getSounds();
            sounds.Clear();
            allSounds.ForEach(p => sounds.Add(p));
        }



        private static List<Sound> getSounds()
        {
            var sounds = new List<Sound>();

            sounds.Add(new Sound("Click"));
            sounds.Add(new Sound("Tick"));

            return sounds;
        }



    }
}
