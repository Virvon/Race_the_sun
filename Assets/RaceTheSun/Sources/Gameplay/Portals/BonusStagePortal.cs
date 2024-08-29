using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.StageInfo;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Portals
{
    public class BonusStagePortal : MonoBehaviour
    {
        private const float HideCurtainDuration = 0.6f;

        private CurrentGenerationStage _currentGenerationStage;
        private WorldGenerator.WorldGenerator _worldGenerator;
        private ILoadingCurtain _loadingCurtain;
        private Sun.Sun _sun;
        private Spaceship.Plane _plane;

        [Inject(Id = GameplayFactoryInjectId.PortalSound)]
        private SoundPlayer _portalSound;

        [Inject]
        private void Construct(CurrentGenerationStage currentGenerationStage, WorldGenerator.WorldGenerator worldGenerator, ILoadingCurtain transitionCurtain, Sun.Sun sun, Spaceship.Plane plane)
        {
            _currentGenerationStage = currentGenerationStage;
            _worldGenerator = worldGenerator;
            _loadingCurtain = transitionCurtain;
            _sun = sun;
            _plane = plane;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Spaceship.Spaceship _))
            {
                _portalSound.Play();
                _loadingCurtain.Show();
                _worldGenerator.Clean();
                _currentGenerationStage.SetBonusLevel();
                _sun.Hide();
                _plane.gameObject.SetActive(false);
                other.GetComponentInChildren<CutSceneMovement>().MoveUpper(startCallback: ()=> _loadingCurtain.Hide(HideCurtainDuration));
            }
        }
    }
}
