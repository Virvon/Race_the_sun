using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.StateMachine.States
{
    public class GameStartState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly WorldGenerator.WorldGenerator _worldGenerator;
        private readonly IGameplayFactory _gameplayFacotry;
        private readonly Spaceship.Spaceship _spaceship;

        private CinemachineVirtualCamera _startCamera;

        public GameStartState(GameplayStateMachine gameplayStateMachine, IStaticDataService staticDataService, WorldGenerator.WorldGenerator worldGenerator, IGameplayFactory gameplayFacotry, Spaceship.Spaceship spaceship, StartCamera startCamera)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _staticDataService = staticDataService;
            _worldGenerator = worldGenerator;
            _gameplayFacotry = gameplayFacotry;
            _spaceship = spaceship;
            _startCamera = startCamera.GetComponent<CinemachineVirtualCamera>();
        }

        public async UniTask Enter()
        {
            StageConfig stageConfig = _staticDataService.GetStage(Stage.StartStage);

            _spaceship.StartCoroutine(SpaceshipMover());
        }

        public UniTask Exit()
        {
            return default;
        }

        private IEnumerator SpaceshipMover()
        {
            Vector3 startPosition = new Vector3(0, 2, -100);
            Vector3 endPosition = new Vector3(0, 2, 30);
            float duration = 1f;
            float time = 0;

            SpaceshipMovement movement = _spaceship.GetComponentInChildren<SpaceshipMovement>();
            movement.enabled = false;

            while(_spaceship.transform.position != endPosition)
            {
                time += Time.deltaTime;
                float progress = time / duration;

                _spaceship.transform.position = Vector3.Lerp(startPosition, endPosition, progress);

                yield return null;
            }

            movement.enabled = true;
            _startCamera.Priority = (int)CameraPriority.NotUse;
            _gameplayStateMachine.Enter<GameLoopState>().Forget();
        }
    }
}
