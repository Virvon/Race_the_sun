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
            BindGameplayStateMachine();
            BindScoreCounter();
        }

        private void BindScoreCounter() =>
            Container.BindInterfacesAndSelfTo<ScoreCounter.ScoreCounter>().AsSingle();

        private void BindGameplayStateMachine() =>
            GameplayStateMachineInstaller.Install(Container);

        private void BindGameplayFactory() =>
            GameplayFactoryInstaller.Install(Container);

        private void BindGameplayBootstrapper() =>
            Container.BindInterfacesAndSelfTo<GameplayBootstrapper>().AsSingle().NonLazy();
    }
}