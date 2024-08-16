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

        [SerializeField] private Button _upgradeButton;
        [SerializeField] private StatType _statType;
        [SerializeField] private MPUIKIT.MPImage _progressbarValue;
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private TMP_Text _upgradeCosteValue;

        private IPersistentProgressService _persistentProgress;
        private IStaticDataService _staticDataService;

        private SpaceshipType _currentSpaceship;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress, IStaticDataService staticDataService)
        {
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;

            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        }

        public int UpgradeCost => _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Level * StartUpgradeCost;

        private void OnDestroy()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        }

        public void ResetSpaceship(SpaceshipType type)
        {
            _currentSpaceship = type;

            float currentStatValue = _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(type).GetStat(_statType).Value;
            float maxStatValue = _staticDataService.GetSpaceship(type).GetStat(_statType).MaxBoost;

            _progressbarValue.fillAmount = currentStatValue / maxStatValue;
            _upgradeCosteValue.text = UpgradeCost.ToString();

            _blockPanel.SetActive(_persistentProgress.Progress.AvailableStatsToUpgrade.CheckAvailability(_statType) == false);
            _upgradeButton.interactable = _persistentProgress.Progress.AvailableStatsToUpgrade.CheckAvailability(_statType) && _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(type).IsUnlocked;
        }

        private void OnUpgradeButtonClicked()
        {
            if (_persistentProgress.Progress.Wallet.TryTake(UpgradeCost))
            {
                _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Level++;
                _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceship).GetStat(_statType).Value += _staticDataService.GetSpaceship(_currentSpaceship).GetStat(_statType).UpgradeValue;
                ResetSpaceship(_currentSpaceship);
            }
        }
    }
}