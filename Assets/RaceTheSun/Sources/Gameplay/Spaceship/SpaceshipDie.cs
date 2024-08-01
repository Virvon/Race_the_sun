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
        [SerializeField] private StartMovement _startMovement;
        [SerializeField] private SpaceshipMovement _spaceshipMovement;

        private int _shieldsCount;
        private Cameras.GameplayCameras _cameras;
        private GameplayStateMachine _gameplayStateMachine;
        private WaitingService _waitingService;

        [Inject]
        private void Construct(Cameras.GameplayCameras cameras, GameplayStateMachine gameplayStateMachine, WaitingService waitingService)
        {
            _cameras = cameras;
            _gameplayStateMachine = gameplayStateMachine;
            _waitingService = waitingService;
        }

        public event Action<int> ShieldsCountChanged;

        public void TryDie()
        {
            if(_shieldsCount > 0)
            {
                _shieldsCount--;
                ShieldsCountChanged?.Invoke(_shieldsCount);
                _startMovement.MoveUp();
            }
            else
            {
                _spaceshipMovement.IsStopped = true;
                _cameras.IncludeCamera(Cameras.GameplayCameraType.SideCamera);
                _waitingService.Wait(2, callback: () => _gameplayStateMachine.Enter<RevivalState>().Forget());
            }
        }

        public void TakeShield()
        {
            _shieldsCount++;
            ShieldsCountChanged?.Invoke(_shieldsCount);
        }
    }
}
