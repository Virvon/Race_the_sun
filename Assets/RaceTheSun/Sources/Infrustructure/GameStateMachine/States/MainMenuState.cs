using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States
{
    public class MainMenuState : IState
    {
        private readonly ILoadingCurtain _loadingCurtainProxy;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISaveLoadService _saveLoadService;

        public MainMenuState(ILoadingCurtain loadingCurtainProxy, ISceneLoader sceneLoader, ISaveLoadService saveLoadService)
        {
            _loadingCurtainProxy = loadingCurtainProxy;
            _sceneLoader = sceneLoader;
            _saveLoadService = saveLoadService;
        }

        public async UniTask Enter()
        {
            _loadingCurtainProxy.Show();

            await _sceneLoader.Load(InfrasructureAssetPath.MainMenuScene);
        }

        public UniTask Exit()
        {
            _saveLoadService.SaveProgress();
            return default;
        }
    }
}
