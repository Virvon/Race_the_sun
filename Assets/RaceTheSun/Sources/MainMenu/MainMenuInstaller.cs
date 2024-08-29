using Assets.RaceTheSun.Sources.GameLogic.Cameras.MainMenu;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.SpaceshipModelFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainMenuBootstrapper();
            BindMainMenuFactory();
            BindMainMenuCameras();
            SpaceshipModelFactoryInstaller.Install(Container);
        }

        private void BindMainMenuCameras()
        {
            Container.BindInterfacesAndSelfTo<MainMenuCameras>().AsSingle();
        }

        private void BindMainMenuFactory() =>
            MainMenuFactoryInstaller.Install(Container);

        private void BindMainMenuBootstrapper() =>
            Container.BindInterfacesAndSelfTo<MainMenuBootstrapper>().AsSingle().NonLazy();
    }
}
