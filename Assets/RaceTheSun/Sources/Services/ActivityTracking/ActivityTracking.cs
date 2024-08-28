using Agava.WebUtility;
using Assets.RaceTheSun.Sources.Services.TimeScale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Audio;

namespace Assets.RaceTheSun.Sources.Services.ActivityTracking
{
    public class ActivityTracking
    {
        private const string MasterMixer = "Master";
        private const int MutedSoundVolume = -80;
        private const int NormalSoundVolume = 0;

        private ITimeScale _timeScale;
        private AudioMixerGroup _audioMixer;

        public ActivityTracking(ITimeScale timeScale, AudioMixerGroup audioMixer)
        {
            _timeScale = timeScale;
            _audioMixer = audioMixer;

            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        ~ActivityTracking() =>
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

        private void OnInBackgroundChange(bool inBackground)
        {
            if (inBackground)
            {
                _timeScale.Scale(TimeScaleType.Pause);
                _audioMixer.audioMixer.SetFloat(MasterMixer, MutedSoundVolume);
            }
            else
            {
                _timeScale.Scale(TimeScaleType.Normal);
                _audioMixer.audioMixer.SetFloat(MasterMixer, NormalSoundVolume);
            }
        }
    }
}
