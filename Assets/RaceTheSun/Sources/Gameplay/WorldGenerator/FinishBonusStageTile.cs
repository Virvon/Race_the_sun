using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Assets.RaceTheSun.Sources.UI.ScoreView;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class FinishBonusStageTile : Tile, IObserver
    {
        private DistanceObservable _distanceObservable;
        private CurrentGenerationStage _currentGenerationStage;
        private WorldGenerator _worldGenerator;
        private Spaceship.Spaceship _spaceship;
        private ILoadingCurtain _loadingCurtain;

        [Inject]
        private void Construct(DistanceObservable distanceObservable, CurrentGenerationStage currentGenerationStage, WorldGenerator worldGenerator, Spaceship.Spaceship spaceship, ILoadingCurtain loadingCurtain)
        {
            _distanceObservable = distanceObservable;
            _currentGenerationStage = currentGenerationStage;
            _worldGenerator = worldGenerator;
            _spaceship = spaceship;
            _loadingCurtain = loadingCurtain;

            _distanceObservable.RegisterObserver(this, transform.position);
        }

        private void OnDestroy()
        {
            
        }

        public void Invoke()
        {
            _loadingCurtain.Show(0.2f, callback: () => _loadingCurtain.Hide(0.6f));
            _worldGenerator.Clean(); 
            _currentGenerationStage.EndBonusLevel();
            _spaceship.GetComponentInChildren<StartMovement>().Move();
        }
    }
}
