using Assets.RaceTheSun.Sources.GameLogic.Animations;
using Assets.RaceTheSun.Sources.GameLogic.Attachment;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.Counters;
using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.StageInfo;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.Gameplay;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.SpaceshipModelFactory;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayBootstrapper();
            BindGameplayFactory();
            BindGameplayCamerasFactory();
            BindGameplayStateMachine();
            BindCounters();
            BindCurrentGenerationStage();
            BindDistanceObservable();
            BindCameras();
            BindHudAnimation();
            BindCurrentSpaceshipStage();
            SpaceshipModelFactory();
            BindAttachmentCellsUpgrader();
        }

        private void BindAttachmentCellsUpgrader()
        {
            Container
                .BindInterfacesAndSelfTo<AttachmentCellsUpgrader>()
                .AsSingle();
        }

        private void SpaceshipModelFactory() =>
            SpaceshipModelFactoryInstaller.Install(Container);

        private void BindGameplayCamerasFactory() =>
            GameplayCamerasFactoryInstaller.Install(Container);

        private void BindCurrentSpaceshipStage()
        {
            Container
                .BindInterfacesAndSelfTo<CurrentSpaceshipStage>()
                .AsSingle();
        }

        private void BindHudAnimation()
        {
            Container
                .Bind<HudAnimation>()
                .FromNew()
                .AsSingle();
        }

        private void BindCameras()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayCameras>()
                .AsSingle();
        }

        private void BindDistanceObservable()
        {
            Container
                .BindInterfacesAndSelfTo<DistanceObservable>()
                .AsSingle();
        }

        private void BindCurrentGenerationStage()
        {
            Container
                .BindInterfacesAndSelfTo<CurrentGenerationStage>()
                .AsSingle();
        }

        private void BindCounters()
        {
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreItemsCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MultiplierProgressCounter>().AsSingle();
        }

        private void BindGameplayStateMachine() =>
            GameplayStateMachineInstaller.Install(Container);

        private void BindGameplayFactory() =>
            GameplayFactoryInstaller.Install(Container);

        private void BindGameplayBootstrapper()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayBootstrapper>()
                .AsSingle()
                .NonLazy();
        }
    }
}