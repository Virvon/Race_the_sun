using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipStatPanel : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private StatType _statType;
        [SerializeField] private MPUIKIT.MPImage _progressbarValue;

        private IPersistentProgressService _persistentProgress;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress, IStaticDataService staticDataService)
        {
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;
        }

        public void ResetSpaceship(SpaceshipType type)
        {
            float currentStatValue = _persistentProgress.Progress.AvailableSpaceships.GetSpaceshipData(type).GetStat(_statType);
            float maxStatValue = _staticDataService.GetSpaceship(type).GetStat(_statType).MaxBoost;

            _progressbarValue.fillAmount = currentStatValue / maxStatValue;
        }

        //private void OnSpaceshipSelected(SpaceshipInfo spaceshipInfo)
        //{
        //    if (spaceshipInfo.IsUnlocked && _persistentProgress.Progress.AvailableStatsToUpgrade.CheckAvailability(_statType))
        //        _upgradeButton.interactable = true;
        //    else
        //        _upgradeButton.interactable = false;
        //}
    }
}