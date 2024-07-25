using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class GameOverPanel : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<GameOverPanel>>
        {
        }
    }
}
