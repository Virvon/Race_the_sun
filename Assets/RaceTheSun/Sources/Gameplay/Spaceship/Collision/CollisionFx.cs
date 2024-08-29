using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Collision
{
    public class CollisionFx : MonoBehaviour
    {
        private const int DestroyDelay = 2;

        private void Start() =>
            Destroy(gameObject, DestroyDelay);

        public class Factory : PlaceholderFactory<string, UniTask<CollisionFx>>
        {
        }
    }
}
