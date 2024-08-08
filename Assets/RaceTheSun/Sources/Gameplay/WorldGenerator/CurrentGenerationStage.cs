using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class CurrentGenerationStage
    {
        private const int StagesCount = 4;

        private readonly IStaticDataService _staticDataService;

        private int _currentStage;
        private AssetReferenceGameObject[] _tilesToGenerate;
        private bool _isBetweenStages;
        private int _currentTile;

        public CurrentGenerationStage(IStaticDataService staticDataService)
        {
            _currentStage = 0;
            _isBetweenStages = true;
            _currentTile = 0;
            _staticDataService = staticDataService;

            _tilesToGenerate = _staticDataService.GetStage(Stage.StartStage).Tiles;
        }

        public Stage GeneratedStageType { get; private set; }

        public AssetReferenceGameObject GetTile()
        {
            _currentTile++;

            if(_currentTile == _tilesToGenerate.Length)
            {
                UpdateTilesToGenerate();
                _currentTile = 0;
            }    

            return _tilesToGenerate[_currentTile];
        }

        public void FinishStage()
        {
            UpdateTilesToGenerate();
            _currentTile = 0;
        }

        public void SetBonusLevel()
        {
            _currentTile = 0;
            _isBetweenStages = false;
            GeneratedStageType = Stage.BonusStage;
            _tilesToGenerate = _staticDataService.GetStage(GeneratedStageType).Tiles;
        }

        public void EndBonusLevel()
        {
            _currentTile = 0;
            _isBetweenStages = false;
            UpdateTilesToGenerate();
        }

        private void UpdateTilesToGenerate()
        {
            StageConfig stageConfig;

            if (_isBetweenStages)
            {
                _isBetweenStages = false;
                _currentStage++;

                if (_currentStage > StagesCount)
                    _currentStage = 1;

                GeneratedStageType = (Stage)_currentStage;
                stageConfig = _staticDataService.GetStage(GeneratedStageType);
            }
            else
            {
                _isBetweenStages = true;
                GeneratedStageType = Stage.BetweenStages;
                stageConfig = _staticDataService.GetStage(GeneratedStageType);
            }

            _tilesToGenerate = stageConfig.Tiles;
        }
    }
}
