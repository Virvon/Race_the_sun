using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class SpaceshipSelection : MonoBehaviour
    {
        [SerializeField] private TMP_Text _spaceshipName;
        [SerializeField] private SpaceshipInfoButton[] _spaceshipInfoButtons;

        private IPersistentProgressService _persistentProgress;
        private SpaceshipInfo _selectedSpaceship;
        private SpaceshipInfo _currentShip;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;
        }

        public event Action<SpaceshipInfo> SpaceshipSelected;

        public bool CanBuyCurrentShip { get; private set; }

        private void OnEnable()
        {
            foreach(SpaceshipInfoButton spaceshipInfoButton in _spaceshipInfoButtons)
            {
                spaceshipInfoButton.Selected += OnSpaceshipSelected;
            }
        }

        private void OnDisable()
        {
            foreach (SpaceshipInfoButton spaceshipInfoButton in _spaceshipInfoButtons)
            {
                spaceshipInfoButton.Selected -= OnSpaceshipSelected;
            }
        }

        public void Unlock()
        {
            if (_persistentProgress.Progress.Wallet.TryTake(_currentShip.BuyCost))
            {
                _currentShip.Unlock();
                _persistentProgress.Progress.AvailableStatsToUpgrade.Add(_currentShip.UnlockedStatType);
            }
        }

        public void Choose()
        {
            if (_currentShip.IsUnlocked)
            {
                _selectedSpaceship?.Deselect();
                _selectedSpaceship = _currentShip;
                _selectedSpaceship.Select();
            }
        }

        private void OnSpaceshipSelected(SpaceshipInfo spaceshipInfo)
        {
            _currentShip = spaceshipInfo;
            _spaceshipName.text = _currentShip.Name;

            if (_currentShip.IsUnlocked == false)
            {
                CanBuyCurrentShip = _currentShip.BuyCost <= _persistentProgress.Progress.Wallet.Value;
            }

            SpaceshipSelected?.Invoke(_currentShip);
        }   
    }
}