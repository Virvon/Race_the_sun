using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Portals
{
    public class ShieldPortal : MonoBehaviour
    {
        [SerializeField] private float _destroyDelay;

        private void Start() =>
            Destroy(gameObject, _destroyDelay);

        public class Factory : PlaceholderFactory<string, UniTask<ShieldPortal>>
        {
        }
    }
}
