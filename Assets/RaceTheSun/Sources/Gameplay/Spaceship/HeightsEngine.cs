using Assets.RaceTheSun.Sources.Utils;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Test
{
    public class HeightsEngine : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _altitude;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxForce;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _damping;

        private float _springSpeed;
        private float _lastDistance;

        private void FixedUpdate()
        {
            Lift();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * _maxDistance);
        }

        private void Lift()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
            {
                float distance = hitInfo.distance;

                _springSpeed = (distance - _lastDistance) * Time.fixedDeltaTime;
                _springSpeed = Mathf.Max(_springSpeed, 0);
                _lastDistance = distance;

                float minForceHeight = _altitude + 1f;
                float maxForceHeight = _altitude - 1f;
                distance = Mathf.Clamp(distance, maxForceHeight, minForceHeight);

                float forceFactor = distance.Remap(maxForceHeight, minForceHeight, _maxForce, 0);
                _rigidbody.AddForce(-transform.forward * Mathf.Max(forceFactor - _springSpeed * _maxForce * _damping, 0), ForceMode.Force);
            }
        }
    }
}
