using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu.Model
{
    public class TrailPoint : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<TrailPoint>>
        {

        }
    }
}