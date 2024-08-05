using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class CustomizeWindow : OpenableWindow
    {
        private MainMenuCameras _mainMenuCameras;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras)
        {
            _mainMenuCameras = mainMenuCameras;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.CustomizeCamera);
            gameObject.SetActive(true);
        }
    }
}