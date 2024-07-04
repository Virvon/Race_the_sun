using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipJump : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Rigidbody _rigidbody;

        private int _jumpBoostsCount;

        public event Action<int> JumpBoostsCountChanged;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _jumpBoostsCount > 0)
            {
                Jump();
                _jumpBoostsCount--;
                JumpBoostsCountChanged?.Invoke(_jumpBoostsCount);
            }
        }

        public void TakeJumpBoost()
        {
            _jumpBoostsCount++;
            JumpBoostsCountChanged?.Invoke(_jumpBoostsCount);
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }
}
