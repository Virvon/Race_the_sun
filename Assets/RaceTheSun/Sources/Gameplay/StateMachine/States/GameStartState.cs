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

        public GameStartState(GameplayStateMachine gameplayStateMachine, IStaticDataService staticDataService, WorldGenerator.WorldGenerator worldGenerator)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _staticDataService = staticDataService;
            _worldGenerator = worldGenerator;
        }

        public UniTask Enter()
        {
            StageConfig stageConfig = _staticDataService.GetStage(Stage.StartStage);
            _worldGenerator.SetTilesToGenerate(stageConfig.Tiles);

            _worldGenerator.LastTileGenerated += OnLastTileGenerated;
            return default;
        }

        public UniTask Exit()
        {
            _worldGenerator.LastTileGenerated -= OnLastTileGenerated;
            return default;
        }

        private void OnLastTileGenerated()
        {
            _gameplayStateMachine.Enter<GameStageState, int>(++_gameplayStateMachine.Stage).Forget();
        }

        
    }
}
