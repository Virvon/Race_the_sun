using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class AudioSettings
    {
        public float MusicVolume;
        public float SoundsVolume;

        public AudioSettings()
        {
            MusicVolume = 0;
            SoundsVolume = 0;
        }
    }
}