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
        private readonly HudAnimation _hudAnimation;
        private readonly IWaitingService _waitingService;
        private readonly Cameras.Cameras _cameras;
        private readonly Spaceship.Spaceship _spaceship;

        public GameLoopState(HudAnimation hudAnimation, IWaitingService waitingService, Cameras.Cameras cameras, Spaceship.Spaceship spaceship)
        {
            _hudAnimation = hudAnimation;
            _waitingService = waitingService;
            _cameras = cameras;
            _spaceship = spaceship;
        }

        public UniTask Enter()
        {
            _spaceship.UpdateSpeedDecorator();
            _cameras.IncludeCamera(CameraType.MainCamera);
            _waitingService.Wait(2.5f, callback: _hudAnimation.Open);

            return default;
        }

        public UniTask Exit()
        {
            _hudAnimation.Hide();
            return default;
        }
    }
}
