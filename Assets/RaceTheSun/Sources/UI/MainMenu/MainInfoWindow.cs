using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.MainMenu.Model;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class MainInfoWindow : OpenableWindow
    {
        [SerializeField] private Education _spaceshipsEducation;
        [SerializeField] private Education _shopEducation;

        private MainMenuCameras _mainMenuCameras;
        private IPersistentProgressService _persistentProgressService;
        private ModelSpawner _modelPoint;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras, IPersistentProgressService persistentProgressService, ModelSpawner modelPoint)
        {
            _mainMenuCameras = mainMenuCameras;
            _persistentProgressService = persistentProgressService;
            _modelPoint = modelPoint;
            CheackEducation();
        }

        public override void Hide() =>
            gameObject.SetActive(false);

        public async override void Open()
        {
            await _modelPoint.Change(_persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType);
            gameObject.SetActive(true);
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.MainCamera);
        }

        private void CheackEducation()
        {
            if (_persistentProgressService.Progress.Wallet.Value <= 0)
                return;

            if (_persistentProgressService.Progress.Education.IsSpaceshipWindowShowed == false)
            {
                _spaceshipsEducation.ShowEducation();
                _persistentProgressService.Progress.Education.IsSpaceshipWindowShowed = true;
            }
            else if(_persistentProgressService.Progress.Education.IsSpaceshipWindowShowed && _persistentProgressService.Progress.Education.IsShopWindowShowed == false)
            {
                _shopEducation.ShowEducation();
                _persistentProgressService.Progress.Education.IsShopWindowShowed = true;
            }
        }
    }
}