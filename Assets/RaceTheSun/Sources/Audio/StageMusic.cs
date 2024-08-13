using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Audio
{
    public class StageMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _fadingDuration;

        private AudioClip _currentAudioClip;
        private Coroutine _coroutine;
        private CurrentSpaceshipStage _currentSpcaceshipStage;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(CurrentSpaceshipStage currentSpaceshipStage, IStaticDataService staticDataService)
        {
            _currentSpcaceshipStage = currentSpaceshipStage;
            _staticDataService = staticDataService;

            _currentSpcaceshipStage.StageChanged += ChangeAudioClip;
            Debug.Log("+=");
        }

        private void OnDestroy()
        {
            _currentSpcaceshipStage.StageChanged -= ChangeAudioClip;
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        public void Play()
        {
            _audioSource.Play();
        }

        private void ChangeAudioClip(Stage currentStage)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            AudioClip audioClip = _staticDataService.GetStage(currentStage).AudioClip;

            _coroutine = StartCoroutine(Changer(audioClip));
        }

        private IEnumerator Changer(AudioClip targetAudioClip)
        {
            Debug.Log("Change " + targetAudioClip.name);

            float passedTime = 0;
            float progress;
            float startVolume = _audioSource.volume;

            if(_currentAudioClip != null)
            {
                while(_audioSource.volume != 0)
                {
                    passedTime += Time.deltaTime;
                    progress = passedTime / _fadingDuration;

                    _audioSource.volume = Mathf.Lerp(startVolume, 0, progress);

                    yield return null;
                }
            }

            if(targetAudioClip != null)
            {
                _audioSource.clip = targetAudioClip;
                _audioSource.Play();
                passedTime = 0;

                while (_audioSource.volume != startVolume)
                {
                    passedTime += Time.deltaTime;
                    progress = passedTime / _fadingDuration;

                    _audioSource.volume = Mathf.Lerp(0, startVolume, progress);

                    yield return null;
                }
            }
        }

        public class Factory : PlaceholderFactory<string, UniTask<StageMusic>>
        {
        }
    }
}
