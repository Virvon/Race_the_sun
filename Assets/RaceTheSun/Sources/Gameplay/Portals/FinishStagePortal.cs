using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Portals
{
    public class FinishStagePortal : MonoBehaviour
    {
        private CurrentGenerationStage _currentGenerationStage;
        private WorldGenerator.WorldGenerator _worldGenerator;
        private PortalSound _portalSound;

        [Inject]
        private void Construct(CurrentGenerationStage currentGenerationStage, WorldGenerator.WorldGenerator worldGenerator, PortalSound portalSound)
        {
            _currentGenerationStage = currentGenerationStage;
            _worldGenerator = worldGenerator;
            _portalSound = portalSound;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Spaceship.Spaceship _))
            {
                _portalSound.Play();
                other.GetComponentInChildren<StartMovement>().Move();
                _worldGenerator.Clean();
                _currentGenerationStage.FinishStage();
            }
        }
    }
}
