using UnityEngine;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "MysteryBoxRewardsConfig", menuName = "StaticData/Create new mystery box rewards config", order = 51)]
    public class MysteryBoxRewardsConfig : ScriptableObject
    {
        public int MinScoreItemsRewardChance;
        public int MinScoreItemsRewardMinCount;
        public int MinScoreItemsRewardMaxCount;

        public int MaxScoreItemsRewardChance;
        public int MaxScoreItemsRewardMinCount;
        public int MaxScoreItemsRewardMaxCount;

        public int ExperienceRewardChance;
    }
}
