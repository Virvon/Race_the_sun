using System;
using System.Collections;
using UnityEngine;
using Virvon.MyBakery.Services.Input;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement
{
    public class SpaceshipJump : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private float _duration;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private SpaceshipFloat _spaceshipFloat;
        [SerializeField] private Spaceship _spaceship;

        private Coroutine _jumping;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
            JumpBoostsCount = 0;

            _inputService.Jumped += OnJumped;
        }

        public event Action JumpBoostsCountChanged;
        public event Action Jumped;

        public int JumpBoostsCount { get; private set; }
        public Vector3 JumpPosition { get; private set; }
        public bool IsJumped { get; private set; }

        private void OnDestroy() =>
            _inputService.Jumped -= OnJumped;

        public void GiveJumpBoost()
        {
            if (JumpBoostsCount >= _spaceship.AttachmentStats.MaxJumpBoostsCount)
                return;

            JumpBoostsCount++;
            JumpBoostsCountChanged?.Invoke();
        }

        private void OnJumped()
        {
            if (JumpBoostsCount > 0)
            {
                if (_jumping != null)
                {
                    StopCoroutine(_jumping);
                }

                float jumpHeight = IsJumped ? _jumpHeight / 2 : _jumpHeight;

                _spaceshipFloat.Stop();
                _jumping = StartCoroutine(Jumping(jumpHeight));
                JumpBoostsCount--;
                JumpBoostsCountChanged?.Invoke();
                Jumped?.Invoke();
            }
        }

        private IEnumerator Jumping(float jumpHeight)
        {
            float expiredSeconds = 0;
            float progress = 0;

            float startHeight = _rigidbody.position.y;
            float finishHeight = startHeight + jumpHeight;

            IsJumped = true;

            while (progress < 1)
            {
                expiredSeconds += Time.deltaTime;
                progress = expiredSeconds / _duration;
                JumpPosition = new Vector3(transform.position.x, startHeight + _jumpCurve.Evaluate(progress) * finishHeight, transform.position.z);
                yield return null;
            }

            _spaceshipFloat.Float();
            IsJumped = false;
        }
    }
}
