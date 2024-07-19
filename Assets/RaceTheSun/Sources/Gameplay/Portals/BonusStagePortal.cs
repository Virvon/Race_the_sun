using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using Assets.RaceTheSun.Sources.UI.LoadingCurtain;
using Assets.RaceTheSun.Sources.UI.ScoreView;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Portals
{
    public class BonusStagePortal : MonoBehaviour
    {
        private CurrentGenerationStage _currentGenerationStage;
        private WorldGenerator.WorldGenerator _worldGenerator;
        private ILoadingCurtain _loadingCurtain;

        [Inject]
        private void Construct(CurrentGenerationStage currentGenerationStage, WorldGenerator.WorldGenerator worldGenerator, ILoadingCurtain transitionCurtain)
        {
            _currentGenerationStage = currentGenerationStage;
            _worldGenerator = worldGenerator;
            _loadingCurtain = transitionCurtain;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Spaceship.Spaceship _))
            {
                _worldGenerator.Clean();
                _currentGenerationStage.SetBonusLevel();
                _loadingCurtain.Show();
                other.GetComponentInChildren<StartMovement>().MoveUpper(startCallback: ()=> _loadingCurtain.Hide(0.6f));
            }
        }
    }
}
