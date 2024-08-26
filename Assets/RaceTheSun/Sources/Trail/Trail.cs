using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Trail
{
    public class Trail : MonoBehaviour
    {
        private void Start()
        {
            transform.localPosition = Vector3.zero;
        }

        public class Factory : PlaceholderFactory<AssetReferenceGameObject, UniTask<Trail>>
        {
        }
    }
}
