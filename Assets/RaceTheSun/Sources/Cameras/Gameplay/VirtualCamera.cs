using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Cameras
{
    public abstract class VirtualCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private CinemachineBlendDefinition _blendDefinition;

        public CinemachineVirtualCamera CinemachineVirtualCamera => _cinemachineVirtualCamera;
        public CinemachineBlendDefinition BlendDefinition => _blendDefinition;

        public class Factory : PlaceholderFactory<string, UniTask<VirtualCamera>>
        {
        }
    }
}
