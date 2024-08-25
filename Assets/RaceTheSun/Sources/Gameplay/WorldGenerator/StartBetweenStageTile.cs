using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.WaitingService;
using Assets.RaceTheSun.Sources.UI.ScoreView;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class StartBetweenStageTile : StartStageTile
    {
        [SerializeField] private Transform[] _birdMovementPath;

        private Bird.Bird _bird;
        private WaitingService _waitingService;
        private Spaceship.Spaceship _spaceship;
        private PerfectStagePanel _perfectStagePanel;
        private CurrentGenerationStage _currentGenerationStage;
        private WorldGenerator _worldGenerator;

        private int _index;

        [Inject]
        private void Construct(Bird.Bird bird, WaitingService waitingService, Spaceship.Spaceship spaceship, PerfectStagePanel perfectStagePanel, CurrentGenerationStage currentGenerationStage, WorldGenerator worldGenerator)
        {
            _bird = bird;
            _waitingService = waitingService;
            _spaceship = spaceship;
            _perfectStagePanel = perfectStagePanel;
            _currentGenerationStage = currentGenerationStage;
            _worldGenerator = worldGenerator;
            _index = _currentGenerationStage.CurrentTile;
        }

        public override void Invoke()
        {
            _spaceship.transform.position = new Vector3(0, _spaceship.transform.position.y, 0);
            _worldGenerator.Replace();

            base.Invoke();

            Vector3[] movementPath = new Vector3[_birdMovementPath.Length];

            for(int i = 0; i < _birdMovementPath.Length; i++)
                movementPath[i] = _birdMovementPath[i].position;

            _waitingService.Wait(0.8f, callback: () => _bird.Move(movementPath));

            if (_spaceship.GetCollisionPerStage() == false)
                _perfectStagePanel.Show();
        }
    }
}
