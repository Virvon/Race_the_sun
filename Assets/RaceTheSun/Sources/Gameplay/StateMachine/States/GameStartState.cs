using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameStartState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly WorldGenerator.WorldGenerator _worldGenerator;
        private readonly IGameplayFactory _gameplayFacotry;

        public GameStartState(GameplayStateMachine gameplayStateMachine, IStaticDataService staticDataService, WorldGenerator.WorldGenerator worldGenerator, IGameplayFactory gameplayFacotry)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _staticDataService = staticDataService;
            _worldGenerator = worldGenerator;
            _gameplayFacotry = gameplayFacotry;
        }

        public async UniTask Enter()
        {
            StageConfig stageConfig = _staticDataService.GetStage(Stage.StartStage);
            _worldGenerator.SetTilesToGenerate(stageConfig.Tiles);
            await _gameplayFacotry.CreateStartCamera();
        }

        public UniTask Exit()
        {
            return default;
        } 
    }
}
