using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Sun;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Assets.RaceTheSun.Sources.UI.ScoreView;
using UnityEngine;
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
        private Sun.Sun _sun;
        private SkyboxSettingsChanger _skyboxSettingsChanger;
        private Spaceship.Plane _plane;

        [Inject]
        private void Construct(DistanceObservable distanceObservable, CurrentGenerationStage currentGenerationStage, WorldGenerator worldGenerator, Spaceship.Spaceship spaceship, ILoadingCurtain loadingCurtain, Sun.Sun sun, SkyboxSettingsChanger skyboxSettingsChanger, Spaceship.Plane plane)
        {
            _distanceObservable = distanceObservable;
            _currentGenerationStage = currentGenerationStage;
            _worldGenerator = worldGenerator;
            _spaceship = spaceship;
            _loadingCurtain = loadingCurtain;
            _sun = sun;
            _skyboxSettingsChanger = skyboxSettingsChanger;
            _plane = plane;
        }

        private void Start()
        {
            _distanceObservable.RegisterObserver(this, transform.position);
        }

        public void Invoke()
        {
            _loadingCurtain.Show(0.2f, callback: () => _loadingCurtain.Hide(0.8f));
            _sun.Reset();
            _skyboxSettingsChanger.Reset();
            _plane.gameObject.SetActive(true);
            _worldGenerator.Clean(); 
            _currentGenerationStage.EndBonusLevel();
            _spaceship.GetComponentInChildren<StartMovement>().Move();
        }
    }
}
