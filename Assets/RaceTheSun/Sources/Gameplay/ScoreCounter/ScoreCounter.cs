using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.ScoreCounter
{
    public class ScoreCounter : ITickable
    {
        private const int ScorePerSecond = 100;
        private const int ScoreItemsToMultiplyProgress = 5;
        private const float SpeedMultiplier = 0.0032f;

        private float _score;
        private int _multiplier;
        private int _multiplierProgress;
        private Spaceship.Spaceship _spaceship;
        public ScoreCounter()
        {
            _score = 0;
            _multiplier = 1;
        }

        public event Action<int> ScoreCountChanged;
        public event Action<int> MultiplierChanged;
        public event Action<int> MultiplierProgressChanged;

        public void Init(Spaceship.Spaceship spaceship)
        {
            _spaceship = spaceship;
        }

        public void Tick()
        {
            if (_spaceship == null)
                return;

            _score += ScorePerSecond * Time.deltaTime * _multiplier * (_spaceship.Speed * SpeedMultiplier);

            ScoreCountChanged?.Invoke((int)_score);
        }

        public void TakeItem()
        {
            _multiplierProgress++;
            MultiplierProgressChanged?.Invoke(_multiplierProgress);

            if(_multiplierProgress == ScoreItemsToMultiplyProgress)
            {
                _multiplier++;
                _multiplierProgress = 0;

                MultiplierChanged?.Invoke(_multiplier);
                MultiplierProgressChanged?.Invoke(_multiplierProgress);
            }
        }
    }
}
