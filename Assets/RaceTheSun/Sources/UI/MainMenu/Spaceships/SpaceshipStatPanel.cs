using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipStatPanel : MonoBehaviour
    {
        private const int StartUpgradeCost = 1000;
        private const string MaxLevelText = "МАКС. УРОВЕНЬ";

        [SerializeField] private Button _upgradeButton;
        [SerializeField] private StatType _statType;
        [SerializeField] private MPUIKIT.MPImage _progressbarValue;
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private TMP_Text _upgradeCosteValue;
        [SerializeField] private GameObject _icon;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;

        private IPersistentProgressService _persistentProgress;
        private IStaticDataService _staticDataService;
        private ISaveLoadService _saveLoadService;
        private SpaceshipType _currentSpaceship;

        public event Action Updated;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress, IStaticDataService staticDataService,ISaveLoadService saveLoadService)
        {
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;

            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        }

        public int UpgradeCost => _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Level * StartUpgradeCost;
        public StatType StatType => _statType;

        private void OnDestroy()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        }

        public void ResetSpaceship(SpaceshipType type)
        {
            _currentSpaceship = type;

            float currentStatLevel = _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Level;
            float maxStatLevel = _staticDataService.GetSpaceship(_currentSpaceship).GetStat(_statType).MaxLevel;

            _progressbarValue.fillAmount = (currentStatLevel - 1) / (maxStatLevel - 1);
            _upgradeCosteValue.text = UpgradeCost.ToString();

            _blockPanel.SetActive(_persistentProgress.Progress.AvailableStatsToUpgrade.CheckAvailability(_statType) == false);
            _upgradeButton.interactable = _persistentProgress.Progress.AvailableStatsToUpgrade.CheckAvailability(_statType) && _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(type).IsUnlocked;

            if (_persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Level >= _staticDataService.GetSpaceship(_currentSpaceship).GetStat(_statType).MaxLevel)
            {
                _upgradeButton.interactable = false;
                _upgradeCosteValue.text = MaxLevelText;
                _icon.SetActive(false);
            }

            Updated?.Invoke();
        }

        private void OnUpgradeButtonClicked()
        {
            if (_persistentProgress.Progress.Wallet.TryTake(UpgradeCost))
            {
                _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).Level++;
                _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Level++;
                _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Value += _staticDataService.GetSpaceship(_currentSpaceship).GetStat(_statType).UpgradeValue;
                _saveLoadService.SaveProgress();
                _currentClickedSpaceshipInfo.UpdateLevel();
                ResetSpaceship(_currentSpaceship);
            }
        }
    }
}