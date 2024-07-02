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
        }

        private void BindMainMenuFactory() =>
            MainMenuFactoryInstaller.Install(Container);

        private void BindMainMenuBootstrapper() =>
            Container.BindInterfacesAndSelfTo<MainMenuBootstrapper>().AsSingle().NonLazy();
    }
}
