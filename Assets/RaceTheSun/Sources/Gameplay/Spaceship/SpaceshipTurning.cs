using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Test
{
    public class SpaceshipTurning : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _turnDuration;
        [SerializeField] private float _maxDeviation;
        [SerializeField] private Transform _model;

        private float _currentTurn;
        private float _targetTurn;
        private Coroutine _turning;

        public float TurnFactor { get; private set; }

        private void FixedUpdate()
        {
            float horizontal = _playerInput.MoveInput.x;

            if (horizontal != 0)
                horizontal = horizontal > 0 ? 1 : -1;

            Turn(horizontal);
            _model.rotation = Quaternion.Euler(0, 0, _currentTurn * -35);
        }

        private void Turn(float horizontal)
        {
            if (horizontal != _targetTurn)
            {
                float duration;
                _targetTurn = horizontal;

                if (horizontal != 0)
                    duration = _turnDuration + Mathf.Abs(_currentTurn) * _turnDuration;
                else if (horizontal == 0)
                    duration = Mathf.Abs(_currentTurn) * _turnDuration;
                else
                    duration = (1 - Mathf.Abs(_currentTurn)) * _turnDuration;

                if (_turning != null)
                    StopCoroutine(_turning);

                _turning = StartCoroutine(Turning(_targetTurn, duration));
            }
        }

        private IEnumerator Turning(float targetTurn, float duration)
        {
            float elapsedTime = 0;
            float startTurn = _currentTurn;

            while (_currentTurn != targetTurn)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                _currentTurn = Mathf.Lerp(startTurn, targetTurn, progress);
                TurnFactor = _currentTurn * _maxDeviation;

                yield return null;
            }
        }
    }
}
