using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI.MainMenu.Spaceships;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class CustomizeWindow : OpenableWindow
    {
        [SerializeField] private TMP_Text _currentSpaceshipName;
        [SerializeField] private CurrentClickedSpaceshipInfo _currentClickedSpaceshipInfo;

        private MainMenuCameras _mainMenuCameras;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras, IStaticDataService staticDataService)
        {
            _mainMenuCameras = mainMenuCameras;
            _staticDataService = staticDataService;
        }

        public override void Hide() =>
            gameObject.SetActive(false);

        public override void Open()
        {
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.CustomizeCamera);
            _currentSpaceshipName.text = _staticDataService.GetSpaceship(_currentClickedSpaceshipInfo.SpaceshipType).Name;
            gameObject.SetActive(true);
        }
    }
}