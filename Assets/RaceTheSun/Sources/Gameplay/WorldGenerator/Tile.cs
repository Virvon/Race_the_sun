using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class Tile : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string , UniTask<Tile>>
        {
        }
    }
}
