using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
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
        private readonly ModelPoint.Factory _modelPointFactory;
        private readonly TrailPoint.Factory _trailPointFactory;

        public MainMenuFactory(UI.MainMenu.MainMenu.Factory mainMenuFactory, IStaticDataService staticDataService, SpaceshipModel.Factory spaceshipModelFactory, DiContainer container, FreeLookCamera.Factory freeLookCameraFactory, MainMenuCameras mainMenuCameras, ModelPoint.Factory modelPointFactory, TrailPoint.Factory trailPointFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _staticDataService = staticDataService;
            _spaceshipModelFactory = spaceshipModelFactory;
            _container = container;
            _freeLookCameraFactory = freeLookCameraFactory;
            _mainMenuCameras = mainMenuCameras;
            _modelPointFactory = modelPointFactory;
            _trailPointFactory = trailPointFactory;
        }

        public async UniTask CreateMainMenu() =>
            await _mainMenuFactory.Create(MainMenuFactoryAssets.MainMenu);

        public async UniTask<SpaceshipModel> CreateSpaceshipModel(SpaceshipType type, Vector3 position)
        {
            SpaceshipModel spaceshipModel = await _spaceshipModelFactory.Create(_staticDataService.GetSpaceship(type).ModelPrefabReference);

            spaceshipModel.transform.position = position;

            return spaceshipModel;
        }

        public async UniTask CreateMainMenuMainCamera()
        {
            FreeLookCamera freeLookCamera = await _freeLookCameraFactory.Create(MainMenuFactoryAssets.MainMenuMainCamera);

            _mainMenuCameras.Init(freeLookCamera.GetComponent<MainMenuMainCamera>());
        }

        public async UniTask CreateSelectionCamera()
        {
            FreeLookCamera freeLookCamera = await _freeLookCameraFactory.Create(MainMenuFactoryAssets.SelectionCamera);

            _mainMenuCameras.Init(freeLookCamera.GetComponent<SelectionCamera>());
        }

        public async UniTask CreateModelPoint(Vector3 position)
        {
            ModelPoint modelPoint = await _modelPointFactory.Create(MainMenuFactoryAssets.ModelPoint);

            modelPoint.transform.position = position;
            _container.Bind<ModelPoint>().FromInstance(modelPoint).AsSingle();
        }

        public async UniTask CreateCustomizeCamera()
        {
            FreeLookCamera freeLookCamera = await _freeLookCameraFactory.Create(MainMenuFactoryAssets.CustomizeCamera);

            _mainMenuCameras.Init(freeLookCamera.GetComponent<CustomizeCamera>());
        }
        public async UniTask CreateTrailCamera()
        {
            FreeLookCamera freeLookCamera = await _freeLookCameraFactory.Create(MainMenuFactoryAssets.TrailCamera);

            _mainMenuCameras.Init(freeLookCamera.GetComponent<TrailCamera>());
        }
        public async UniTask CreateTrailPoint(Vector3 position)
        {
            TrailPoint trailPoint = await _trailPointFactory.Create(MainMenuFactoryAssets.TrailPoint);

            trailPoint.transform.position = position;
            _container.Bind<TrailPoint>().FromInstance(trailPoint).AsSingle();
        }
    }
}