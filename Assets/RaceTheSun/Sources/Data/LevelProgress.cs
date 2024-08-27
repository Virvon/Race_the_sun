using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class LevelProgress
    {
        public const int MaxLevel = 10;
        public const int ExperienceToLevelUp = 1000;

        public UpgradingData UpgradingData;
        public int Experience;
        public int Level;
        public int LastShowedLevel;

        public event Action ExperienceCountChanged;

        public LevelProgress(UpgradingData upgradingData)
        {
            UpgradingData = upgradingData;

            Experience = 0;
            Level = 1;
            LastShowedLevel = 1;
        }

        public bool IsMaxLevel => Level >= MaxLevel;

        public void UpdateExperience(int count)
        {
            if (Level >= MaxLevel)
                return;

            Experience += count;

            if(Experience >= ExperienceToLevelUp)
            {
                int levelsCount = Experience / ExperienceToLevelUp;

                levelsCount = levelsCount + Level <= MaxLevel ? levelsCount : MaxLevel - Level;

                Level += levelsCount;
                Experience %= ExperienceToLevelUp;

                UpgradingData.Upgrade(Level);

                for(int i = Level - levelsCount + 1; i <= Level; i++)
                    UpgradingData.Upgrade(i);
            }

            ExperienceCountChanged?.Invoke();
        }
    }
}