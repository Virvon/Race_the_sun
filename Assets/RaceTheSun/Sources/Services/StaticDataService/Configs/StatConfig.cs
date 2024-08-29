﻿using Assets.RaceTheSun.Sources.MainMenu;
using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [Serializable]
    public class StatConfig
    {
        public StatType Type;
        public float StartValue;
        public int StartLevel;
        public int MaxLevel;
        public float UpgradeValue;
    }
}
