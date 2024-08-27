using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Assets.RaceTheSun.Sources.Trail;
using Assets.RaceTheSun.Sources.Upgrading;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        StageConfig GetStage(Stage stage);
        SpaceshipConfig GetSpaceship(SpaceshipType type);
        SpaceshipConfig[] GetSpaceships();
        GameplayWorldConfig GetGameplayWorld();
        TrailConfig GetTrail(TrailType type);
        TrailConfig[] GetTrails();
        MysteryBoxRewardsConfig GetMysteryBoxRewards();
        AttachmentConfig GetAttachment(UpgradeType type);
        LevelUnclockInfoConfig GetLevelUnlockInfo(int level);
    }
}