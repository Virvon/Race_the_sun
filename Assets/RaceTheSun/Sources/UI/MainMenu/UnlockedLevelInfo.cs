using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Services.SaveLoad;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class UnlockedLevelInfo : MonoBehaviour
    {
        private const string LevelName = "ОТКРЫТ УРОВЕНЬ";

        [SerializeField] private TMP_Text _levelInfo;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _subtitle;
        [SerializeField] private Button _continueButton;

        private IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;
        private IAssetProvider _assetProvider;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(
            IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService,
            IAssetProvider assetProvider,
            ISaveLoadService saveLoadService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
            _continueButton.onClick.AddListener(OnContinueButtonClicked);

            CheackUnlockedLevel();
        }

        private void OnDestroy() =>
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);

        private void OnContinueButtonClicked() =>
            CheackUnlockedLevel();

        private async void CheackUnlockedLevel()
        {
            if (_persistentProgressService
                .Progress
                .LevelProgress
                .LastShowedLevel < _persistentProgressService
                .Progress
                .LevelProgress
                .Level)
            {
                _persistentProgressService.Progress.LevelProgress.LastShowedLevel++;
                LevelUnclockInfoConfig levelUnclockInfoConfig = _staticDataService
                    .GetLevelUnlockInfo(_persistentProgressService.Progress.LevelProgress.LastShowedLevel);

                _levelInfo.text = $"{LevelName} {_persistentProgressService.Progress.LevelProgress.LastShowedLevel}";
                _title.text = levelUnclockInfoConfig.Title;
                _subtitle.text = levelUnclockInfoConfig.Subtitle;
                _icon.sprite = await _assetProvider.Load<Sprite>(levelUnclockInfoConfig.IconReference);

                gameObject.SetActive(true);

                if (levelUnclockInfoConfig.NeedReward)
                    _persistentProgressService.Progress.Wallet.Give(100);

                _saveLoadService.SaveProgress();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}