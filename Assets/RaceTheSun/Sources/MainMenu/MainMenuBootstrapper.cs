using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu
{
    public class MainMenuBootstrapper : IInitializable
    {
        private readonly IMainMenuFactory _mainMenuFactory;
        private readonly IUiFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly MainMenuCameras _mainMenuCameras;
        private readonly ILoadingCurtain _loadingCurtain;

        private Vector3 _modelPointPosition = new Vector3(0, 3, 0);
        private Vector3 _trailPointPosition = new Vector3(0, 3, -1.5f);

        public MainMenuBootstrapper(IMainMenuFactory mainMenuFactory, IUiFactory uiFactory, IPersistentProgressService persistentProgressService, MainMenuCameras mainMenuCameras, ILoadingCurtain loadingCurtain)
        {
            _mainMenuFactory = mainMenuFactory;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
            _mainMenuCameras = mainMenuCameras;
            _loadingCurtain = loadingCurtain;
        }

        public async void Initialize()
        {
            await _mainMenuFactory.CreateModelPoint(_modelPointPosition);
            await _mainMenuFactory.CreateTrailPoint(_trailPointPosition);
            await _mainMenuFactory.CreateMainMenu();
            await _mainMenuFactory.CreateMainMenuMainCamera();
            await _mainMenuFactory.CreateSelectionCamera();
            await _mainMenuFactory.CreateCustomizeCamera();
            await _mainMenuFactory.CreateTrailCamera();

            _mainMenuCameras.IncludeCamera(MainMenuCameraType.MainCamera);
            _loadingCurtain.Hide(0.1f);
        }
    }
}
