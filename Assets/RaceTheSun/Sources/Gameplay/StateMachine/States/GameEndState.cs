using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Cinemachine;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameEndState : IState
    {
        private readonly CinemachineVirtualCamera _spaceshipSideCamera;

        public GameEndState(SpaceshipSideCamera spaceshipSideCamera)
        {
            _spaceshipSideCamera = spaceshipSideCamera.GetComponent<CinemachineVirtualCamera>();
        }

        public UniTask Enter()
        {
            _spaceshipSideCamera.Priority = (int)CameraPriority.Use;
            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
