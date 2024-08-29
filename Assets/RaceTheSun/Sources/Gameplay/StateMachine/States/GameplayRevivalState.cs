using Cysharp.Threading.Tasks;
using Agava.YandexGames;
using Assets.RaceTheSun.Sources.Gameplay.Portals;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.UI.GameOverPanel;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameplayRevivalState : IState
    {
        private readonly RevivalPanel _revivalPanel;
        private readonly CutSceneMovement _startMovement;
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly SpaceshipShieldPortal _spaceshipShieldPortal;
        private readonly Sun.Sun _sun;

        private bool _isRevivalTryed;

        public GameplayRevivalState(
            RevivalPanel revivalPanel,
            CutSceneMovement startMovement,
            GameplayStateMachine gameplayStateMachine,
            SpaceshipShieldPortal spaceshipShieldPortal,
            Sun.Sun sun)
        {
            _revivalPanel = revivalPanel;

            _isRevivalTryed = false;
            _startMovement = startMovement;
            _gameplayStateMachine = gameplayStateMachine;
            _spaceshipShieldPortal = spaceshipShieldPortal;
            _sun = sun;
        }

        public UniTask Enter()
        {
            _revivalPanel.RevivalButtonClicked += OnRevivalButtonClicked;
            _revivalPanel.RevivalTimeEnded += OnRevivalTimeEnded;

            if (_isRevivalTryed == false)
            {
                _revivalPanel.Open();
                _isRevivalTryed = true;
            }
            else
            {
                _gameplayStateMachine.Enter<GameplayResultState>().Forget();
            }

            return default;
        }

        public UniTask Exit()
        {
            _revivalPanel.RevivalButtonClicked -= OnRevivalButtonClicked;
            _revivalPanel.RevivalTimeEnded -= OnRevivalTimeEnded;

            return default;
        }

        private void OnRevivalTimeEnded()
        {
            _revivalPanel.Hide(callback: () => _gameplayStateMachine.Enter<GameplayResultState>().Forget());
        }

        private void OnRevivalButtonClicked()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            InterstitialAd.Show(onCloseCallback: (_) =>
            {
                _gameplayStateMachine.Enter<GameLoopState>().Forget();
                _spaceshipShieldPortal.Activate(false);
                _revivalPanel.Hide();
                _sun.IsStopped = false;
            });
#else
            _gameplayStateMachine.Enter<GameplayLoopState>().Forget();
            _spaceshipShieldPortal.Activate(false);
            _revivalPanel.Hide();
            _sun.IsStopped = false;
#endif
        }
    }
}
