﻿using Assets.MyBakery.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Assets.RaceTheSun.Sources.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindLoadingCurtain();
            BindAssetProvider();
            BindSceneLoader();
            //BindInputService();
            BindGameplayFactory();
            BindStaticDataService();
        }

        private void BindStaticDataService() =>
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

        private void BindGameplayFactory()
        {
            Container
                .Bind<IGameplayFactory>()
                .FromSubContainerResolve()
                .ByInstaller<GameplayFactoryInstaller>()
                .AsSingle();
        }

        private void BindSceneLoader() =>
            Container.BindInterfacesTo<SceneLoader>().AsSingle();

        private void BindAssetProvider() =>
            Container.BindInterfacesTo<AssetProvider>().AsSingle();

        private void BindLoadingCurtain()
        {
            Container.BindFactory<string, UniTask<LoadingCurtain>, LoadingCurtain.Factory>().FromFactory<KeyPrefabFactoryAsync<LoadingCurtain>>();
            Container.BindInterfacesAndSelfTo<LoadingCurtainProxy>().AsSingle();
        }

        private void BindGameStateMachine() =>
            GameStateMachineInstaller.Install(Container);
    }
}
