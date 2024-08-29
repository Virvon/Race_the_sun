using Zenject;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}