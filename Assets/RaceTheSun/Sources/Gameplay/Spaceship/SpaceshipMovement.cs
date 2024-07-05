using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _turnDuration;
        [SerializeField] private float _maxDeviation;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _model;
        [SerializeField] private SpaceshipJump _spaceshipJump;

        private float _currentTurn;
        private float _targetTurn;
        private Coroutine _turning;
        private Vector3 _normal;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0)
                horizontal = horizontal > 0 ? 1 : -1;

            Move(horizontal);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out SpaceshipDie _))
                return;

            _normal = collision.contacts[0].normal;
        }


        private void Move(float horizontal)
        {
            if (horizontal != _targetTurn)
            {
                float duration;
                _targetTurn = horizontal;

                if(horizontal != 0)
                    duration = _turnDuration + Mathf.Abs(_currentTurn) * _turnDuration;
                else if(horizontal == 0)
                    duration = Mathf.Abs(_currentTurn) * _turnDuration;
                else
                    duration = (1 - Mathf.Abs(_currentTurn)) * _turnDuration;

                if (_turning != null)
                    StopCoroutine(_turning);

                _turning = StartCoroutine(Turning(_targetTurn, duration));
            }

            Vector3 direction = new Vector3(_currentTurn * _maxDeviation, 0, 1);
            Vector3 directionAlongSurface = Project(direction.normalized);
            Vector3 offset = directionAlongSurface * (_speed * Time.deltaTime);

            //Vector3 offset = direction.normalized * (_speed * Time.deltaTime);
            Debug.DrawRay(transform.position, offset * 5);

            Vector3 currentPositon;

            if (_spaceshipJump.JumpPosition != Vector3.zero)
            {
                currentPositon = new Vector3(_rigidbody.position.x, _spaceshipJump.JumpPosition.y, _rigidbody.position.z);
                _normal = Vector3.zero;
            }
            else
            {
                currentPositon = _rigidbody.position;
            }

            _rigidbody.MovePosition(currentPositon + offset);

            Rotate(_currentTurn);
        }

        private Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction, _normal) * _normal;
        }

        private void Rotate(float deviation)
        {
            _model.rotation = Quaternion.Euler(0, 0, deviation * -35);
        }

        private IEnumerator Turning(float targetTurn, float duration)
        {
            float elapsedTime = 0;
            float startTurn = _currentTurn;

            while(_currentTurn != targetTurn)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                _currentTurn = Mathf.Lerp(startTurn, targetTurn, progress);

                yield return null;
            }
        }
    }
}
