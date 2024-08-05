using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class LevelProgress
    {
        public const int MaxLevel = 10;
        public const int ExperienceToLevelUp = 1000;

        private readonly UpgradingData _upgradingData;

        public int Experience;
        public int Level;

        public LevelProgress(UpgradingData upgradingData)
        {
            _upgradingData = upgradingData;

            Experience = 0;
            Level = 1;
        }

        public bool IsMaxLevel => Level >= MaxLevel;

        public void UpdateExperience(int count)
        {
            if (Level >= MaxLevel)
                return;

            Experience += count;

            if(Experience >= ExperienceToLevelUp)
            {
                Level++;
                Experience -= ExperienceToLevelUp;

                _upgradingData.Upgrade(Level);
            }
        }
    }
}