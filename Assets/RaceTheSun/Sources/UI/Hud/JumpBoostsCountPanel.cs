using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class JumpBoostsCountPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _jumpBoostsCountValue;
        [SerializeField] private GameObject _infoPanel;

        private SpaceshipJump _spaceshipJump;

        [Inject]
        private void Construct(SpaceshipJump spaceshipJump)
        {
            _spaceshipJump = spaceshipJump;

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
