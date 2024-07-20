using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public struct CollisionInfo
    {
        public CollisionInfo(float distance, float dot, Vector3 collisionPosition)
        {
            Distance = distance;
            Dot = dot;
            CollisionPosition = collisionPosition;
        }

        public float Distance { get; private set; }
        public float Dot { get; private set; }
        public Vector3 CollisionPosition { get; private set; }
    }
}
