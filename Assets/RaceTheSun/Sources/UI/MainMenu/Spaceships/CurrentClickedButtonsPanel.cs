﻿using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Services.SaveLoad;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships
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
        private ISaveLoadService _saveLoadService;
        private SpaceshipType _currentSpaceshipType;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService, IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
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

        private void OnSelectButtonClicked()
        {
            _persistentProgressService.Progress.AvailableSpaceships.Selcect(_currentSpaceshipType);
            _saveLoadService.SaveProgress();
            _currentClickedSpaceshipWatcher.Reset();
        }

        private void OnCurrentSpaceshipChanged(SpaceshipType type)
        {
            _currentSpaceshipType = type;

            bool isUnlocked = _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentSpaceshipType).IsUnlocked;

            if (isUnlocked)
            {
                _buyButton.gameObject.SetActive(false);
                _selectOrCustomizePanel.SetActive(true);

                _selectButton.interactable = _persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType != type;
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
            if (_persistentProgressService.Progress.Wallet.TryTake(_staticDataService.GetSpaceship(_currentSpaceshipType).BuyCost))
            {
                _persistentProgressService.Progress.AvailableSpaceships.Unlock(_currentSpaceshipType);
                _persistentProgressService.Progress.AvailableStatsToUpgrade.Add(_staticDataService.GetSpaceship(_currentSpaceshipType).UnlockedStatType);
                _persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType = _currentSpaceshipType;
                _saveLoadService.SaveProgress();
                _currentClickedSpaceshipWatcher.Reset();
            }
            else
            {
                Debug.Log("cant buy");
            }
        }
    }
}