using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipTurning : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _turnDuration;
        [SerializeField] private float _maxDeviation;
        
        private SpaceshipModel _model;

        private float _currentTurn;
        private float _targetTurn;
        private Coroutine _turning;

        public float TurnFactor { get; private set; }

        private void FixedUpdate()
        {
            if (_model == null)
                return;

            float horizontal = _playerInput.MoveInput.x;

            if (horizontal != 0)
                horizontal = horizontal > 0 ? 1 : -1;

            Turn(horizontal);
            RotatiModel(_currentTurn);
        }

        private void RotatiModel(float angle)
        {
            _model.transform.rotation = Quaternion.Euler(0, 0, angle * -35);
        }

        public void Init(SpaceshipModel spaceshipModel)
        {
            _model = spaceshipModel;
        }

        public void Reset()
        {
            if (_turning != null)
                StopCoroutine(_turning);

            _currentTurn = 0;
            _targetTurn = 0;
            TurnFactor = 0;
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
