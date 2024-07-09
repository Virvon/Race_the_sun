using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameStageState : IPayloadState<int>
    {
        private readonly WorldGenerator.WorldGenerator _worldGenerator;
        private readonly IStaticDataService _staticDataService;

        public GameStageState(WorldGenerator.WorldGenerator worldGenerator, IStaticDataService staticDataService)
        {
            _worldGenerator = worldGenerator;
            _staticDataService = staticDataService;
        }

        public UniTask Enter(int stage)
        {
            StageConfig stageConfig = _staticDataService.GetStage((Stage)stage);
            _worldGenerator.SetTilesToGenerate(stageConfig.Tiles);

            return default;
        }

        public UniTask Exit()
        {
            throw new NotImplementedException();
        }
    }
}
