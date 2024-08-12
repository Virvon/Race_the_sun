using System;
using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipJump : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _duration;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private SpaceshipFloat _spaceshipFloat;

        private Coroutine _jumping;
        private int _jumpBoostsCount;

        public event Action<int> JumpBoostsCountChanged;
        public event Action Jumped;

        public Vector3 JumpPosition { get; private set; }
        public bool IsJumped { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _jumpBoostsCount > 0)
            {
                if(_jumping != null)
                {
                    StopCoroutine(_jumping);
                }

                _spaceshipFloat.Stop();
                _jumping = StartCoroutine(Jumping());
                _jumpBoostsCount--;
                JumpBoostsCountChanged?.Invoke(_jumpBoostsCount);
                Jumped?.Invoke();
            }
        }

        public void TakeJumpBoost()
        {
            _jumpBoostsCount++;
        }

        private IEnumerator Jumping()
        {
            float expiredSeconds = 0;
            float progress = 0;

            float startHeight = _rigidbody.position.y;
            float finishHeight = startHeight + _jumpHeight;

            IsJumped = true;

            while (progress < 1)
            {
                expiredSeconds += Time.deltaTime;
                progress = expiredSeconds / _duration;
                JumpPosition = new Vector3(transform.position.x, startHeight + (_jumpCurve.Evaluate(progress) * finishHeight), transform.position.z);
                yield return null;
            }

            _spaceshipFloat.Float();
            IsJumped = false;
        }
    }
}
