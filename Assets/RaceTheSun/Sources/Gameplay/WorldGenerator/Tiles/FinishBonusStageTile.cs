using Assets.RaceTheSun.Sources.Gameplay.DistanceObserver;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement;
using Assets.RaceTheSun.Sources.Gameplay.Sun;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.StageInfo;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.Tiles
{
    public class FinishBonusStageTile : Tile, IObserver
    {
        private const float ShowCurtainDuration = 0.2f;
        private const float HideCurtainDuration = 0.8f;

        private DistanceObservable _distanceObservable;
        private CurrentGenerationStage _currentGenerationStage;
        private WorldGenerator _worldGenerator;
        private ILoadingCurtain _loadingCurtain;
        private Sun.Sun _sun;
        private SkyboxSettingsChanger _skyboxSettingsChanger;
        private Plane _plane;
        private CutSceneMovement _cutSceneMovement;

        [Inject]
        private void Construct(DistanceObservable distanceObservable, CurrentGenerationStage currentGenerationStage, WorldGenerator worldGenerator, ILoadingCurtain loadingCurtain, Sun.Sun sun, SkyboxSettingsChanger skyboxSettingsChanger, Plane plane, CutSceneMovement cutSceneMovement)
        {
            _distanceObservable = distanceObservable;
            _currentGenerationStage = currentGenerationStage;
            _worldGenerator = worldGenerator;
            _loadingCurtain = loadingCurtain;
            _sun = sun;
            _skyboxSettingsChanger = skyboxSettingsChanger;
            _plane = plane;
            _cutSceneMovement = cutSceneMovement;
        }

        private void Start() =>
            _distanceObservable.RegisterObserver(this, transform.position);

        public void Invoke()
        {
            _loadingCurtain.Show(ShowCurtainDuration, callback: () => _loadingCurtain.Hide(HideCurtainDuration));
            _sun.Reset();
            _skyboxSettingsChanger.Reset();
            _plane.gameObject.SetActive(true);
            _worldGenerator.Clean();
            _currentGenerationStage.EndBonusLevel();
            _cutSceneMovement.MoveStart();
        }
    }
}
