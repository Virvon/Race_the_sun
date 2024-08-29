using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
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
        private readonly IGameplayFactory _gameplayFactory;

        public event Action<int> ScoreCountChanged;

        public ScoreCounter(MultiplierProgressCounter progressMultiplierCounter, IGameplayFactory gameplayFactory)
        {
            _progressMultiplierCounter = progressMultiplierCounter;
            _gameplayFactory = gameplayFactory;

            Score = 0;
        }

        public float Score { get; private set; }

        public void Tick()
        {
            if (_gameplayFactory.Spaceship == null)
                return;

            Score += ScorePerSecond * Time.deltaTime * _progressMultiplierCounter.Multiplier * (_gameplayFactory.Spaceship.Speed * SpeedMultiplier);

            ScoreCountChanged?.Invoke((int)Score);
        }
    }
}
