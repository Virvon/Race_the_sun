﻿using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.MainMenu.Model;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactoryInstaller : Installer<MainMenuFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IMainMenuFactory>()
                .To<MainMenuFactory>()
                .AsSingle();

            Container
                .BindFactory<string, UniTask<UI.MainMenu.MainMenu>, UI.MainMenu.MainMenu.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<UI.MainMenu.MainMenu>>();

            Container
                .BindFactory<string, UniTask<ModelSpawner>, ModelSpawner.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<ModelSpawner>>();

            Container
                .BindFactory<string, UniTask<TrailPoint>, TrailPoint.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<TrailPoint>>();
        }
    }
}