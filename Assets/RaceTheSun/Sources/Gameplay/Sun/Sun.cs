using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Sun
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _startHeight;
        [SerializeField] private float _finishHeight;
        [SerializeField] private float _speed;

        private Transform _spaceship;
        private bool _isShadowed;
        private float _progress;
        private Battery _battery;
        
        [Inject]
        private void Construct(Spaceship.Spaceship spaceship)
        {
            _spaceship = spaceship.transform;
            _isShadowed = false;
            _battery = spaceship.GetComponentInChildren<Battery>();
        }

        private void Update()
        {
            float positionY = GetHeight();
            Move(positionY);
            CheckShadowed();
        }

        private void CheckShadowed()
        {
            bool isShadowed = _isShadowed;

            if (Physics.Raycast(transform.position, (_spaceship.position - transform.position).normalized, out RaycastHit hitInfo, Mathf.Infinity))
                isShadowed = hitInfo.transform.TryGetComponent(out Spaceship.Spaceship _) == false;

            if (isShadowed != _isShadowed)
            {
                _isShadowed = isShadowed;
                _battery.ChangeShadowed(_isShadowed);
            }
        }

        private void Move(float positionY)
        {
            float positionZ = Mathf.Sqrt(Mathf.Pow(_radius, 2) - Mathf.Pow(positionY, 2));

            transform.position = new Vector3(0, positionY, positionZ) + new Vector3(_spaceship.position.x, 2.4f, _spaceship.position.z);
            transform.rotation = Quaternion.LookRotation((new Vector3(_spaceship.position.x, 2.4f, _spaceship.position.z) - transform.position).normalized);
        }

        private float GetHeight()
        {
            if(_progress < 1)
                _progress += Time.deltaTime * _speed;

            return Mathf.Lerp(_startHeight, _finishHeight, _progress);
        }

        public class Factory : PlaceholderFactory<string, UniTask<Sun>>
        {
        }
    }
}
