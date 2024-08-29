﻿using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.UI.GameOverPanel;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameplayResultState : IState
    {
        private const float ExperienceMultipllier = 0.0008f;

        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly ResultPanel _resultPanel;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly Counters.ScoreCounter _scoreCounter;

        public GameplayResultState(GameplayStateMachine gameplayStateMachine, ResultPanel resultPanel, IPersistentProgressService persistentProgressService, Counters.ScoreCounter scoreCounter)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _resultPanel = resultPanel;
            _persistentProgressService = persistentProgressService;
            _scoreCounter = scoreCounter;

            resultPanel.gameObject.SetActive(false);
        }

        public UniTask Enter()
        {
            _resultPanel.Open();
            _persistentProgressService.Progress.LevelProgress.UpdateExperience(GetExperience());
            _resultPanel.Hided += OnResultPanelHided;
            return default;
        }

        public UniTask Exit()
        {
            _resultPanel.Hided -= OnResultPanelHided;
            return default;
        }

        private int GetExperience() =>
            (int)(_scoreCounter.Score * ExperienceMultipllier * _persistentProgressService.Progress.AvailableSpaceships.GetCurrentSpaceshipData().ExperienceMultiplier.Value);

        private void OnResultPanelHided() =>
            _gameplayStateMachine.Enter<GameplayEndState>().Forget();
    }
}
