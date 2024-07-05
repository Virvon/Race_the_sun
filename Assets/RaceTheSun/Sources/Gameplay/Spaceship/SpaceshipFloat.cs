using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipFloat : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _floatTime;

        private Coroutine _floating;
        private RigidbodyConstraints _startConstraints;

        private void Start()
        {
            _startConstraints = _rigidbody.constraints;
        }

        public void Float()
        {
            StopFloating();

            _floating = StartCoroutine(Floating());
        }

        public void Stop()
        {
            StopFloating();
            _rigidbody.constraints = _startConstraints;
        }

        private void StopFloating()
        {
            if (_floating != null)
                StopCoroutine(_floating);
        }

        private IEnumerator Floating()
        {
            float elapsedTime = 0;

            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            _rigidbody.freezeRotation = true;

            while(elapsedTime < _floatTime)
            {
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            Stop();
        }
    }
}
