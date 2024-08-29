using System;
using Assets.RaceTheSun.Sources.MainMenu;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class StatData
    {
        public StatType Type;
        public float Value;
        public int Level;

        public StatData(StatType type, float value, int startLevel)
        {
            Level = startLevel;
            Type = type;
            Value = value;
        }
    }
}