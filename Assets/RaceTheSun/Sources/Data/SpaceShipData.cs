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

        public SpaceshipData(SpaceshipType type, float batteryBoost, float experienceMultiplierBoost, float pickupRangeBoost, float floatTimeBoost, bool isUnlocked)
        {
            Type = type;
            BatteryBoost = new StatData(StatType.Battery, batteryBoost);
            ExperienceMultiplierBoost = new StatData(StatType.ExperienceMultiplier, experienceMultiplierBoost);
            PickupRangeBoost = new StatData(StatType.PickUpRange, pickupRangeBoost);
            FloatTimeBoost = new StatData(StatType.FloatTime, floatTimeBoost);
            IsUnlocked = isUnlocked;
            TrailType = TrailType.Basic;
            UpgradeTypes = new List<UpgradeType>();
        }

        public float GetStat(StatType statType)
        {
            switch (statType)
            {
                case StatType.Battery:
                    return BatteryBoost.Value;
                case StatType.ExperienceMultiplier:
                    return ExperienceMultiplierBoost.Value;
                case StatType.PickUpRange:
                    return PickupRangeBoost.Value;
                case StatType.FloatTime:
                    return FloatTimeBoost.Value;
                default:
                    return 0;
            }
        }
    }
}