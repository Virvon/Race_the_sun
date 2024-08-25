using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class CollisionFx : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 2);
        }

        public class Factory : PlaceholderFactory<string, UniTask<CollisionFx>>
        {
        }
    }
}
