using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
using Assets.RaceTheSun.Sources.Services.TimeScale;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud.PausePanel
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

        private void OnEnable() =>
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick()
        {
            _gameplayStateMachine.Enter<GameplayEndState>().Forget();
            _timeScale.Scale(TimeScaleType.Normal);
        }
    }
}
