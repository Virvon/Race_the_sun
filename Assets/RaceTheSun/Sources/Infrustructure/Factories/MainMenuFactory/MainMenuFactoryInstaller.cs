using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory
{
    public class MainMenuFactoryInstaller : Installer<MainMenuFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IMainMenuFactory>().To<MainMenuFactory>().AsSingle();

            Container
                .BindFactory<string, UniTask<UI.MainMenu.MainMenu>, UI.MainMenu.MainMenu.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<UI.MainMenu.MainMenu>>();

            Container
                .BindFactory<AssetReferenceGameObject, UniTask<SpaceshipModel>, SpaceshipModel.Factory>()
                .FromFactory<RefefencePrefabFactoryAsync<SpaceshipModel>>();

            Container
                .BindFactory<string, UniTask<FreeLookCamera>, FreeLookCamera.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<FreeLookCamera>>();
        }
    }
}