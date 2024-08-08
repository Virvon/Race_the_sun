using Assets.RaceTheSun.Sources.Utils;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class HeightsEngine : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _altitude;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxForce;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _damping;
        [SerializeField] private Vector3 _halfExtence;

        private float _springSpeed;
        private float _lastDistance;

        private void FixedUpdate()
        {
            Lift();
        }

        private void Lift()
        {//Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore)
            if (Physics.BoxCast(_rigidbody.position, _halfExtence, transform.forward, out RaycastHit hitInfo, Quaternion.identity, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
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
