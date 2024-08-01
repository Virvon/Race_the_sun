using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMainMenuBootstrapper();
            BindMainMenuFactory();
            BindUiFactory();
            BindMainMenuCameras();
        }

        private void BindMainMenuCameras()
        {
            Container.BindInterfacesAndSelfTo<MainMenuCameras>().AsSingle();
        }

        private void BindUiFactory() =>
            UiFactoryInstaller.Install(Container);

        private void BindMainMenuFactory() =>
            MainMenuFactoryInstaller.Install(Container);

        private void BindMainMenuBootstrapper() =>
            Container.BindInterfacesAndSelfTo<MainMenuBootstrapper>().AsSingle().NonLazy();
    }
}
