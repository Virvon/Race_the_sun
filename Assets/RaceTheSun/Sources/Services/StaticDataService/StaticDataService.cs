using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Infrastructure;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetsProvider;
        
        private Dictionary<Stage, StageConfig> _stageConfigs;
        private Dictionary<SpaceshipType, SpaceshipConfig> _spaceshipConfigs;

        public StaticDataService(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public async UniTask InitializeAsync()
        {
            List<UniTask> tasks = new List<UniTask>();

            tasks.Add(LoadStageConfigs());
            tasks.Add(LoadSpaceshipConfigs());

            await UniTask.WhenAll(tasks);
        }

        public SpaceshipConfig[] GetSpaceships() =>
            _spaceshipConfigs.Values.ToArray();

        public SpaceshipConfig GetSpaceship(SpaceshipType type) =>
            _spaceshipConfigs.TryGetValue(type, out SpaceshipConfig config) ? config : null;

        public StageConfig GetStage(Stage stage) =>
            _stageConfigs.TryGetValue(stage, out StageConfig config) ? config : null;

        private async UniTask LoadSpaceshipConfigs()
        {
            SpaceshipConfig[] spaceshipConfigs = await GetConfigs<SpaceshipConfig>();

            _spaceshipConfigs = spaceshipConfigs.ToDictionary(value => value.Type, value => value);
        }

        private async UniTask LoadStageConfigs()
        {
            LevelConfig[] levelConfigs = await GetConfigs<LevelConfig>();
            LevelConfig levelConfig = levelConfigs.First();

            _stageConfigs = levelConfig.StageConfigs.ToDictionary(value => value.Stage, value => value);
        }

        private async UniTask<TConfig[]> GetConfigs<TConfig>() where TConfig : class
        {
            List<string> keys = await GetConfigKeys<TConfig>();
            return await _assetsProvider.LoadAll<TConfig>(keys);
        }

        private async UniTask<List<string>> GetConfigKeys<TConfig>() =>
            await _assetsProvider.GetAssetsListByLabel<TConfig>(AssetLabels.Configs);
    }
}