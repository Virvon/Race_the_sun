using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Audio
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

        private CurrentSpaceshipStage _currentSpcaceshipStage;
        private bool _isPaused;

        private AudioSource _currentAudioSource;

        [Inject]
        private void Construct(CurrentSpaceshipStage currentSpaceshipStage)
        {
            _currentSpcaceshipStage = currentSpaceshipStage;

            _isPaused = false;
            _currentAudioSource = _startStageAudioSource;

            _currentSpcaceshipStage.StageChanged += ChangeAudioClip;
        }

        private void OnDestroy() =>
            _currentSpcaceshipStage.StageChanged -= ChangeAudioClip;

        public void Pause()
        {
            _currentAudioSource.Pause();
            _isPaused = true;
        }

        public void Play()
        {
            if (_isPaused)
            {
                _currentAudioSource.Play();
                _isPaused = false;
            }
        }

        private void ChangeAudioClip(Stage currentStage)
        {
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

            if (targetAudioSource == null)
                return;

            _currentAudioSource.Stop();
            _currentAudioSource = targetAudioSource;
            _currentAudioSource.Play();
            _isPaused = false;
        }

        public class Factory : PlaceholderFactory<string, UniTask<StageMusic>>
        {
        }
    }
}
