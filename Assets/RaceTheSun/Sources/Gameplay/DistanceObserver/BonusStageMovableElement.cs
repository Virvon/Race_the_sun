using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.DistanceObserver
{
    public class BonusStageMovableElement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxRotationSpeed;
        [SerializeField] private float _minRotationSpeed;
        [SerializeField] private bool _isNotVerticalMoved;
        [SerializeField] private bool _isPushed;

        private float _rotationSpeed;
        private Vector3 _movementDirection;
        private Vector3 _rotationDirection;
        private float _angle;

        private void Start()
        {
            _movementDirection = Random.insideUnitSphere;
            _rotationDirection = Random.insideUnitSphere.normalized;

            if (_isNotVerticalMoved)
                _movementDirection.y = 0;

            float speed = Random.Range(_minSpeed, _maxSpeed);

            _movementDirection = _movementDirection.normalized * speed;
            _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        }

        private void Update()
        {
            _angle += _rotationSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + _movementDirection, Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(_angle, _rotationDirection);
        }
    }
}
