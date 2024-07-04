using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimationCurve _accelerationCurve;
        [SerializeField] private float _extremePointsDuration;
        [SerializeField] private float _maxDeviation;
        [SerializeField] private Transform _model;
        [SerializeField] private float _speed;

        private float _curvePosition = 0;
        private Vector3 _normal;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");

            if(horizontal != 0)
                horizontal = horizontal > 0? 1 : -1;

            Move(horizontal);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out SpaceshipDie _))
                return;

            _normal = collision.contacts[0].normal;
            _rigidbody.velocity = Vector3.zero;
        }

        private void Move(float horizontal)
        {
            if (horizontal == 0 && _curvePosition != 0)
                horizontal = _curvePosition > 0 ? -1 : 1;

            _curvePosition += _extremePointsDuration * Time.deltaTime * horizontal;

            _curvePosition = Mathf.Clamp(_curvePosition, -1, 1);
            float deviation = RoundUp(_accelerationCurve.Evaluate(_curvePosition));

            Vector3 direction = new Vector3(deviation * _maxDeviation, 0,  1);
            Vector3 directionAlongSurface = Project(direction.normalized);
            Vector3 offset = directionAlongSurface * (_speed * Time.deltaTime);

            _rigidbody.MovePosition(_rigidbody.position + offset);
            Rotate(deviation);
        }

        

        private void Rotate(float deviation)
        {
            _model.rotation = Quaternion.Euler(0, 0, deviation * -35);
        }

        private Vector3 Project(Vector3 direction)
        {
            return direction - Vector3.Dot(direction, _normal) * _normal;
        }

        private float RoundUp(float number)
        {
            if (1 - number < 0.05f)
                number = 1;
            else if (1 + number < 0.05f)
                number = -1;
            else if (number < 0.05f && number > -0.05f)
                number = 0;

            return number;
        }
    }
}
