using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipStatPanel : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private SpaceshipSelection _spaceshipSelection;
        [SerializeField] private StatType _statType;

        private IPersistentProgressService _persistentProgress;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;
        }

        private void OnEnable()
        {
            _spaceshipSelection.SpaceshipSelected += OnSpaceshipSelected;
        }

        private void OnDisable()
        {
            _spaceshipSelection.SpaceshipSelected -= OnSpaceshipSelected;
        }

        private void OnSpaceshipSelected(SpaceshipInfo spaceshipInfo)
        {
            if (spaceshipInfo.IsUnlocked && _persistentProgress.Progress.AvailableStatsToUpgrade.CheckAvailability(_statType))
                _upgradeButton.interactable = true;
            else
                _upgradeButton.interactable = false;
        }
    }
}