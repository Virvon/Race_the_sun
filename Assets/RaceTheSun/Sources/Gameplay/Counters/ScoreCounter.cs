using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Counters
{
    public class ScoreCounter : ITickable
    {
        private const int ScorePerSecond = 750;
        private const float SpeedMultiplier = 0.0032f;

        private readonly MultiplierProgressCounter _progressMultiplierCounter;

        private Spaceship.Spaceship _spaceship;

        public event Action<int> ScoreCountChanged;

        public ScoreCounter(MultiplierProgressCounter progressMultiplierCounter)

        {
            _progressMultiplierCounter = progressMultiplierCounter;

            Score = 0;
        }

        public float Score { get; private set; }

        public void Init(Spaceship.Spaceship spaceship)
        {
            _spaceship = spaceship;
        }

        public void Tick()
        {
            if (_spaceship == null)
                return;

            Score += ScorePerSecond * Time.deltaTime * _progressMultiplierCounter.Multiplier * (_spaceship.Speed * SpeedMultiplier);

            ScoreCountChanged?.Invoke((int)Score);
        }


    }
}
