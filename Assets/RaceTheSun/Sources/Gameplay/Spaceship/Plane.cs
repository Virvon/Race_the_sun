using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class Plane : MonoBehaviour
    {
        [SerializeField] private float _positionY;
        [SerializeField] private Vector3 _offset;

        private Transform _target;

        [Inject]
        private void Construct(Spaceship spaceship)
        {
            _target = spaceship.transform;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            transform.position = new Vector3(_target.position.x, _positionY, _target.position.z) + _offset;
        }

        public class Factory : PlaceholderFactory<string, UniTask<Plane>>
        {
        }
    }
}
