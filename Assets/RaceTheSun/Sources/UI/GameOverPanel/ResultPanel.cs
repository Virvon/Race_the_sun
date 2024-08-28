using Assets.RaceTheSun.Sources.Animations;
using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private ResultScorePanel _resultScorePanel;
        [SerializeField] private GameObject _header;
        [SerializeField] private ResultScoreItems _resultScoreItemsValue;
        [SerializeField] private MultipliplyScoreItemsButton _multipliplyScoreItemsButton;

        private ScoreItemsCounter _scoreItemsCounter;

        public event Action Hided;

        [Inject]
        private void Construct(ScoreItemsCounter scoreItemsCounter)
        {
            _scoreItemsCounter = scoreItemsCounter;
        }

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
            _header.SetActive(true);
            gameObject.SetActive(true);

            _resultScorePanel.ShowResult(callbakc: () =>
            {
                _resultScoreItemsValue.SetScoreItemsValue(_scoreItemsCounter.ScoreItemsPerGame, callback: () =>
                {
                    _multipliplyScoreItemsButton.gameObject.SetActive(true);
                });
            });
        }

        private void Hide()
        {
            _header.SetActive(false);
            gameObject.SetActive(false);
            Hided?.Invoke();
        }
    }
}
