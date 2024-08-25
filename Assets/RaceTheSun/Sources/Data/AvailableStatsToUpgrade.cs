using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable] 
    public class AvailableStatsToUpgrade
    {
        public List<StatType> Stats;

        public AvailableStatsToUpgrade() =>
            Stats = new();

        public void Add(StatType stat)
        {
            if (CheckAvailability(stat))
                return;

            Stats.Add(stat);
        }

        public bool CheckAvailability(StatType stat) =>
            Stats.Any(value => value == stat);
    }
}