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
        [SerializeField] private SpaceshipJump _jump;
        [SerializeField] private float _collisionForceMultiplier;
        [SerializeField] private float _collisionForceDuration;
        [SerializeField] private Spaceship _spaceship;
        [SerializeField] private Vector3 _halfExtents;

        public bool IsStopped;

        private Vector3 _surfaceNormal;
        private bool _isFlight;
        private float _flightTime;
        private Vector3 _startSurfaceNormal;
        private bool _isBounced;
        private float _forceTime;
        private CollisionChecker _collisionChecker;
        private CollisionInfo _collisionInfo;
        private Vector3 _bounceDirection;

        public Vector3 Offset { get; private set; }
        public bool IsCollided { get; private set; }
        public CollisionInfo CollisionInfo => _collisionInfo;

        private void Start()
        {
            _isBounced = false;
            IsStopped = false;

            _collisionChecker = new CollisionChecker(_halfExtents, _rigidbody, _layerMask);
        }

        private void FixedUpdate()
        {
            if (IsStopped)
                return;

            TryFly();
            CheckStartOfBounced();

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

            if (_rigidbody == null || Offset == Vector3.zero)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawCube(_rigidbody.position + Offset, _halfExtents * 2);

        }

        private void CheckStartOfBounced()
        {
            IsCollided = _collisionChecker.CheckCollision(Offset, out _collisionInfo);

            if (IsCollided && _isBounced == false)
            {
                _forceTime = 0;
                _isBounced = true;
                _spaceship.StopBoostSpeed();
                _bounceDirection = _rigidbody.transform.position - _collisionInfo.CollisionPosition;
                _bounceDirection = new Vector3(_bounceDirection.x, 0, 0).normalized;
            }
        }

        private Vector3 AdjustOffsetLengthToCollision(Vector3 offset)
        {
            if (IsCollided && offset.magnitude > _collisionInfo.Distance)
                offset = offset.normalized * _collisionInfo.Distance;

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

            if (_isBounced)
            {
                direction = (Vector3.forward + (_bounceDirection * _collisionForceMultiplier)).normalized;
                _forceTime += Time.fixedDeltaTime;
                if (_forceTime > _collisionForceDuration)
                    _isBounced = false;
            }
            else
            {
                direction = (Vector3.forward + new Vector3(_turning.TurnFactor, 0, 0)).normalized;
            }

            Vector3 directionAlongSurface = Project(direction);
            Vector3 offset = directionAlongSurface * _spaceship.Speed * Time.fixedDeltaTime;
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
