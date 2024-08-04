using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class StatData
    {
        public StatType Type;
        public float Value;

        public StatData(StatType type, float value)
        {
            Type = type;
            Value = value;
        }
    }
}