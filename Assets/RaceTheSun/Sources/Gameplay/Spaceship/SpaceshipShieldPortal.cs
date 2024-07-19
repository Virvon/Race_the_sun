using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipShieldPortal : MonoBehaviour 
    {
        [SerializeField] private float _shieldPortalHeight;

        public void Activate(CollisionPortalPoint collisionPortalPoint)
        {
            transform.position = new Vector3(collisionPortalPoint.transform.position.x, _shieldPortalHeight, collisionPortalPoint.transform.position.z);
        }

        public class Factory : PlaceholderFactory<string, UniTask<SpaceshipShieldPortal>>
        {
        }
    }
}
