﻿using Assets.RaceTheSun.Sources.Animations;
using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayBootstrapper();
            BindGameplayFactory();
            BindGameplayStateMachine();
            BindScoreCounter();
            BindCurrentGenerationStage();
            BindDistanceObservable();
            BindCameras();
            BindHudAnimation();
        }

        private void BindHudAnimation()
        {
            Container.Bind<HudAnimation>().FromNew().AsSingle();
        }

        private void BindCameras() =>
            Container.BindInterfacesAndSelfTo<Cameras.Cameras>().AsSingle();

        private void BindDistanceObservable() =>
            Container.BindInterfacesAndSelfTo<DistanceObservable>().AsSingle();

        private void BindCurrentGenerationStage() =>
            Container.BindInterfacesAndSelfTo<CurrentGenerationStage>().AsSingle();

        private void BindScoreCounter() =>
            Container.BindInterfacesAndSelfTo<ScoreCounter.ScoreCounter>().AsSingle();

        private void BindGameplayStateMachine() =>
            GameplayStateMachineInstaller.Install(Container);

        private void BindGameplayFactory() =>
            GameplayFactoryInstaller.Install(Container);

        private void BindGameplayBootstrapper() =>
            Container.BindInterfacesAndSelfTo<GameplayBootstrapper>().AsSingle().NonLazy();
    }
}