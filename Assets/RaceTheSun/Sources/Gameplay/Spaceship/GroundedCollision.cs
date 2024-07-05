using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class GroundedCollision : MonoBehaviour
    {
        [SerializeField] private SpaceshipJump _spaceshipJump;

        public Vector3 Normal { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            Normal = collision.contacts[0].normal;
        }

        private void OnEnable()
        {
            _spaceshipJump.Jumped += OnJumped;
        }

        private void OnDisable()
        {
            _spaceshipJump.Jumped -= OnJumped;
        }

        private void OnJumped()
        {
            Normal = Vector3.zero;
        }
    }
}
