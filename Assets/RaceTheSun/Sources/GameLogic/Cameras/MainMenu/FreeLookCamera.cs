using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu
{
    public abstract class FreeLookCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
        [SerializeField] private float _yAxisValue;
        [SerializeField] private float _xAxisValue;

        public CinemachineFreeLook CinemachineFreeLook => _cinemachineFreeLook;

        public void ResetPosition()
        {
            _cinemachineFreeLook.m_YAxis.Value = _yAxisValue;
            _cinemachineFreeLook.m_XAxis.Value = _xAxisValue;
        }

        public class Factory : PlaceholderFactory<string, UniTask<FreeLookCamera>>
        {
        }
    }
}
