using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class SpaceShipData
    {
        public float BatteryTime;
        public float ExperienceMultiplier;
        public float PickupRange;
        public float FloatTime;

        public SpaceShipData(float batteryTime, float experienceMultiplier, float pickupRange, float floatTime)
        {
            BatteryTime = batteryTime;
            ExperienceMultiplier = experienceMultiplier;
            PickupRange = pickupRange;
            FloatTime = floatTime;
        }
    }
}