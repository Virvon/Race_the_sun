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
    public class CurrentClickedButtonsPanel : MonoBehaviour
    {
        [SerializeField] private CurrentClickedSpacehipWatcher _currentClickedSpaceshipWatcher;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _buyCostValue;
        [SerializeField] private GameObject _selectOrCustomizePanel;
        [SerializeField] private Button _selectButton;

        private IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;

        private SpaceshipType _currentSpaceshipType;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
        }

        private void OnEnable()
        {
            _currentClickedSpaceshipWatcher.CurrentSpaceshipChanged += OnCurrentSpaceshipChanged;
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        private void OnDisable()
        {
            _currentClickedSpaceshipWatcher.CurrentSpaceshipChanged += OnCurrentSpaceshipChanged;
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        }

        private void OnSelectButtonClicked() =>
            _persistentProgressService.Progress.AvailableSpaceships.Selcect(_currentSpaceshipType);

        private void OnCurrentSpaceshipChanged(SpaceshipType type)
        {
            _currentSpaceshipType = type;

            bool isUnlocked = _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceshipType).IsUnlocked;

            if(isUnlocked)
            {
                _buyButton.gameObject.SetActive(false);
                _selectOrCustomizePanel.SetActive(true);
            }
            else
            {
                _buyCostValue.text = _staticDataService.GetSpaceship(_currentSpaceshipType).BuyCost.ToString();
                _selectOrCustomizePanel.SetActive(false);
                _buyButton.gameObject.SetActive(true);
            }
        }

        private void OnBuyButtonClicked()
        {
            if(_persistentProgressService.Progress.Wallet.TryTake(_staticDataService.GetSpaceship(_currentSpaceshipType).BuyCost))
            {
                _persistentProgressService.Progress.AvailableSpaceships.Unlock(_currentSpaceshipType);
            }
            else
            {
                Debug.Log("cant buy");
            }
        }
    }
}