using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrustructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameStartState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly Spaceship.Spaceship _spaceship;
        private readonly StartMovement _startMovement;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly Sun.Sun _sun;


        public GameStartState(GameplayStateMachine gameplayStateMachine, IStaticDataService staticDataService, Spaceship.Spaceship spaceship, ILoadingCurtain loadingCurtain, Sun.Sun sun)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _staticDataService = staticDataService;
            _spaceship = spaceship;
            _startMovement = _spaceship.GetComponentInChildren<StartMovement>();
            _loadingCurtain = loadingCurtain;
            _sun = sun;
        }

        public UniTask Enter()
        {
            _loadingCurtain.Hide(0.6f);

            StageConfig stageConfig = _staticDataService.GetStage(Stage.StartStage);

            _startMovement.Move(() =>
            {
                _gameplayStateMachine.Enter<GameLoopState>().Forget();
            });

            _sun.Show();

            return default;
        }

        public UniTask Exit()
        {
            return default;
        }

        
    }
}
