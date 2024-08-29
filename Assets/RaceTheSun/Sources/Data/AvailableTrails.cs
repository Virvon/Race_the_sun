using System;
using System.Collections.Generic;
using Assets.RaceTheSun.Sources.GameLogic.Trail;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class AvailableTrails
    {
        public List<TrailType> UnlockedTrails;

        public AvailableTrails(List<TrailType> trails) =>
            UnlockedTrails = trails;

        public bool IsUnlocked(TrailType type) =>
            UnlockedTrails.Contains(type);
    }
}