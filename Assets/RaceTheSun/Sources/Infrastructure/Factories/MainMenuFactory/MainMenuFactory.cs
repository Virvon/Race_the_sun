using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly UI.MainMenu.MainMenu.Factory _mainMenuFactory;
        private readonly DiContainer _container;
        private readonly MainMenuCameras _mainMenuCameras;
        private readonly TrailPoint.Factory _trailPointFactory;
        private readonly ModelPoint.Factory _modelPointFactory;

        public MainMenuFactory(UI.MainMenu.MainMenu.Factory mainMenuFactory, DiContainer container, MainMenuCameras mainMenuCameras, TrailPoint.Factory trailPointFactory, ModelPoint.Factory modelPointFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _container = container;
            _mainMenuCameras = mainMenuCameras;
            _trailPointFactory = trailPointFactory;
            _modelPointFactory = modelPointFactory;
        }

        public async UniTask CreateMainMenu() =>
            await _mainMenuFactory.Create(MainMenuFactoryAssets.MainMenu);

        public async UniTask CreateModelPoint(Vector3 position)
        {
            ModelPoint modelPoint = await _modelPointFactory.Create(MainMenuFactoryAssets.ModelPoint);

            modelPoint.transform.position = position;
            _container.Bind<ModelPoint>().FromInstance(modelPoint).AsSingle();
        }

        public async UniTask CreateTrailPoint(Vector3 position)
        {
            TrailPoint trailPoint = await _trailPointFactory.Create(MainMenuFactoryAssets.TrailPoint);

            trailPoint.transform.position = position;
            _container.Bind<TrailPoint>().FromInstance(trailPoint).AsSingle();
        }
    }
}