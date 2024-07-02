using Assets.RaceTheSun.Sources.Infrustructure.GameStateMachine;
using Zenject;

namespace Virvon.MyBakery.Infrustructure.States
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            //Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}