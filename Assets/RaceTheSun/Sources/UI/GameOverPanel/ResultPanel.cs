using Assets.RaceTheSun.Sources.Animations;
using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private GameOverPanelAnimationElement _gameOverPanelAnimationElement;
        [SerializeField] private Button _continueButton;
        [SerializeField] private ResultScorePanel _resultScorePanel;
        [SerializeField] private TMP_Text _scoreItemsValue;

        private ScoreItemsCounter _scoreItemsCounter;

        public event Action Hided;

        [Inject]
        private void Construct(ScoreItemsCounter scoreItemsCounter)
        {
            _scoreItemsCounter = scoreItemsCounter;
        }

        private void OnEnable()
        {
            Debug.Log("enable");
            _gameOverPanelAnimationElement.Opened += OnOpened;
            _continueButton.onClick.AddListener(Hide);
        }
      
        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(Hide); 
            _gameOverPanelAnimationElement.Opened -= OnOpened;
        }

        public void Open()
        {
            _gameOverPanelAnimationElement.Open();
            _scoreItemsValue.text = _scoreItemsCounter.ScoreItemsPerGame.ToString();
        }

        private void Hide()
        {
            _gameOverPanelAnimationElement.Hided += ()=> Hided?.Invoke();
            _gameOverPanelAnimationElement.Hide();
        }

        private void OnOpened()
        {
            Debug.Log("OnOpened");
            _resultScorePanel.ShowResult();
        }
    }
}
