using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void OnEnable() =>
            _button.onClick.AddListener(EnterGameLoopState);

        private void OnDisable() =>
            _button.onClick.RemoveListener(EnterGameLoopState);

        private async void EnterGameLoopState() =>
            await _gameStateMachine.Enter<GameLoopState>();
    }
}
