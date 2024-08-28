﻿using Assets.RaceTheSun.Sources.Infrastructure.AssetManagement;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.SceneManagement;
using Assets.RaceTheSun.Sources.Services.ActivityTracking;
using Assets.RaceTheSun.Sources.Services.CoroutineRunner;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.TimeScale;
using Assets.RaceTheSun.Sources.Services.WaitingService;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Virvon.MyBakery.Services.Input;
using Zenject;

namespace Assets.RaceTheSun.Sources.CompositionRoot
{
    public class GameInstaller : MonoInstaller, Services.CoroutineRunner.ICoroutineRunner
    {
        [SerializeField] private AudioMixer _audioMixer;

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindLoadingCurtain();
            BindAssetProvider();
            BindSceneLoader();
            BindInputService();
            BindGameplayFactory();
            BindStaticDataService();
            BindSaveLoadService();
            BindPersistentProgressService();
            BindUiFactory();
            BindTimeScale();
            BindCoroutineRunner();
            BindWaitingService();
            BindAttachment();
        }

        private void BindActivityTracking()
        {
            Container.Bind<AudioMixer>().FromInstance(_audioMixer).AsSingle();
            Container.BindInterfacesAndSelfTo<ActivityTracking>().AsSingle().NonLazy();
        }

        private void BindAttachment()
        {
            Container
                .BindInterfacesAndSelfTo<Attachment.Attachment>()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle();
        }

        private void BindWaitingService()
        {
            Container
                .BindInterfacesAndSelfTo<WaitingService>()
                .AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindTimeScale()
        {
            Container
                .BindInterfacesAndSelfTo<TimeScale>()
                .AsSingle();
        }

        private void BindUiFactory()
        {
            Container
                 .Bind<IUiFactory>()
                 .FromSubContainerResolve()
                 .ByInstaller<UiFactoryInstaller>()
                 .AsSingle();
        }


        private void BindPersistentProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<PersistentProgressService>()
                .AsSingle();
        }

        private void BindSaveLoadService()
        {
            Container
                .BindInterfacesAndSelfTo<SaveLoadService>()
                .AsSingle();
        }

        private void BindStaticDataService()
        {
            Container
                .BindInterfacesAndSelfTo<StaticDataService>()
                .AsSingle();
        }

        private void BindGameplayFactory()
        {
            Container
                .Bind<IGameplayFactory>()
                .FromSubContainerResolve()
                .ByInstaller<GameplayFactoryInstaller>()
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .BindInterfacesTo<SceneLoader>()
                .AsSingle();
        }

        private void BindAssetProvider()
        {
            Container
                .BindInterfacesTo<AssetProvider>()
                .AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container
                .BindFactory<string, UniTask<LoadingCurtain>, LoadingCurtain.Factory>()
                .FromFactory<KeyPrefabFactoryAsync<LoadingCurtain>>();

            Container
                .BindInterfacesAndSelfTo<LoadingCurtainProxy>()
                .AsSingle();
        }

        private void BindGameStateMachine() =>
            GameStateMachineInstaller.Install(Container);
    }
}
