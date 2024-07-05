using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Wallet Wallet;
        public AvailableStatsToUpgrade AvailableStatsToUpgrade;

        public PlayerProgress()
        {
            Wallet = new();
            AvailableStatsToUpgrade = new ();
        }
    }
}