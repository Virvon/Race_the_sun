using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.AssetManagement
{
    public class RefefencePrefabFactory<TComponent> : IFactory<AssetReferenceGameObject, UniTask<TComponent>>
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;

        public RefefencePrefabFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<TComponent> Create(AssetReferenceGameObject assetReference)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(assetReference);
            GameObject newObject = _instantiator.InstantiatePrefab(prefab);
            return newObject.GetComponent<TComponent>();
        }

    }
}