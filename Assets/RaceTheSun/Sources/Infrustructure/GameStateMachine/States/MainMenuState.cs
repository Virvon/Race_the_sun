using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States
{
    public class MainMenuState : IState
    {
        private readonly ILoadingCurtain _loadingCurtainProxy;
        private readonly ISceneLoader _sceneLoader;

        public MainMenuState(ILoadingCurtain loadingCurtainProxy, ISceneLoader sceneLoader)
        {
            _loadingCurtainProxy = loadingCurtainProxy;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter()
        {
            _loadingCurtainProxy.Show();

            await _sceneLoader.Load(InfrasructureAssetPath.MainMenuScene);

            _loadingCurtainProxy.Hide();
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
