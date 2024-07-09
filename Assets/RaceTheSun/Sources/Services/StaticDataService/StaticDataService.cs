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

        public StaticDataService(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public async UniTask InitializeAsync()
        {
            List<UniTask> tasks = new List<UniTask>();

            tasks.Add(LoadStageConfigs());

            await UniTask.WhenAll(tasks);
        }

        public StageConfig GetStage(Stage stage) =>
            _stageConfigs.TryGetValue(stage, out StageConfig config) ? config : null;

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