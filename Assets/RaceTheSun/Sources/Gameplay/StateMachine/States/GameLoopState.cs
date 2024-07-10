using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly SpaceshipMainCamera _spaceshipMainCamera;

        public GameLoopState(SpaceshipMainCamera spaceshipMainCamera)
        {
            _spaceshipMainCamera = spaceshipMainCamera;
        }

        public UniTask Enter()
        {
            _spaceshipMainCamera.GetComponent<CinemachineVirtualCamera>().Priority = (int)CameraPriority.Use;

            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
