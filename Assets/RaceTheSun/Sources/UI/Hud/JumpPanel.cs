using Assets.RaceTheSun.Sources.Gameplay.Portals;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class JumpPanel : MonoBehaviour
    {
        private SpaceshipJump _spaceshipJump;
        private SpaceshipDie _spaceshipDie;
        private SpaceshipShieldPortal _spaceshipShieldPortal;

        [Inject]
        private void Construct(
            SpaceshipJump spaceshipJump,
            SpaceshipDie spaceshipDie,
            SpaceshipShieldPortal spaceshipShieldPortal)
        {
            _spaceshipJump = spaceshipJump;
            _spaceshipDie = spaceshipDie;
            _spaceshipShieldPortal = spaceshipShieldPortal;

            _spaceshipJump.JumpBoostsCountChanged += TryActive;
            _spaceshipDie.Died += Hide;
            _spaceshipDie.Stopped += Hide;
            _spaceshipShieldPortal.Activated += TryActive;

            TryActive();
        }

        public bool IsActivated { get; private set; }

        private void OnDestroy()
        {
            _spaceshipJump.JumpBoostsCountChanged -= TryActive;
            _spaceshipDie.Died -= Hide;
            _spaceshipDie.Stopped -= Hide;
            _spaceshipShieldPortal.Activated -= TryActive;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsActivated = false;
        }

        public void TryActive()
        {
            IsActivated = _spaceshipJump.JumpBoostsCount > 0;
            gameObject.SetActive(IsActivated);
        }
    }
}
