using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayBootstrapper();
            BindGameplayFactory();
        }

        private void BindGameplayFactory() =>
            GameplayFactoryInstaller.Install(Container);

        private void BindGameplayBootstrapper() =>
            Container.BindInterfacesAndSelfTo<GameplayBootstrapper>().AsSingle().NonLazy();
    }
}
