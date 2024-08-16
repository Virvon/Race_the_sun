using Assets.RaceTheSun.Sources.Animations;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class JumpBoostsCountPanel : HudAnimationElement
    {
        [SerializeField] private TMP_Text _jumpBoostsCountValue;
        [SerializeField] private TMP_Text _maxJumpBoostsCountValue;
        [SerializeField] private GameObject _infoPanel;

        private SpaceshipJump _spaceshipJump;

        [Inject]
        private void Construct(SpaceshipJump spaceshipJump, Spaceship spaceship)
        {
            _spaceshipJump = spaceshipJump;
            _maxJumpBoostsCountValue.text = spaceship.AttachmentStats.MaxJumpBoostsCount.ToString();

            _spaceshipJump.JumpBoostsCountChanged += ChangeInfo;

            ChangeInfo(0);
        }

        private void OnDestroy()
        {
            _spaceshipJump.JumpBoostsCountChanged -= ChangeInfo;
        }

        private void ChangeInfo(int jumpBoostsCount)
        {
            _jumpBoostsCountValue.text = jumpBoostsCount.ToString();

            if (jumpBoostsCount > 0 && _infoPanel.activeSelf == false)
                _infoPanel.SetActive(true);
            else if (jumpBoostsCount == 0)
                _infoPanel.SetActive(false);
        }
    }
}
