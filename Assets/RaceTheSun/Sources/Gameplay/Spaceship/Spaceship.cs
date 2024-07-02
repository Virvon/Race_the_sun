using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class Spaceship : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<Spaceship>>
        {
        }
    }
}
