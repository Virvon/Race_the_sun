using Assets.RaceTheSun.Sources.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;
using Assets.RaceTheSun.Sources.Services.TimeScale;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private GameplayStateMachine _gameplayStateMachine;
        private ITimeScale _timeScale;

        [Inject]
        private void Construct(GameplayStateMachine gameplayStateMachine, ITimeScale timeScale)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _timeScale = timeScale;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _gameplayStateMachine.Enter<GameEndState>().Forget();
            _timeScale.Scale(TimeScaleType.Normal);
        }
    }
}
