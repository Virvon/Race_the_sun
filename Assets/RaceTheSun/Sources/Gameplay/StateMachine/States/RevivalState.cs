using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.UI.GameOverPanel;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class RevivalState : IState
    {
        private readonly RevivalPanel _revivalPanel;
        private readonly StartMovement _startMovement;
        private readonly GameplayStateMachine _gameplayStateMachine;

        private bool _isRevivalTryed;

        public RevivalState(RevivalPanel revivalPanel, StartMovement startMovement, GameplayStateMachine gameplayStateMachine)
        {
            _revivalPanel = revivalPanel;

            _isRevivalTryed = false;
            _startMovement = startMovement;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public async UniTask Enter()
        {
            _revivalPanel.RevivalButtonClicked += OnRevivalButtonClicked;
            _revivalPanel.RevivalTimeEnded += OnRevivalTimeEnded;

            if(_isRevivalTryed == false)
            {
                _revivalPanel.Open();
                _isRevivalTryed = true;
            }
            else
            {
                _gameplayStateMachine.Enter<ResultState>().Forget();
            }
        }

        public UniTask Exit()
        {
            _revivalPanel.RevivalButtonClicked -= OnRevivalButtonClicked;
            _revivalPanel.RevivalTimeEnded -= OnRevivalTimeEnded;

            return default;
        }

        private void OnRevivalTimeEnded()
        {
            _revivalPanel.Hide(callback: () => _gameplayStateMachine.Enter<ResultState>().Forget());
        }

        private void OnRevivalButtonClicked()
        {
            _gameplayStateMachine.Enter<GameLoopState>().Forget();
            _startMovement.MoveUp();
            _revivalPanel.Hide();
        }
    }
}
