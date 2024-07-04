using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class Wallet
    {
        public int Value;

        public event Action<int> ValueChanged;

        public void Take(int value)
        {
            Value += value;
            ValueChanged?.Invoke(Value);
        }
    }
}