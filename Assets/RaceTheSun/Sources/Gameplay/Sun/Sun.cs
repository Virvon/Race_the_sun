using System.Collections;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Battery;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Sun
{
    public class Sun : MonoBehaviour
    {
        private const float SpaceshipPositionY = 6.2f;
        [SerializeField] private float _radius;
        [SerializeField] private float _startHeight;
        [SerializeField] private float _finishHeight;
        [SerializeField] private float _downMoveSpeed;
        [SerializeField] private float _upMoveSpeed;
        [SerializeField] private float _hidedHeight;
        [SerializeField] private float _cutSceneHeaght;
        [SerializeField] private float _cutSceneDuration;

        public bool IsStopped;

        private Transform _spaceship;
        private bool _isShadowed;
        private float _progress;
        private Battery _battery;
        private bool _isMovedDown;
        private bool _isHided;
        private float _positionY;

        [Inject]
        private void Construct(Spaceship.Spaceship spaceship)
        {
            _spaceship = spaceship.transform;
            _isShadowed = false;
            _battery = _spaceship.GetComponentInChildren<Battery>();
            _isMovedDown = true;
            _isHided = false;
            IsStopped = true;
            _positionY = _cutSceneHeaght;
        }

        private void Update()
        {
            if (_isHided || IsStopped)
                return;

            _positionY = GetHeight();
            Move(_positionY);
            CheckShadowed();
        }

        public void Restart()
        {
            _isHided = false;
            Move(_startHeight);
        }

        public void Show() =>
            StartCoroutine(CutScene());

        public void SetMovementDirection(bool isMovedDown) =>
            _isMovedDown = isMovedDown;

        public void Hide()
        {
            _isHided = true;
            Move(_hidedHeight);
            _battery.ChangeShadowed(false);
        }

        private void CheckShadowed()
        {
            bool isShadowed = _isShadowed;

            if (Physics.Raycast(
                transform.position,
                (_spaceship.position - transform.position).normalized,
                out RaycastHit hitInfo,
                Mathf.Infinity))
            {
                isShadowed = hitInfo.transform.TryGetComponent(out Spaceship.Spaceship _) == false;
            }

            if (isShadowed != _isShadowed)
            {
                _isShadowed = isShadowed;
                _battery.ChangeShadowed(_isShadowed);
            }
        }

        private void Move(float positionY)
        {
            float positionZ = Mathf.Sqrt(Mathf.Pow(_radius, 2) - Mathf.Pow(positionY, 2));

            transform.position = new Vector3(0, positionY, positionZ) + new Vector3(_spaceship.position.x, SpaceshipPositionY, _spaceship.position.z);

            transform.rotation = Quaternion.LookRotation((new Vector3(_spaceship.position.x, SpaceshipPositionY, _spaceship.position.z) - transform.position).normalized);
        }

        private float GetHeight()
        {
            if ((_progress < 1 && _isMovedDown) || (_progress > 0 && _isMovedDown == false))
            {
                float movementSpeed = _isMovedDown ? _downMoveSpeed : _upMoveSpeed * -1;
                _progress += Time.deltaTime * movementSpeed;
            }

            return Mathf.Lerp(_startHeight, _finishHeight, _progress);
        }

        private IEnumerator CutScene()
        {
            float passedTime = 0;
            float progress;

            while (_positionY != _startHeight)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _cutSceneDuration;

                _positionY = Mathf.Lerp(_cutSceneHeaght, _startHeight, progress);
                Move(_positionY);

                yield return null;
            }

            IsStopped = false;
        }

        public class Factory : PlaceholderFactory<string, UniTask<Sun>>
        {
        }
    }
}
