using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactoryInstaller : Installer<MainMenuFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IMainMenuFactory>().To<MainMenuFactory>().AsSingle();

            Container
                .BindFactory<string, UniTask<GameObject>, HudFactory>()
                .FromFactory<KeyPrefabFactoryAsync<GameObject>>();
        }
    }
}