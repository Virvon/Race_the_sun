using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.GameLogic.Trail;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Assets.RaceTheSun.Sources.Upgrading;

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