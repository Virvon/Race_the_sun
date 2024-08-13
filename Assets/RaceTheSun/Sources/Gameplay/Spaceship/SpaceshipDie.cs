using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
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
        private Cameras.GameplayCameras _cameras;
        private GameplayStateMachine _gameplayStateMachine;
        private WaitingService _waitingService;
        private SpaceshipShieldPortal _spaceshipShieldPortal;

        public event Action Died;

        [Inject]
        private void Construct(Cameras.GameplayCameras cameras, GameplayStateMachine gameplayStateMachine, WaitingService waitingService)
        {
            _cameras = cameras;
            _gameplayStateMachine = gameplayStateMachine;
            _waitingService = waitingService;
        }

        public event Action<int> ShieldsCountChanged;

        public void Init(SpaceshipShieldPortal spaceshipShieldPortal)
        {
            _spaceshipShieldPortal = spaceshipShieldPortal;
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
                Debug.Log("died");
                Died?.Invoke();
                _cameras.IncludeCamera(Cameras.GameplayCameraType.SideCamera);
                _waitingService.Wait(2, callback: () => _gameplayStateMachine.Enter<RevivalState>().Forget());
                return false;
            }
        }

        public void GiveShield()
        {
            _shieldsCount++;
            ShieldsCountChanged?.Invoke(_shieldsCount);
        }
    }
}
