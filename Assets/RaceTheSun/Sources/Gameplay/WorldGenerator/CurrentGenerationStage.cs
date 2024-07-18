using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using System;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class CurrentGenerationStage
    {
        private const int StagesCount = 1;

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
            _tilesToGenerate = _staticDataService.GetStage(Stage.BonusStage).Tiles;
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

                stageConfig = _staticDataService.GetStage((Stage)_currentStage);
            }
            else
            {
                _isBetweenStages = true;
                stageConfig = _staticDataService.GetStage(Stage.BetweenStages);
            }

            _tilesToGenerate = stageConfig.Tiles;
        }
    }
}
