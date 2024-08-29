using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.GameLogic.Trail;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Assets.RaceTheSun.Sources.Upgrading;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetsProvider;

        private Dictionary<Stage, StageConfig> _stageConfigs;
        private Dictionary<SpaceshipType, SpaceshipConfig> _spaceshipConfigs;
        private GameplayWorldConfig _gameplayWorldConfig;
        private Dictionary<TrailType, TrailConfig> _trailConfigs;
        private MysteryBoxRewardsConfig _mysteryBoxRewardsConfig;
        private Dictionary<UpgradeType, AttachmentConfig> _attachmentConfigs;
        private Dictionary<int, LevelUnclockInfoConfig> _levelUnlockInfoConfigs;

        public StaticDataService(IAssetProvider assetsProvider) =>
            _assetsProvider = assetsProvider;

        public async UniTask InitializeAsync()
        {
            List<UniTask> tasks = new List<UniTask>();

            tasks.Add(LoadGameplayWorldConfig());
            tasks.Add(LoadSpaceshipConfigs());
            tasks.Add(LoadTrailConfigs());
            tasks.Add(LoadMysteryBoxRewardsConfig());
            tasks.Add(LoadAttachmentConfigs());
            tasks.Add(LoadLevelUnlockInfoConfigs());

            await UniTask.WhenAll(tasks);
        }

        public LevelUnclockInfoConfig GetLevelUnlockInfo(int level) =>
            _levelUnlockInfoConfigs.TryGetValue(level, out LevelUnclockInfoConfig config) ? config : null;

        public AttachmentConfig GetAttachment(UpgradeType type) =>
            _attachmentConfigs.TryGetValue(type, out AttachmentConfig config) ? config : null;

        public MysteryBoxRewardsConfig GetMysteryBoxRewards() =>
            _mysteryBoxRewardsConfig;

        public TrailConfig[] GetTrails() =>
            _trailConfigs.Values.ToArray();

        public TrailConfig GetTrail(TrailType type) =>
            _trailConfigs.TryGetValue(type, out TrailConfig config) ? config : null;

        public GameplayWorldConfig GetGameplayWorld() =>
            _gameplayWorldConfig;

        public SpaceshipConfig[] GetSpaceships() =>
            _spaceshipConfigs.Values.ToArray();

        public SpaceshipConfig GetSpaceship(SpaceshipType type) =>
            _spaceshipConfigs.TryGetValue(type, out SpaceshipConfig config) ? config : null;

        public StageConfig GetStage(Stage stage) =>
            _stageConfigs.TryGetValue(stage, out StageConfig config) ? config : null;

        private async UniTask LoadMysteryBoxRewardsConfig()
        {
            MysteryBoxRewardsConfig[] configs = await GetConfigs<MysteryBoxRewardsConfig>();
            _mysteryBoxRewardsConfig = configs.First();
        }

        private async UniTask LoadLevelUnlockInfoConfigs()
        {
            LevelsConfig[] levelConfigs = await GetConfigs<LevelsConfig>();
            _levelUnlockInfoConfigs = levelConfigs.First().LevelUnclockInfoConfigs.ToDictionary(value => value.Level, value => value);
        }

        private async UniTask LoadAttachmentConfigs()
        {
            AttachmentConfig[] attachmentConfigs = await GetConfigs<AttachmentConfig>();

            _attachmentConfigs = attachmentConfigs.ToDictionary(value => value.AttachmentUpgradeType, value => value);
        }

        private async UniTask LoadSpaceshipConfigs()
        {
            SpaceshipConfig[] spaceshipConfigs = await GetConfigs<SpaceshipConfig>();

            _spaceshipConfigs = spaceshipConfigs.ToDictionary(value => value.Type, value => value);
        }

        private async UniTask LoadTrailConfigs()
        {
            TrailConfig[] trailConfigs = await GetConfigs<TrailConfig>();

            _trailConfigs = trailConfigs.ToDictionary(value => value.Type, value => value);
        }

        private async UniTask LoadGameplayWorldConfig()
        {
            GameplayWorldConfig[] gameplayWorldConfigs = await GetConfigs<GameplayWorldConfig>();
            _gameplayWorldConfig = gameplayWorldConfigs.First();

            _stageConfigs = _gameplayWorldConfig.StageConfigs.ToDictionary(value => value.Stage, value => value);
        }

        private async UniTask<TConfig[]> GetConfigs<TConfig>()
            where TConfig : class
        {
            List<string> keys = await GetConfigKeys<TConfig>();
            return await _assetsProvider.LoadAll<TConfig>(keys);
        }

        private async UniTask<List<string>> GetConfigKeys<TConfig>() =>
            await _assetsProvider.GetAssetsListByLabel<TConfig>(AssetLabels.Configs);
    }
}