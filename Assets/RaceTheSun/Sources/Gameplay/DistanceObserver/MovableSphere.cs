using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.DistanceObserver
{
    public class MovableSphere : MonoBehaviour, IObserver
    {
        [SerializeField] private float _distanceToMove;
        [SerializeField] private Vector3 _movementDirection;
        [SerializeField] private float _speed;
        [SerializeField] private float _movementDistance;

        private DistanceObservable _distanceObservable;
        private Vector3 _startPosition;
        private bool _canMove;

        [Inject]
        private void Construct(DistanceObservable distanceObservable)
        {
            _distanceObservable = distanceObservable;
            _canMove = false;
        }

        private void Start()
        {
            _startPosition = transform.position;
            _distanceObservable.RegisterObserver(
                this,
                new Vector3(transform.position.x, transform.position.y, transform.position.z - _distanceToMove));
        }

        private void Update()
        {
            if (_canMove == false)
                return;

            transform.position = Vector3.MoveTowards(
                transform.position, transform.position + _movementDirection.normalized,
                _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _startPosition) >= _movementDistance)
                _canMove = false;
        }

        public void Invoke() =>
            _canMove = true;
    }
}
