using Assets.RaceTheSun.Sources.Infrastructure;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetsProvider;

        public StaticDataService(IAssetProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public async UniTask InitializeAsync()
        {
            List<UniTask> tasks = new List<UniTask>();

            tasks.Add(LoadWindowConfigs());

            await UniTask.WhenAll(tasks);
        }

        public WindowConfig GetWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out var config) ? config : null;

        private async UniTask LoadWindowConfigs()
        {
            WindowStaticData[] windowStaticDatas = await GetConfigs<WindowStaticData>();
            WindowStaticData windowStaticData = windowStaticDatas.First();

            _windowConfigs = windowStaticData.Configs.ToDictionary(value => value.WindowId, value => value);
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