using Assets.RaceTheSun.Sources.Animations;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private GameOverPanelAnimationElement _gameOverPanelAnimationElement;
        [SerializeField] private Button _continueButton;

        public event Action Hided;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(Hide); 
        }

        public void Open()
        {
            _gameOverPanelAnimationElement.Open();
        }

        private void Hide()
        {
            _gameOverPanelAnimationElement.Hided += ()=> Hided?.Invoke();
            _gameOverPanelAnimationElement.Hide();
        }
    }
}
