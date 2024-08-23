using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Assets.RaceTheSun.Sources.Audio
{
    public class StageMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _startStageAudioSource;
        [SerializeField] private AudioSource _stage1AudioSource;
        [SerializeField] private AudioSource _stage2AudioSource;
        [SerializeField] private AudioSource _stage3AudioSource;
        [SerializeField] private AudioSource _stage4AudioSource;
        [SerializeField] private AudioSource _bonusStageAudioSource;
        [SerializeField] private AudioSource _betweenStageAudioSource;

        [SerializeField] private float _fadingDuration;
        [SerializeField] private AudioMixer _mixer;

        private AudioClip _currentAudioClip;
        private Coroutine _coroutine;
        private CurrentSpaceshipStage _currentSpcaceshipStage;
        private IStaticDataService _staticDataService;
        private bool _isPaused;

        private AudioSource _currentAudioSource;

        [Inject]
        private void Construct(CurrentSpaceshipStage currentSpaceshipStage, IStaticDataService staticDataService)
        {
            _currentSpcaceshipStage = currentSpaceshipStage;
            _staticDataService = staticDataService;

            _isPaused = false;
            _currentAudioSource = _startStageAudioSource;

            _currentSpcaceshipStage.StageChanged += ChangeAudioClip;
        }

        private void OnDestroy()
        {
            _currentSpcaceshipStage.StageChanged -= ChangeAudioClip;
        }

        public void Pause()
        {
            _currentAudioSource.Pause();
            _isPaused = true;
        }

        public void Play()
        {
            if(_isPaused)
            {
                _currentAudioSource.Play();
                _isPaused = false;
            }
        }

        private void ChangeAudioClip(Stage currentStage)
        {
            //if (_coroutine != null)
            //    StopCoroutine(_coroutine);

            //AudioClip audioClip = _staticDataService.GetStage(currentStage).AudioClip;

            //_coroutine = StartCoroutine(Changer(audioClip));

            //_startStageAudioSource.Stop();
            //_startStageAudioSource.clip = _staticDataService.GetStage(currentStage).AudioClip;
            //_startStageAudioSource.Play();
            //_isPaused = false;

            AudioSource targetAudioSource = null;

            switch (currentStage)
            {
                case Stage.BonusStage:
                    targetAudioSource = _bonusStageAudioSource;
                    break;
                case Stage.BetweenStages:
                    targetAudioSource = _betweenStageAudioSource;
                    break;
                case Stage.StartStage:
                    targetAudioSource = _startStageAudioSource;
                    break;
                case Stage.Stage1:
                    targetAudioSource = _stage1AudioSource;
                    break;
                case Stage.Stage2:
                    targetAudioSource = _stage2AudioSource;
                    break;
                case Stage.Stage3:
                    targetAudioSource = _stage3AudioSource;
                    break;
                case Stage.Stage4:
                    targetAudioSource = _stage4AudioSource;
                    break;
            }

            if(targetAudioSource == null)
            {
                Debug.Log("audio source not founded");
                return;
            }

            _currentAudioSource.Stop();
            _currentAudioSource = targetAudioSource;
            _currentAudioSource.Play();
            _isPaused = false;
        }

        private IEnumerator Changer(AudioClip targetAudioClip)
        {
            float passedTime = 0;
            float progress;
            float startVolume = _startStageAudioSource.volume;

            if(_currentAudioClip != null)
            {
                while(_startStageAudioSource.volume != 0)
                {
                    passedTime += Time.deltaTime;
                    progress = passedTime / _fadingDuration;

                    _startStageAudioSource.volume = Mathf.Lerp(startVolume, 0, progress);

                    yield return null;
                }
            }

            if(targetAudioClip != null)
            {
                _startStageAudioSource.clip = targetAudioClip;
                _startStageAudioSource.Play();
                passedTime = 0;

                while (_startStageAudioSource.volume != startVolume)
                {
                    passedTime += Time.deltaTime;
                    progress = passedTime / _fadingDuration;

                    _startStageAudioSource.volume = Mathf.Lerp(0, startVolume, progress);

                    yield return null;
                }
            }
        }

        public class Factory : PlaceholderFactory<string, UniTask<StageMusic>>
        {
        }
    }
}
