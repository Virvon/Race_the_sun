using Assets.RaceTheSun.Sources.Services.TimeScale;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud.Pause
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private ITimeScale _timeScale;

        [Inject]
        private void Construct(ITimeScale timeScale)
        {
            _timeScale = timeScale;

            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _timeScale.Scale(TimeScaleType.Pause);
        }
    }
}
