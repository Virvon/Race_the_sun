﻿using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Battery;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine;
using Assets.RaceTheSun.Sources.Gameplay.StateMachine.States;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.CamerasFactory.Gameplay;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.SpaceshipModelFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay
{
    public class GameplayBootstrapper : IInitializable
    {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly StatesFactory _statesFactory;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly ISpaceshipModelFactory _spaceshipModelFactory;
        private readonly IGameplayCamerasFactory _gameplayCamerasFactory;

        public GameplayBootstrapper(IGameplayFactory gameplayFactory, GameplayStateMachine gameplayStateMachine, StatesFactory statesFactory, IPersistentProgressService persistentProgress, ISpaceshipModelFactory spaceshipModelFactory, IGameplayCamerasFactory gameplayCamerasFactory)
        {
            _gameplayFactory = gameplayFactory;
            _gameplayStateMachine = gameplayStateMachine;
            _statesFactory = statesFactory;
            _persistentProgress = persistentProgress;
            _spaceshipModelFactory = spaceshipModelFactory;
            _gameplayCamerasFactory = gameplayCamerasFactory;
        }

        public async void Initialize()
        {
            await _gameplayCamerasFactory.CreateStartCamera();
            await _gameplayFactory.CreateStageMusic();
            await _gameplayFactory.CreateCollectItemsSoundEffects();
            await _gameplayFactory.CreatePortalSoundPlayer();
            await _gameplayFactory.CreateDestroySoundPlayer();

            Spaceship.Spaceship spaceship = await _gameplayFactory.CreateSpaceship();
            SpaceshipModel spaceshipModel = await _spaceshipModelFactory.CreateSpaceshipModel(_persistentProgress.Progress.AvailableSpaceships.CurrentSpaceshipType, spaceship.GetComponentInChildren< Assets.RaceTheSun.Sources.Gameplay.ModelPoint>().transform.position, spaceship.transform);
            spaceship.GetComponentInChildren<SpaceshipTurning>().Init(spaceshipModel);
            spaceship.GetComponentInChildren<BatteryIndicator>().Init(spaceshipModel.GetComponentInChildren<MeshRenderer>());

            await _gameplayFactory.CreateSun();
            await _gameplayFactory.CreatePlane();
            await _gameplayFactory.CreateShpaceshipShieldPortal();
            await _gameplayFactory.CreateBird();
            await _gameplayFactory.CreateWorldGenerator();
            await _gameplayFactory.CreateHud();
            await _gameplayCamerasFactory.CreateSpaceshipMainCamera();
            await _gameplayCamerasFactory.CreateSpaceshipSideCamera();
            await _gameplayCamerasFactory.CreateSpaceshipUpperCamera();
            await _gameplayCamerasFactory.CreateCollisionPortalCamera();
            await _gameplayCamerasFactory.CreateShieldCamera();
            await _gameplayFactory.CreateGameOverPanel();

            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameStartState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameLoopState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<RevivalState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<ResultState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<GameEndState>());

            await _gameplayStateMachine.Enter<GameStartState>();
        }
    }
}
