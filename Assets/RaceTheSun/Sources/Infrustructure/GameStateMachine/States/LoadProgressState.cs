using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrustructure.GameStateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly Infrastructure.GameStateMachine.GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(Infrastructure.GameStateMachine.GameStateMachine stateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = stateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public UniTask Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<MainMenuState>().Forget();

            return default;
        }

        public UniTask Exit()
        {
            return default;
        }

        private void LoadProgressOrInitNew() =>
            _persistentProgressService.Progress = _saveLoadService.LoadProgress() ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress()
        {
            PlayerProgress progress = new();

            return progress;
        }
    }
}