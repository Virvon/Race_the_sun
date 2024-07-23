using Assets.RaceTheSun.Sources.Animations;
using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Services.WaitingService;
using Cinemachine;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly SpaceshipMainCamera _spaceshipMainCamera;
        private readonly HudAnimation _hudAnimation;
        private readonly IWaitingService _waitingService;

        public GameLoopState(SpaceshipMainCamera spaceshipMainCamera, HudAnimation hudAnimation, IWaitingService waitingService)
        {
            _spaceshipMainCamera = spaceshipMainCamera;
            _hudAnimation = hudAnimation;
            _waitingService = waitingService;
        }

        public UniTask Enter()
        {
            _spaceshipMainCamera.GetComponent<CinemachineVirtualCamera>().Priority = (int)CameraPriority.Use;
            _waitingService.Wait(2.5f, callback: _hudAnimation.Open);

            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
