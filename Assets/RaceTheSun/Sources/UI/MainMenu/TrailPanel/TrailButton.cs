using Assets.RaceTheSun.Sources.Trail;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.TrailPanel
{
    public class TrailButton : MonoBehaviour
    {
        [SerializeField] private TrailType _trailType;
        [SerializeField] private Button _button;

        public event Action<TrailType> Clicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke(_trailType);
        }
    }
}
