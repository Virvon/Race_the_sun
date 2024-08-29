using Assets.RaceTheSun.Sources.Infrastructure;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.Infrustructure.AssetManagement;
using Assets.RaceTheSun.Sources.Services.SaveLoad;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrustructure.GameStateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly ILoadingCurtain _loadingCurtainProxy;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadService _saveLoadService;

        public GameLoopState(ILoadingCurtain loadingCurtainProxy, ISceneLoader sceneLoader, IAssetProvider assetProvider, ISaveLoadService saveLoadService)
        {
            _loadingCurtainProxy = loadingCurtainProxy;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
        }

        public async UniTask Enter()
        {
            _loadingCurtainProxy.Show();

            await _sceneLoader.Load(InfrasructureAssetPath.GameplayScene);
        }

        public UniTask Exit()
        {
            _assetProvider.CleanUp();
            _saveLoadService.SaveProgress();
            return default;
        }
    }
}
