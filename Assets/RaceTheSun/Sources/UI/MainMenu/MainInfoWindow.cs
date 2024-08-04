using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class MainInfoWindow : OpenableWindow
    {
        private MainMenuCameras _mainMenuCameras;
        private IPersistentProgressService _persistentProgressService;
        private ModelPoint _modelPoint;

        [Inject]
        private void Construct(MainMenuCameras mainMenuCameras, IPersistentProgressService persistentProgressService, ModelPoint modelPoint)
        {
            _mainMenuCameras = mainMenuCameras;
            _persistentProgressService = persistentProgressService;
            _modelPoint = modelPoint;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public async override void Open()
        {
            await _modelPoint.Change(_persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType);
            gameObject.SetActive(true);
            _mainMenuCameras.IncludeCamera(MainMenuCameraType.MainCamera);
        }
    }
}