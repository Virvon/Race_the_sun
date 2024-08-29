using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.WaitingService;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipDie : MonoBehaviour
    {
        [SerializeField] private Spaceship _spaceship;

        private int _shieldsCount;
        private GameplayCameras _cameras;
        private GameplayStateMachine _gameplayStateMachine;
        private WaitingService _waitingService;
        private StageMusic _stageMusic;

        [Inject(Id = GameplayFactoryInjectId.DestroySound)]
        private SoundPlayer _destroySound;

        private GameplayCameras _gameplayCameras;
        private Sun.Sun _sun;

        private SpaceshipShieldPortal _spaceshipShieldPortal;
        private Plane _plane;

        public event Action Died;
        public event Action Stopped;

        [Inject]
        private void Construct(GameplayCameras cameras, GameplayStateMachine gameplayStateMachine, WaitingService waitingService, StageMusic stageMusic, GameplayCameras gameplayCameras)
        {
            _cameras = cameras;
            _gameplayStateMachine = gameplayStateMachine;
            _waitingService = waitingService;
            _stageMusic = stageMusic;
            _gameplayCameras = gameplayCameras;
        }

        public event Action<int> ShieldsCountChanged;

        public void Init(SpaceshipShieldPortal spaceshipShieldPortal)
        {
            _spaceshipShieldPortal = spaceshipShieldPortal;
        }

        public void Init(Sun.Sun sun)
        {
            _sun = sun;
        }

        public void Init(Plane plane)
        {
            _plane = plane;
        }

        public bool TryRevive()
        {
            _spaceship.gameObject.SetActive(false);
            
            if(_shieldsCount > 0)
            {
                _spaceshipShieldPortal.Activate();
                _shieldsCount--;
                ShieldsCountChanged?.Invoke(_shieldsCount);
                return true;
            }
            else
            {
                _gameplayCameras.SpaceshipMainCamera.Shake();
                _plane.HideEffect();
                _sun.IsStopped = true;
                _destroySound.Play();
                _stageMusic.Pause();
                Died?.Invoke();

                _waitingService.Wait(0.4f, callback: ()=>{
                    _cameras.IncludeCamera(GameplayCameraType.SideCamera);
                    _waitingService.Wait(0.5f, callback: () => _gameplayStateMachine.Enter<RevivalState>().Forget());
                });
               
                return false;
            }
        }

        public void Stop()
        {
            _plane.HideEffect();
            _sun.IsStopped = true;
            _stageMusic.Pause();
            Stopped?.Invoke();
            _cameras.IncludeCamera(GameplayCameraType.SideCamera);
            _waitingService.Wait(2, callback: () => _gameplayStateMachine.Enter<ResultState>().Forget());
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
