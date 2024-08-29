using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<MainMenu>>
        {
        }
    }
}
