using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.UI.GameOverPanel;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class ResultState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly ResultPanel _resultPanel;

        public ResultState(GameplayStateMachine gameplayStateMachine, ResultPanel resultPanel)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _resultPanel = resultPanel;
        }

        public UniTask Enter()
        {
            _resultPanel.Open();
            _resultPanel.Hided += OnResultPanelHided;
            return default;
        }

        public UniTask Exit()
        {
            _resultPanel.Hided -= OnResultPanelHided;
            return default;
        }

        private void OnResultPanelHided()
        {
            _gameplayStateMachine.Enter<GameEndState>().Forget();
        }
    }
}
