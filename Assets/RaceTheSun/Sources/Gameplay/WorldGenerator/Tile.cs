using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class Tile : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<AssetReferenceGameObject , UniTask<Tile>>
        {
        }
    }
}
