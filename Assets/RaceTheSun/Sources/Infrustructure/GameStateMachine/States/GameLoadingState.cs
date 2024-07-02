using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States
{
    public class GameLoadingState : IState
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        public GameLoadingState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader)
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter()
        {
            _loadingCurtain.Show();

            await _sceneLoader.Load(InfrasructureAssetPath.GameLoadingScene);

            _loadingCurtain.Hide();
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
