using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService.Configs
{
    [Serializable]
    public class StatConfig
    {
        public StatType Type;
        public float StartValue;
        public float StartBoost;
        public float MaxBoost;
    }
}
