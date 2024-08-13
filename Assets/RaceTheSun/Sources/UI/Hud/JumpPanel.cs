using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class JumpPanel : MonoBehaviour
    {
        private SpaceshipJump _spaceshipJump;

        [Inject]
        private void Construct(SpaceshipJump spaceshipJump)
        {
            _spaceshipJump = spaceshipJump;

            _spaceshipJump.JumpBoostsCountChanged += TryActive;

            TryActive(0);
        }

        private void OnDestroy()
        {
            _spaceshipJump.JumpBoostsCountChanged -= TryActive;
        }

        private void TryActive(int jumpBoostsCount)
        {
            gameObject.SetActive(jumpBoostsCount > 0);
        }
    }
}
