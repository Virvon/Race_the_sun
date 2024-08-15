using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.Settings
{
    public class SettingsWindow : OpenableWindow
    {
        private const string MusicMixer = "Music";

        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _soundsVolumeSlider;
        [SerializeField] private AudioMixer _audioMixer;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

            _musicVolumeSlider.value = _persistentProgressService.Progress.AudioSettings.MusicVolume;
            _soundsVolumeSlider.value = _persistentProgressService.Progress.AudioSettings.SoundsVolume;

            ChangeMusicMixer(_persistentProgressService.Progress.AudioSettings.MusicVolume);

            _musicVolumeSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            _soundsVolumeSlider.onValueChanged.AddListener(OnSoundsSliderValueChanged);

        }

        private void OnDestroy()
        {
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            _soundsVolumeSlider.onValueChanged.RemoveListener(OnSoundsSliderValueChanged);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnSoundsSliderValueChanged(float value)
        {
            _persistentProgressService.Progress.AudioSettings.SoundsVolume = value;
        }

        private void OnMusicSliderValueChanged(float value)
        {
            _persistentProgressService.Progress.AudioSettings.MusicVolume = value;
            ChangeMusicMixer(value);
        }

        private void ChangeMusicMixer(float value)
        {
            _audioMixer.SetFloat(MusicMixer, value);
        }
    }
}
