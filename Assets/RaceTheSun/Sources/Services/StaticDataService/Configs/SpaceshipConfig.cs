﻿using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [CreateAssetMenu(fileName = "SpaceshipConfig", menuName = "StaticData/Create new spaceship config", order = 51)]
    public class SpaceshipConfig : ScriptableObject
    {
        public SpaceshipType Type;
        public AssetReferenceGameObject ModelPrefabReference;

        public bool IsUnlockedOnStart;
        public int BuyCost;

        public StatConfig Battery;
        public StatConfig ExperienceMultiplier;
        public StatConfig PickUpRange;
        public StatConfig FloatTime;

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
