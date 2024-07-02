using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter(string payload)
        {
            _loadingCurtain.Show();
            await _sceneLoader.Load(payload);

            await InitGameWorld();

            _stateMachine.Enter<GameLoopState>().Forget();
        }

#pragma warning disable CS1998
        public async UniTask Exit() =>
            _loadingCurtain.Hide();

        private async UniTask InitGameWorld()
        {

        }
    }
}
