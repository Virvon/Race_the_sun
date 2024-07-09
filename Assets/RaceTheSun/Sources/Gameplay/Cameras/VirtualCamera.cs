using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public class VirtualCamera : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<VirtualCamera>>
        {
        }
    }
}
