﻿using System;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.GameLogic.Trail;
using Assets.RaceTheSun.Sources.MainMenu.Model;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Services.SaveLoad;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships;
using Assets.RaceTheSun.Sources.UI.MainMenu.TrailPanel;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class TrailWindow : OpenableWindow
    {
        [SerializeField] private TrailButton[] _trailButtons;
        [SerializeField] private TrailInfoPanel _trailInfoPanel;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;

        private MainMenuCameras _mainMenuCameras;
        private ModelSpawner _modelPoint;
        private IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;
        private ISaveLoadService _saveLoadService;
        private TrailType _currentTrailType;
        private GameObject _currentSelectedFrame;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras, ModelSpawner modelPoint, IPersistentProgressService persistentProgressService, IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            _mainMenuCameras = mainMenuCameras;
            _modelPoint = modelPoint;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;

            foreach (TrailButton trailButton in _trailButtons)
                trailButton.Clicked += OnTrailButtonClicked;

            _trailInfoPanel.Clicked += OnTrailInfoClicked;
        }

        private void OnDestroy()
        {
            foreach (TrailButton trailButton in _trailButtons)
                trailButton.Clicked -= OnTrailButtonClicked;

            _trailInfoPanel.Clicked -= OnTrailInfoClicked;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            _currentSelectedFrame?.SetActive(false);
            _trailInfoPanel.Hide();
            _modelPoint.ChangeTrail(_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).TrailType);
        }

        public override void Open()
        {
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.TrailCamera);
            gameObject.SetActive(true);
        }

        private void OnTrailButtonClicked(TrailType trailType, GameObject selectedFrame)
        {
            _currentTrailType = trailType;
            _trailInfoPanel.ShowInfo(_currentTrailType);
            _modelPoint.ChangeTrail(_currentTrailType);

            _currentSelectedFrame?.SetActive(false);
            _currentSelectedFrame = selectedFrame;
            _currentSelectedFrame.SetActive(true);
        }

        private void OnTrailInfoClicked()
        {
            if (_persistentProgressService.Progress.AvailableTrails.IsUnlocked(_currentTrailType))
            {
                _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_currentClickedSpaceshipInfo.SpaceshipType).TrailType = _currentTrailType;
                _trailInfoPanel.Hide();
            }
            else if (_persistentProgressService.Progress.Wallet.TryTake(_staticDataService.GetTrail(_currentTrailType).BuyCost))
            {
                _persistentProgressService.Progress.AvailableTrails.UnlockedTrails.Add(_currentTrailType);
                _trailInfoPanel.ShowInfo(_currentTrailType);
            }

            _saveLoadService.SaveProgress();
            _currentSelectedFrame.SetActive(false);
        }
    }
}