using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay
{
    public class GameplayStateMachineInstaller : Installer<GameplayStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<GameplayStateMachine>().AsSingle();
        }
    }
}
