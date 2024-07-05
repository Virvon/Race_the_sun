using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipInfoButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SpaceshipInfo _spaceshipInfo;

        public event Action<SpaceshipInfo> Selected;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Selected?.Invoke(_spaceshipInfo);
        }
    }
}