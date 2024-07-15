using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class Collision : MonoBehaviour
    {
        [SerializeField] private Vector3 _halfExtents;
        [SerializeField] private SpaceshipMovement _movement;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Spaceship _spaceship;

        public bool IsCollided { get; private set; }
        public float Distance { get; private set; }
        public float Dot { get; private set; }
        public Vector3 HitPosition { get; private set; }

        private void FixedUpdate()
        {
            IsCollided = Physics.BoxCast(_rigidbody.position, _halfExtents, _movement.Offset, out RaycastHit hitInfo, _rigidbody.rotation, Vector3.Distance(_rigidbody.position, _rigidbody.position + _movement.Offset), _layerMask, QueryTriggerInteraction.Ignore);

            if (IsCollided)
            {
                Distance = hitInfo.distance;
                Dot = Vector3.Dot(hitInfo.normal, _movement.Offset.normalized);
                HitPosition = hitInfo.point;
                _spaceship.StopBoostSpeed();
            }
        }

        private void OnDrawGizmos()
        {
            if (_rigidbody == null || _movement == null || _movement.Offset == Vector3.zero)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawCube(_rigidbody.position + _movement.Offset, _halfExtents * 2);
        }
    }
}
