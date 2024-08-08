using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class StartStageTile : Tile, IObserver
    {
        private CurrentSpaceshipStage _currentSpacehsipStage;
        private DistanceObservable _distanceObservable;
        private Stage _stage;

        [Inject]
        private void Construct(DistanceObservable distanceObservable, CurrentSpaceshipStage currentSpacehsipStage, CurrentGenerationStage currentGenerationStage)
        {
            _currentSpacehsipStage = currentSpacehsipStage;
            _distanceObservable = distanceObservable;
            

            _stage = currentGenerationStage.GeneratedStageType;
        }

        private void Start()
        {
            _distanceObservable.RegisterObserver(this, transform.position);
        }

        public void Invoke()
        {
            _currentSpacehsipStage.SetCurrentStage(_stage);
            Debug.Log("Start stage " + _stage);
        }
    }
}
