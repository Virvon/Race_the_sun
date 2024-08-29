using Assets.RaceTheSun.Sources.GameLogic.Animations;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class JumpBoostsCountPanel : HudAnimationElement
    {
        [SerializeField] private TMP_Text _jumpBoostsCountValue;
        [SerializeField] private TMP_Text _maxJumpBoostsCountValue;
        [SerializeField] private GameObject _infoPanel;

        private SpaceshipJump _spaceshipJump;

        [Inject]
        private void Construct(SpaceshipJump spaceshipJump, Spaceship spaceship, PersistentProgressService persistentProgressService)
        {
            _spaceshipJump = spaceshipJump;
            _maxJumpBoostsCountValue.text = spaceship.AttachmentStats.MaxJumpBoostsCount.ToString();

            _spaceshipJump.JumpBoostsCountChanged += ChangeInfo;

            ChangeInfo();

            if (persistentProgressService.Progress.Upgrading.IsUpgraded(Upgrading.UpgradeType.JumpBoost) == false)
                gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _spaceshipJump.JumpBoostsCountChanged -= ChangeInfo;
        }

        private void ChangeInfo()
        {
            _jumpBoostsCountValue.text = _spaceshipJump.JumpBoostsCount.ToString();

            if (_spaceshipJump.JumpBoostsCount > 0 && _infoPanel.activeSelf == false)
                _infoPanel.SetActive(true);
            else if (_spaceshipJump.JumpBoostsCount == 0)
                _infoPanel.SetActive(false);
        }
    }
}
