using Assets.RaceTheSun.Sources.Services.WaitingService;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class Plane : MonoBehaviour
    {
        [SerializeField] private float _positionY;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private float _startEffectDelay;

        private Transform _target;
        private IWaitingService _waitingService;

        [Inject]
        private void Construct(Spaceship spaceship, IWaitingService waitingService)
        {
            _target = spaceship.transform;
            _waitingService = waitingService;
        }

        private void Start() =>
            _waitingService.Wait(_startEffectDelay, ShowEffect);

        private void LateUpdate()
        {
            if (_target == null)
                return;

            transform.position = new Vector3(_target.position.x, _positionY, _target.position.z) + _offset;
        }

        public void HideEffect()
        {
            _effect.Stop();
        }

        public void ShowEffect()
        {
            _effect.Play();
        }

        public class Factory : PlaceholderFactory<string, UniTask<Plane>>
        {
        }
    }
}
