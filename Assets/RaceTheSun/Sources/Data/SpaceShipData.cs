using System;
using System.Collections.Generic;
using Assets.RaceTheSun.Sources.GameLogic.Trail;
using Assets.RaceTheSun.Sources.MainMenu;
using Assets.RaceTheSun.Sources.Upgrading;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class SpaceshipData
    {
        public SpaceshipType Type;
        public StatData Battery;
        public StatData ExperienceMultiplier;
        public StatData PickUpRange;
        public StatData FloatTime;
        public bool IsUnlocked;
        public TrailType TrailType;
        public List<UpgradeType> UpgradeTypes;
        public int Level;

        public SpaceshipData(
            SpaceshipType type,
            float batteryValue,
            float experienceMultiplierValue,
            float pickupRangeValue,
            float floatTimeValue,
            bool isUnlocked,
            int startBateryLevel,
            int startExperienceMultipllierLevel,
            int startPickUpRangeLevel,
            int startFloatTimeLevel,
            int startSpaceshipLevel)
        {
            Type = type;
            Battery = new StatData(StatType.Battery, batteryValue, startBateryLevel);
            ExperienceMultiplier = new StatData(StatType.ExperienceMultiplier, experienceMultiplierValue, startExperienceMultipllierLevel);
            PickUpRange = new StatData(StatType.PickUpRange, pickupRangeValue, startPickUpRangeLevel);
            FloatTime = new StatData(StatType.FloatTime, floatTimeValue, startFloatTimeLevel);
            IsUnlocked = isUnlocked;
            TrailType = TrailType.Basic;
            UpgradeTypes = new List<UpgradeType>();
            Level = startSpaceshipLevel;
        }

        public StatData GetStat(StatType statType)
        {
            switch (statType)
            {
                case StatType.Battery:
                    return Battery;
                case StatType.ExperienceMultiplier:
                    return ExperienceMultiplier;
                case StatType.PickUpRange:
                    return PickUpRange;
                case StatType.FloatTime:
                    return FloatTime;
                default:
                    return null;
            }
        }
    }
}