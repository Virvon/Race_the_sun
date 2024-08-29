using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.MainMenu.Model;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactory : IMainMenuFactory
    {
        private readonly UI.MainMenu.MainMenu.Factory _mainMenuFactory;
        private readonly DiContainer _container;
        private readonly TrailPoint.Factory _trailPointFactory;
        private readonly ModelSpawner.Factory _modelPointFactory;

        public MainMenuFactory(
            UI.MainMenu.MainMenu.Factory mainMenuFactory,
            DiContainer container,
            TrailPoint.Factory trailPointFactory,
            ModelSpawner.Factory modelPointFactory)
        {
            _mainMenuFactory = mainMenuFactory;
            _container = container;
            _trailPointFactory = trailPointFactory;
            _modelPointFactory = modelPointFactory;
        }

        public async UniTask CreateMainMenu() =>
            await _mainMenuFactory.Create(MainMenuFactoryAssets.MainMenu);

        public async UniTask CreateModelSpawner(Vector3 position)
        {
            ModelSpawner modelPoint = await _modelPointFactory.Create(MainMenuFactoryAssets.ModelPoint);

            modelPoint.transform.position = position;
            _container.Bind<ModelSpawner>().FromInstance(modelPoint).AsSingle();
        }

        public async UniTask CreateTrailPoint(Vector3 position)
        {
            TrailPoint trailPoint = await _trailPointFactory.Create(MainMenuFactoryAssets.TrailPoint);

            trailPoint.transform.position = position;
            _container.Bind<TrailPoint>().FromInstance(trailPoint).AsSingle();
        }
    }
}