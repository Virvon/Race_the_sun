using System;

namespace Assets.RaceTheSun.Sources.Gameplay.ScoreCounter
{
    public class MultiplierProgressCounter
    {
        private const int ScoreItemsToMultiplyProgress = 5;

        private int _scoreItemsProgress;
        

        public event Action<int> MultiplierChanged;
        public event Action<int> MultiplierProgressChanged;

        public MultiplierProgressCounter()
        {
            _scoreItemsProgress = 0;
            Multiplier = 1;
        }

        public int Multiplier { get; private set; }

        public void GiveScoreItem()
        {
            _scoreItemsProgress++;
            MultiplierProgressChanged?.Invoke(_scoreItemsProgress);

            if (_scoreItemsProgress == ScoreItemsToMultiplyProgress)
            {
                Multiplier++;
                _scoreItemsProgress = 0;

                MultiplierChanged?.Invoke(Multiplier);
                MultiplierProgressChanged?.Invoke(_scoreItemsProgress);
            }
        }
    }
}
