using Agava.YandexGames;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class ShopWindow : OpenableWindow
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _reward;
        [SerializeField] private TMP_Text _rewardValue;

        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;

            _rewardValue.text = _reward.ToString();

            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnButtonClick()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            InterstitialAd.Show(onCloseCallback: (_) =>
            {
                _persistentProgressService.Progress.Wallet.Give(_reward);
                _saveLoadService.SaveProgress();
            });
#else
            _persistentProgressService.Progress.Wallet.Give(_reward);
            _saveLoadService.SaveProgress();
#endif
        }
    }
}