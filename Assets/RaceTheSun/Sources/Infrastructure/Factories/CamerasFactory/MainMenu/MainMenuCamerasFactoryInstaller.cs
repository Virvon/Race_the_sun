using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.MainMenu
{
    public class MainMenuCamerasFactoryInstaller : Installer<MainMenuCamerasFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IMainMenuCamerasFactory>()
                .To<MainMenuCamerasFactory>()
                .AsSingle();

            Container
                .BindFactory<string, UniTask<FreeLookCamera>, FreeLookCamera.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<FreeLookCamera>>();
        }
    }
}
