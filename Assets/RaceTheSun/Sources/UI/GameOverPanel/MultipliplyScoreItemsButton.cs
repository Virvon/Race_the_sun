using Agava.YandexGames;
using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class MultipliplyScoreItemsButton : MonoBehaviour
    {
        private const int MinReward = 20;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _rewardValue;
        [SerializeField] private ResultScoreItems _resultScoreItems;

        private IPersistentProgressService _persistentProgressService;
        private ScoreItemsCounter _scoreItemsCounter;
        private int _reward;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, ScoreItemsCounter scoreItemsCounter)
        {
            _persistentProgressService = persistentProgressService;
            _scoreItemsCounter = scoreItemsCounter;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);

            _reward = GetReward();
            _rewardValue.text = _reward.ToString();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private int GetReward()
        {
            if (_scoreItemsCounter.ScoreItemsPerGame < 10)
                return MinReward;
            else
                return _scoreItemsCounter.ScoreItemsPerGame;
        }

        private void OnButtonClick()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            InterstitialAd.Show(onCloseCallback: (_) =>
            {
                 _persistentProgressService.Progress.Wallet.Give(_reward);
            _resultScoreItems.SetScoreItemsValue(_reward + _scoreItemsCounter.ScoreItemsPerGame);
            gameObject.SetActive(false);
            });
#else
            _persistentProgressService.Progress.Wallet.Give(_reward);
            _resultScoreItems.SetScoreItemsValue(_reward + _scoreItemsCounter.ScoreItemsPerGame);
            gameObject.SetActive(false);
#endif
            
        }
    }
}
 