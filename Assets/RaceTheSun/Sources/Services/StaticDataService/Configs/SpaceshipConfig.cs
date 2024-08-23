using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "SpaceshipConfig", menuName = "StaticData/Create new spaceship config", order = 51)]
    public class SpaceshipConfig : ScriptableObject
    {
        public SpaceshipType Type;
        public AssetReferenceGameObject ModelPrefabReference;

        public StatType UnlockedStatType;

        public bool IsUnlockedOnStart;
        public int BuyCost;

        public List<BatteryMaterialInfo> BatteryMaterialsInfo;
        public Material ChargedBatteryMaterial;
        public Material DischargedBatteryMaterial;
        public Material LowBatteryMaterial;

        public StatConfig Battery;
        public StatConfig ExperienceMultiplier;
        public StatConfig PickUpRange;
        public StatConfig FloatTime;

        public string Name;
        public string Title;
        public string UnlockText;

        public StatConfig GetStat(StatType statType)
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
