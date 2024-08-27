using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.Settings
{
    public class SettingsWindow : OpenableWindow
    {
        private const string MusicMixer = "Music";
        private const string SoundsMixer = "Sounds";
        private const int MinValue = -20;

        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _soundsVolumeSlider;
        [SerializeField] private AudioMixer _audioMixer;

        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;

            _musicVolumeSlider.value = _persistentProgressService.Progress.AudioSettings.MusicVolume;
            _soundsVolumeSlider.value = _persistentProgressService.Progress.AudioSettings.SoundsVolume;

            _musicVolumeSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            _soundsVolumeSlider.onValueChanged.AddListener(OnSoundsSliderValueChanged);

            _musicVolumeSlider.value = _persistentProgressService.Progress.AudioSettings.MusicVolume;
            _soundsVolumeSlider.value = _persistentProgressService.Progress.AudioSettings.SoundsVolume;

            ChangeMixerVolume(MusicMixer, _persistentProgressService.Progress.AudioSettings.MusicVolume);
            ChangeMixerVolume(SoundsMixer, _persistentProgressService.Progress.AudioSettings.SoundsVolume);
        }

        private void OnDestroy()
        {
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            _soundsVolumeSlider.onValueChanged.RemoveListener(OnSoundsSliderValueChanged);
        }

        public override void Hide()
        {
            _saveLoadService.SaveProgress();
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnMusicSliderValueChanged(float value)
        {
            _persistentProgressService.Progress.AudioSettings.MusicVolume = value;
            ChangeMixerVolume(MusicMixer, value);
        }

        private void OnSoundsSliderValueChanged(float value)
        {
            _persistentProgressService.Progress.AudioSettings.SoundsVolume = value;
            ChangeMixerVolume(SoundsMixer, value);
        }

        private void ChangeMixerVolume(string mixerName, float value)
        {
            if (value <= MinValue)
                value = -80;

            _audioMixer.SetFloat(mixerName, value);
        }
    }
}
