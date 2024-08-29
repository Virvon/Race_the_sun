using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.Services.SaveLoad;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States
{
    public class MainMenuState : IState
    {
        private readonly ILoadingCurtain _loadingCurtainProxy;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetProvider _assetProvider;

        public MainMenuState(
            ILoadingCurtain loadingCurtainProxy,
            ISceneLoader sceneLoader,
            ISaveLoadService saveLoadService,
            IAssetProvider assetProvider)
        {
            _loadingCurtainProxy = loadingCurtainProxy;
            _sceneLoader = sceneLoader;
            _saveLoadService = saveLoadService;
            _assetProvider = assetProvider;
        }

        public async UniTask Enter()
        {
            _loadingCurtainProxy.Show();

            await _sceneLoader.Load(InfrasructureAssetPath.MainMenuScene);
        }

        public UniTask Exit()
        {
            _assetProvider.CleanUp();
            _saveLoadService.SaveProgress();
            return default;
        }
    }
}
