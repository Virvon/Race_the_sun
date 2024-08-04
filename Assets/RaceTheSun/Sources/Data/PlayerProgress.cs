using System;
using System.Collections.Generic;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Wallet Wallet;
        public AvailableStatsToUpgrade AvailableStatsToUpgrade;
        public AvailableSpaceships AvailableSpaceships;

        public PlayerProgress(List<SpaceshipData> spaceshipDatas)
        {
            Wallet = new();
            AvailableStatsToUpgrade = new();
            AvailableSpaceships = new(spaceshipDatas);
        }
    }
}