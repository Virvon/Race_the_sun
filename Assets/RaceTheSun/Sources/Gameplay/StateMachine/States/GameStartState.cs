using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameStartState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly WorldGenerator.WorldGenerator _worldGenerator;
        private readonly IGameplayFactory _gameplayFacotry;
        private readonly Spaceship.Spaceship _spaceship;
        private readonly StartMovement _startMovement;
        

        public GameStartState(GameplayStateMachine gameplayStateMachine, IStaticDataService staticDataService, WorldGenerator.WorldGenerator worldGenerator, IGameplayFactory gameplayFacotry, Spaceship.Spaceship spaceship)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _staticDataService = staticDataService;
            _worldGenerator = worldGenerator;
            _gameplayFacotry = gameplayFacotry;
            _spaceship = spaceship;
            _startMovement = _spaceship.GetComponentInChildren<StartMovement>();
            
        }

        public async UniTask Enter()
        {
            StageConfig stageConfig = _staticDataService.GetStage(Stage.StartStage);

            _startMovement.Move(() =>
            {
                _gameplayStateMachine.Enter<GameLoopState>().Forget();
            });
        }

        public UniTask Exit()
        {
            return default;
        }

        
    }
}
