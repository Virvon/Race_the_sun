using System;
using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.WaitingService;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipDie : MonoBehaviour
    {
        private const int EnterResultStateDelay = 2;
        private const float EnterRevivalStateDelay = 0.5f;
        private const float ShowDeathDuration = 0.4f;

        [SerializeField] private Spaceship _spaceship;

        private int _shieldsCount;
        private GameplayCameras _cameras;
        private GameplayStateMachine _gameplayStateMachine;
        private WaitingService _waitingService;
        private StageMusic _stageMusic;

        [Inject(Id = GameplayFactoryInjectId.DestroySound)]
        private SoundPlayer _destroySound;

        private GameplayCameras _gameplayCameras;
        private IGameplayFactory _gameplayFactory;

        [Inject]
        private void Construct(GameplayCameras cameras, GameplayStateMachine gameplayStateMachine, WaitingService waitingService, StageMusic stageMusic, GameplayCameras gameplayCameras, IGameplayFactory gameplayFactory)
        {
            _cameras = cameras;
            _gameplayStateMachine = gameplayStateMachine;
            _waitingService = waitingService;
            _stageMusic = stageMusic;
            _gameplayCameras = gameplayCameras;
            _gameplayFactory = gameplayFactory;
        }

        public event Action Died;
        public event Action Stopped;
        public event Action<int> ShieldsCountChanged;

        public bool TryRevive()
        {
            _spaceship.gameObject.SetActive(false);

            if (_shieldsCount > 0)
            {
                _gameplayFactory.SpaceshipShieldPortal.Activate();
                _shieldsCount--;
                ShieldsCountChanged?.Invoke(_shieldsCount);

                return true;
            }
            else
            {
                _gameplayCameras.SpaceshipMainCamera.Shake();
                _gameplayFactory.Plane.HideEffect();
                _gameplayFactory.Sun.IsStopped = true;
                _destroySound.Play();
                _stageMusic.Pause();
                Died?.Invoke();

                _waitingService.Wait(ShowDeathDuration, callback: ( ) =>
                {
                    _cameras.IncludeCamera(GameplayCameraType.SideCamera);
                    _waitingService.Wait(EnterRevivalStateDelay, callback: () => _gameplayStateMachine.Enter<GameplayRevivalState>().Forget());
                });

                return false;
            }
        }

        public void Stop()
        {
            _gameplayFactory.Plane.HideEffect();
            _gameplayFactory.Sun.IsStopped = true;
            _stageMusic.Pause();
            Stopped?.Invoke();
            _cameras.IncludeCamera(GameplayCameraType.SideCamera);
            _waitingService.Wait(EnterResultStateDelay, callback: () => _gameplayStateMachine.Enter<GameplayResultState>().Forget());
        }

        public void GiveShield()
        {
            if (_shieldsCount >= _spaceship.AttachmentStats.MaxShileldsCount)
                return;

            _shieldsCount++;
            ShieldsCountChanged?.Invoke(_shieldsCount);
        }
    }
}
