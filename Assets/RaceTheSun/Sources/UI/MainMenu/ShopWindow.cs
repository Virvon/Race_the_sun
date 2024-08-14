using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class ShopWindow : OpenableWindow
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _reward;

        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;

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
                _persistentProgressService.Progress.Wallet.Take(_reward);
            });
#else
            _persistentProgressService.Progress.Wallet.Take(_reward);
#endif
        }
    }
}