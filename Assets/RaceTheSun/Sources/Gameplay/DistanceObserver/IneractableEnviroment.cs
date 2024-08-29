using Assets.RaceTheSun.Sources.GameLogic.Animations;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.DistanceObserver
{
    public class IneractableEnviroment : MonoBehaviour, IObserver
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _distanceToMove;

        private DistanceObservable _distanceObservable;

        [Inject]
        private void Construct(DistanceObservable distanceObservable)
        {
            _distanceObservable = distanceObservable;
        }

        private void Start()
        {
            _distanceObservable.RegisterObserver(this, new Vector3(transform.position.x, transform.position.y, transform.position.z - _distanceToMove));
        }

        public void Invoke()
        {
            if(_animator != null)
                _animator.SetTrigger(AnimationPath.Move);
        }
    }
}
