using Assets.MyBakery.Sources.Services.StaticData;
using Assets.RaceTheSun.Sources.Infrastructure;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Assets.MyBakery.Sources.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assets;

        public StaticDataService(IAssetProvider assets)
        {
            _assets = assets;
        }

        private Dictionary<string, LevelStaticData> _levels;

        public async UniTask InitializeAsync()
        {
            List<UniTask> tasks = new List<UniTask>();

            tasks.Add(LoadLevelStaticDatas());

            await UniTask.WhenAll(tasks);
        }

        public LevelStaticData GetLevelStaticData(string sceneName) =>
            _levels.TryGetValue(sceneName, out LevelStaticData staticData) ? staticData : null;

        private async UniTask LoadLevelStaticDatas()
        {
            LevelStaticData[] configs = await GetConfigs<LevelStaticData>();
            _levels = configs.ToDictionary(config => config.LevelKey, configs => configs);
        }

        private async UniTask<TConfig[]> GetConfigs<TConfig>() where TConfig : class
        {
            List<string> keys = await GetConfigKeys<TConfig>();
            return await _assets.LoadAll<TConfig>(keys);
        }

        private async UniTask<List<string>> GetConfigKeys<TConfig>() =>
            await _assets.GetAssetsListByLabel<TConfig>(AssetLabels.Configs);
    }
}