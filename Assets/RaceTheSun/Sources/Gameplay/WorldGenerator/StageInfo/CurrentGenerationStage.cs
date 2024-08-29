using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.StageInfo
{
    public class CurrentGenerationStage
    {
        private const int StagesCount = 4;

        private readonly IStaticDataService _staticDataService;

        private int _currentStage;
        private AssetReferenceGameObject[] _tilesToGenerate;
        private bool _isBetweenStages;

        public CurrentGenerationStage(IStaticDataService staticDataService)
        {
            _currentStage = 0;
            _isBetweenStages = true;
            CurrentTile = 0;
            _staticDataService = staticDataService;
            CurrentStageNumber = 1;

            _tilesToGenerate = _staticDataService.GetStage(Stage.StartStage).Tiles;
        }

        public int CurrentTile { get; private set; }

        public Stage GeneratedStageType { get; private set; }
        public int CurrentStageNumber { get; private set; }

        public AssetReferenceGameObject GetTile()
        {
            CurrentTile++;

            if (CurrentTile == _tilesToGenerate.Length)
            {
                UpdateTilesToGenerate();
                CurrentTile = 0;
            }

            return _tilesToGenerate[CurrentTile];
        }

        public void FinishStage()
        {
            UpdateTilesToGenerate();
            CurrentTile = 0;
        }

        public void SetBonusLevel()
        {
            CurrentTile = 0;
            _isBetweenStages = false;
            GeneratedStageType = Stage.BonusStage;
            _tilesToGenerate = _staticDataService.GetStage(GeneratedStageType).Tiles;
        }

        public void EndBonusLevel()
        {
            CurrentTile = 0;
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
                CurrentStageNumber++;
                stageConfig = _staticDataService.GetStage(GeneratedStageType);
            }

            _tilesToGenerate = stageConfig.Tiles;
        }
    }
}
