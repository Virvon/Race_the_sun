using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipDie : MonoBehaviour
    {
        [SerializeField] private Transform _spaceship;

        private GameStateMachine _gameStateMachine;

        private int _shieldsCount;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public event Action<int> ShieldsCountChanged;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out Wall _))
            {
                if(_shieldsCount > 0)
                {
                    _shieldsCount--;
                    ShieldsCountChanged?.Invoke(_shieldsCount);
                    _spaceship.position = new Vector3(transform.position.x, 50, transform.position.z);
                }
                else
                    _gameStateMachine.Enter<MainMenuState>();
            }
        }

        public void TakeShield()
        {
            _shieldsCount++;
            ShieldsCountChanged?.Invoke(_shieldsCount);
        }
    }
}
