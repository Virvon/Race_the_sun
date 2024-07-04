using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Wallet Wallet;

        public PlayerProgress()
        {
            Wallet = new();
        }
    }
}