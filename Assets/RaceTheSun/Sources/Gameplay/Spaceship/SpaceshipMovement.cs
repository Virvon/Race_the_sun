using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _maxDistance;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _minSpeed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _flightDistance;
        [SerializeField] private float _flightDuration;
        [SerializeField] private SpaceshipTurning _turning;
        [SerializeField] private Collision _collision;
        [SerializeField] private SpaceshipJump _jump;
        [SerializeField] private float _collisionForceMultiplier;
        [SerializeField] private float _collisionForceDuration;
        [SerializeField] private Spaceship _spaceship;

        public bool IsStopped;

        private Vector3 _surfaceNormal;
        private bool _isFlight;
        private float _flightTime;
        private Vector3 _startSurfaceNormal;
        private bool _isCollided;
        private float _forceTime;
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        private void Construct(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        public Vector3 Offset { get; private set; }

        private void Start()
        {
            _isCollided = false;
            IsStopped = false;
        }

        private void FixedUpdate()
        {
            if (IsStopped)
                return;

            TryFly();
            CheckStartOfCollision();

            Offset = GetOffset();
            Offset = AdjustOffsetToJump(Offset);
            Offset = AdjustOffsetLengthToCollision(Offset);

            _rigidbody.MovePosition(_rigidbody.position + Offset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
            Gizmos.DrawLine(transform.position, transform.position - transform.up * _maxDistance);

            if (Offset == Vector3.zero)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + Offset, 0.3f);
            Gizmos.DrawLine(transform.position, transform.position + Offset);
        }

        private void CheckStartOfCollision()
        {
            if (_collision.IsCollided && _isCollided == false)
            {
                _forceTime = 0;
                _isCollided = true;
            }
        }

        private Vector3 AdjustOffsetLengthToCollision(Vector3 offset)
        {
            if (_collision.IsCollided && offset.magnitude > _collision.Distance)
                offset = offset.normalized * _collision.Distance;

            return offset;
        }

        private Vector3 AdjustOffsetToJump(Vector3 offset)
        {
            if (_jump.IsJumped)
                offset = new Vector3(offset.x, _jump.JumpPosition.y - _rigidbody.position.y, offset.z);

            return offset;
        }

        private Vector3 GetOffset()
        {
            Vector3 direction;

            if (_isCollided)
            {
                Vector3 forceDirection = _rigidbody.transform.position - _collision.HitPosition;
                forceDirection = new Vector3(forceDirection.x, 0, 0).normalized;
                direction = (Vector3.forward + (forceDirection * _collisionForceMultiplier)).normalized;

                _forceTime += Time.fixedDeltaTime;

                if (_forceTime > _collisionForceDuration)
                    _isCollided = false;
            }
            else
            {
                direction = (Vector3.forward + new Vector3(_turning.TurnFactor, 0, 0)).normalized;
            }

            Vector3 directionAlongSurface = Project(direction);
            Vector3 offset = directionAlongSurface * _spaceship.SpeedProvider.GetSpeed() * Time.fixedDeltaTime;
            return offset;
        }

        private void TryFly()
        {
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hitInfo.distance >= _flightDistance && _isFlight == false)
                {
                    SetFlightSettings();
                }
                else if (hitInfo.distance < _flightDistance)
                {
                    _surfaceNormal = hitInfo.normal;
                    _isFlight = false;
                }
            }
            else if (_isFlight == false)
            {
                SetFlightSettings();
            }

            if (_isFlight)
            {
                _flightTime += Time.fixedDeltaTime;
                float progress = _flightTime / _flightDuration;
                _surfaceNormal = Vector3.Lerp(_startSurfaceNormal, Vector3.up, progress);
            }
        }

        private void SetFlightSettings()
        {
            _isFlight = true;
            _flightTime = 0;
            _startSurfaceNormal = _surfaceNormal;
        }

        private Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction, _surfaceNormal) * _surfaceNormal;
        }
    }
}
