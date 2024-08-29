using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.Services.CoroutineRunner;
using Agava.YandexGames;
using UnityEngine;
using System.Collections;
using System;
using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;

namespace Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly LoadingCurtainProxy _loadingCurtainProxy;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, LoadingCurtainProxy loadingCurtainProxy, IAssetProvider assetProvider, IStaticDataService staticDataService, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _loadingCurtainProxy = loadingCurtainProxy;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
        }

        public async UniTask Enter()
        {
            await InitServices();
            _coroutineRunner.StartCoroutine(InitializeYandexSdk(callback: () => _stateMachine.Enter<LoadProgressState>().Forget()));
        }

        public UniTask Exit() =>
            default;

        private async UniTask InitServices()
        {
            await _assetProvider.InitializeAsync();
            await _loadingCurtainProxy.InitializeAsync();
            await _staticDataService.InitializeAsync();
        }

        private IEnumerator InitializeYandexSdk(Action callback)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            callback?.Invoke();
            yield break;
#else
            yield return YandexGamesSdk.Initialize();

            if (YandexGamesSdk.IsInitialized == false)
                throw new ArgumentNullException(nameof(YandexGamesSdk), "Yandex SDK didn't initialized correctly");

            YandexGamesSdk.CallbackLogging = true;
            StickyAd.Show();
            callback?.Invoke();
#endif
        }
    }
}
