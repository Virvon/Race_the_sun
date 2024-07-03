using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipJump : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Rigidbody _rigidbody;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }
    }
}
