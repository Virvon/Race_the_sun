using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Collision
{
    public class CollisionChecker
    {
        private readonly Vector3 _halfExtents;
        private readonly Rigidbody _rigidbody;
        private readonly LayerMask _layerMask;

        public CollisionChecker(Vector3 halfExtents, Rigidbody rigidbody, LayerMask layerMask)
        {
            _halfExtents = halfExtents;
            _rigidbody = rigidbody;
            _layerMask = layerMask;
        }

        public bool CheckCollision(Vector3 offset, out CollisionInfo collidoinInfo)
        {
            collidoinInfo = new CollisionInfo();

            float castDistance = Vector3.Distance(_rigidbody.position, _rigidbody.position + offset);

            bool isCollided = Physics.BoxCast(
                _rigidbody.position,
                _halfExtents, offset,
                out RaycastHit hitInfo,
                _rigidbody.rotation,
                castDistance,
                _layerMask,
                QueryTriggerInteraction.Ignore);

            if (isCollided)
            {
                float dot = hitInfo.collider.transform
                    .TryGetComponent(out ImpassableEnvironment _) ? -1 : Vector3.Dot(hitInfo.normal, offset.normalized);

                collidoinInfo = new CollisionInfo(hitInfo.distance, dot, hitInfo.point);
            }

            return isCollided;
        }
    }
}
