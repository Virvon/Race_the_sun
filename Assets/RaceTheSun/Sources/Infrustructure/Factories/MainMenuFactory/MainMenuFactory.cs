using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly UI.MainMenu.MainMenu.Factory _mainMenuFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly SpaceshipModel.Factory _spaceshipModelFactory;
        private readonly DiContainer _container;
        private readonly FreeLookCamera.Factory _freeLookCameraFactory;
        private readonly MainMenuCameras _mainMenuCameras;

        public MainMenuFactory(UI.MainMenu.MainMenu.Factory mainMenuFactory, IStaticDataService staticDataService, SpaceshipModel.Factory spaceshipModelFactory, DiContainer container, FreeLookCamera.Factory freeLookCameraFactory, MainMenuCameras mainMenuCameras)
        {
            _mainMenuFactory = mainMenuFactory;
            _staticDataService = staticDataService;
            _spaceshipModelFactory = spaceshipModelFactory;
            _container = container;
            _freeLookCameraFactory = freeLookCameraFactory;
            _mainMenuCameras = mainMenuCameras;
        }

        public async UniTask CreateMainMenu() =>
            await _mainMenuFactory.Create(MainMenuFactoryAssets.MainMenu);

        public async UniTask CreateSpaceshipModel(SpaceshipType type, Vector3 position)
        {
            SpaceshipModel spaceshipModel = await _spaceshipModelFactory.Create(_staticDataService.GetSpaceship(type).ModelPrefabReference);

            spaceshipModel.transform.position = position;
            _container.Bind<SpaceshipModel>().FromInstance(spaceshipModel).AsSingle();            
        }

        public async UniTask CreateMainMenuMainCamera()
        {
            FreeLookCamera freeLookCamera = await _freeLookCameraFactory.Create(MainMenuFactoryAssets.MainMenuMainCamera);

            _mainMenuCameras.Init(freeLookCamera.GetComponent<MainMenuMainCamera>());
        }

        public async UniTask CreateTrailCamera()
        {
            FreeLookCamera freeLookCamera = await _freeLookCameraFactory.Create(MainMenuFactoryAssets.TrailCamera);

            _mainMenuCameras.Init(freeLookCamera.GetComponent<TrailCamera>());
        }
    }
}