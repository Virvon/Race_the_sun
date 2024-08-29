using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.MainMenu.ModelPoint;
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
                .BindFactory<string, UniTask<ModelPoint>, ModelPoint.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<ModelPoint>>();

            Container
                .BindFactory<string, UniTask<TrailPoint>, TrailPoint.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<TrailPoint>>();
        }
    }
}