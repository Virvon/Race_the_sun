using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.MainMenu;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu
{
    public class MainMenuBootstrapper : IInitializable
    {
        private const float HideLoadingCurtainDuration = 0.1f;

        private readonly IMainMenuFactory _mainMenuFactory;
        private readonly MainMenuCameras _mainMenuCameras;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IMainMenuCamerasFactory _mainMenuCamerasFactory;

        private Vector3 _modelSpawnerPosition = new Vector3(0, 3, 0);
        private Vector3 _trailPointPosition = new Vector3(0, 3, -1.5f);

        public MainMenuBootstrapper(
            IMainMenuFactory mainMenuFactory,
            MainMenuCameras mainMenuCameras,
            ILoadingCurtain loadingCurtain,
            IMainMenuCamerasFactory mainMenuCamerasFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _mainMenuCameras = mainMenuCameras;
            _loadingCurtain = loadingCurtain;
            _mainMenuCamerasFactory = mainMenuCamerasFactory;
        }

        public async void Initialize()
        {
            await _mainMenuFactory.CreateModelSpawner(_modelSpawnerPosition);
            await _mainMenuFactory.CreateTrailPoint(_trailPointPosition);
            await _mainMenuFactory.CreateMainMenu();
            await _mainMenuCamerasFactory.CreateMainMenuMainCamera();
            await _mainMenuCamerasFactory.CreateSelectionCamera();
            await _mainMenuCamerasFactory.CreateCustomizeCamera();
            await _mainMenuCamerasFactory.CreateTrailCamera();

            _mainMenuCameras.IncludeCamera(MainMenuCameraType.MainCamera);
            _loadingCurtain.Hide(HideLoadingCurtainDuration);
        }
    }
}
