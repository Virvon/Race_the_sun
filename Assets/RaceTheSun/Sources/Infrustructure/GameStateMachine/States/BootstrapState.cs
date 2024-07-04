using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Infrustructure.GameStateMachine.States;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly LoadingCurtainProxy _loadingCurtainProxy;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(GameStateMachine stateMachine, LoadingCurtainProxy loadingCurtainProxy, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _loadingCurtainProxy = loadingCurtainProxy;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async UniTask Enter()
        {
            await InitServices();

            _stateMachine.Enter<LoadProgressState>().Forget();
        }

        public UniTask Exit() =>
            default;

        private async UniTask InitServices()
        {
            await _assetProvider.InitializeAsync();
            await _loadingCurtainProxy.InitializeAsync();
            await _staticDataService.InitializeAsync();
        }
    }
}
