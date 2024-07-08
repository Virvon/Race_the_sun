using System;
using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Test
{
    public class SpaceshipMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _maxDistance;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _speed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _flightDistance;
        [SerializeField] private float _flightDuration;
        [SerializeField] private SpaceshipTurning _turning;
        [SerializeField] private Collision _collision;
        [SerializeField] private float _destoryDot;
        [SerializeField] private float _accelerationFromMinSpeed;
        [SerializeField] private SpaceshipJump _jump;
        [SerializeField] private float _collisionForceMultiplier;
        [SerializeField] private float _collisionForceDuration;

        private Vector3 _surfaceNormal;
        private bool _isFlight;
        private float _flightTime;
        private Vector3 _startSurfaceNormal;
        private float _currentSpeed;
        private bool _isCollided;
        private float _forceTime;

        public Vector3 Offset { get; private set; }

        private void Start()
        {
            _currentSpeed = _speed;
            _isCollided = false;
        }

        private void FixedUpdate()
        {
            TryNormalizeSpeed();
            TryFly();
            CheckStartOfCollision();

            Offset = GetOffset();
            Offset = AdjustOffsetToJump(Offset);
            Offset = AdjustOffsetLengthToCollision(Offset);

            _rigidbody.MovePosition(_rigidbody.position + Offset);

            SetCurrentSpeed();
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

        private void SetCurrentSpeed()
        {
            if (_collision.IsCollided)
            {
                if (_collision.Dot > _destoryDot)
                    _currentSpeed = _minSpeed;
                else
                    _currentSpeed = 0;
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
            Vector3 offset = directionAlongSurface * _currentSpeed * Time.fixedDeltaTime;
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

        private void TryNormalizeSpeed()
        {
            if (_currentSpeed < _speed)
            {
                float speedFactor = _accelerationFromMinSpeed * Time.deltaTime;

                if (_currentSpeed + speedFactor > _speed)
                    _currentSpeed = _speed;
                else
                    _currentSpeed += speedFactor;
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
