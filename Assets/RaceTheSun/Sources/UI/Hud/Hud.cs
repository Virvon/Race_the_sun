using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class Hud : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<Hud>>
        {
        }
    }
}
