using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using Assets.RaceTheSun.Sources.Trail;
using Assets.RaceTheSun.Sources.Upgrading;
using System;
using System.Collections.Generic;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class SpaceshipData
    {
        public SpaceshipType Type;
        public StatData BatteryBoost;
        public StatData ExperienceMultiplierBoost;
        public StatData PickupRangeBoost;
        public StatData FloatTimeBoost;
        public bool IsUnlocked;
        public TrailType TrailType;
        public List<UpgradeType> UpgradeTypes;

        public SpaceshipData(SpaceshipType type, float batteryBoost, float experienceMultiplierBoost, float pickupRangeBoost, float floatTimeBoost, bool isUnlocked, int startBateryLevel, int startExperienceMultipllierLevel, int startPickUpRangeLevel, int startFloatTimeLevel)
        {
            Type = type;
            BatteryBoost = new StatData(StatType.Battery, batteryBoost, startBateryLevel);
            ExperienceMultiplierBoost = new StatData(StatType.ExperienceMultiplier, experienceMultiplierBoost, startExperienceMultipllierLevel);
            PickupRangeBoost = new StatData(StatType.PickUpRange, pickupRangeBoost, startPickUpRangeLevel);
            FloatTimeBoost = new StatData(StatType.FloatTime, floatTimeBoost, startFloatTimeLevel);
            IsUnlocked = isUnlocked;
            TrailType = TrailType.Basic;
            UpgradeTypes = new List<UpgradeType>();
        }

        public StatData GetStat(StatType statType)
        {
            switch (statType)
            {
                case StatType.Battery:
                    return BatteryBoost;
                case StatType.ExperienceMultiplier:
                    return ExperienceMultiplierBoost;
                case StatType.PickUpRange:
                    return PickupRangeBoost;
                case StatType.FloatTime:
                    return FloatTimeBoost;
                default:
                    return null;
            }
        }
    }
}