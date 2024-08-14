using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Trail;
using Assets.RaceTheSun.Sources.UI.MainMenu.TrailPanel;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class TrailWindow : OpenableWindow
    {
        [SerializeField] private TrailButton[] _trailButtons;
        [SerializeField] private TrailInfoPanel _trailInfoPanel;

        private MainMenuCameras _mainMenuCameras;
        private ModelPoint _modelPoint;
        private IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;
        private TrailType _currentTrailType;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras, ModelPoint modelPoint, IPersistentProgressService persistentProgressService, IStaticDataService staticDataService)
        {
            _mainMenuCameras = mainMenuCameras;
            _modelPoint = modelPoint;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;

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
        }

        public override void Open()
        {
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.TrailCamera);
            gameObject.SetActive(true);
        }

        private void OnTrailButtonClicked(TrailType trailType)
        {
            _currentTrailType = trailType;
            _trailInfoPanel.ShowInfo(_currentTrailType);
            _modelPoint.ChangeTrail(_currentTrailType);
        }

        private void OnTrailInfoClicked()
        {
            if (_persistentProgressService.Progress.AvailableTrails.IsUnlocked(_currentTrailType))
            {
                _persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType).TrailType = _currentTrailType;
                _trailInfoPanel.Hide();
            }
            else if (_persistentProgressService.Progress.Wallet.TryTake(_staticDataService.GetTrail(_currentTrailType).BuyCost))
            {
                _persistentProgressService.Progress.AvailableTrails.UnlockedTrails.Add(_currentTrailType);
                _trailInfoPanel.ShowInfo(_currentTrailType);
            }
                
        }
    }
}