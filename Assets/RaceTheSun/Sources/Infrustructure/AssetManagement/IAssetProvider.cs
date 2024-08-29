using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Infrustructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<TAsset> Load<TAsset>(string key) where TAsset : class;
        UniTask<TAsset> Load<TAsset>(AssetReferenceGameObject reference) where TAsset : class;
        UniTask InitializeAsync();
        UniTask WarmupAssetsByLable(string label);
        UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;
        UniTask<List<string>> GetAssetsListByLabel<TAsset>(string label);
        void CleanUp();
        UniTask<TAsset> Load<TAsset>(AssetReference reference);
    }
}