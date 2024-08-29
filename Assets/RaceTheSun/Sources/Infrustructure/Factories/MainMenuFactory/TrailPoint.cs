using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrustructure.Factories.MainMenuFactory
{
    public class TrailPoint : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<TrailPoint>>
        {

        }
    }
}