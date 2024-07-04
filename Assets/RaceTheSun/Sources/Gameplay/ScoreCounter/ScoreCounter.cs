using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.ScoreCounter
{
    public class ScoreCounter : ITickable
    {
        private const int ScorePerSecond = 100;

        private float _score;
        private int _multiplier;
        private int _multiplierProgress;

        public ScoreCounter()
        {
            _score = 0;
            _multiplier = 1;
        }

        public event Action<int> ScoreCountChanged;
        public event Action<int> MultiplierChanged;
        public event Action<int> MultiplierProgressChanged;

        public void Tick()
        {
            _score += ScorePerSecond * Time.deltaTime * _multiplier;

            ScoreCountChanged?.Invoke((int)_score);
        }

        public void TakeItem()
        {
            _multiplierProgress++;
            MultiplierProgressChanged?.Invoke(_multiplierProgress);

            if(_multiplierProgress == 5)
            {
                _multiplier++;
                _multiplierProgress = 0;

                MultiplierChanged?.Invoke(_multiplier);
                MultiplierProgressChanged?.Invoke(_multiplierProgress);
            }
        }
    }
}
