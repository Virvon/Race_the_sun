using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SpaceshipType _spaceshipType;

        public event Action<SpaceshipType> Clicked;

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
            Clicked?.Invoke(_spaceshipType);
        }
    }
}