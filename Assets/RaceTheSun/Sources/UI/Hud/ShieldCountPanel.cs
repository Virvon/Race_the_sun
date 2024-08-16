using Assets.RaceTheSun.Sources.Animations;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class ShieldCountPanel : HudAnimationElement
    {
        [SerializeField] private TMP_Text _shieldsCountValue;
        [SerializeField] private TMP_Text _maxShieldsCountValue;
        [SerializeField] private GameObject _infoPanel;

        private SpaceshipDie _spaceshipDie;

        [Inject]
        private void Construct(SpaceshipDie spaceshipDie, Spaceship spaceship)
        {
            _spaceshipDie = spaceshipDie;
            _maxShieldsCountValue.text = spaceship.AttachmentStats.MaxShileldsCount.ToString();

            _spaceshipDie.ShieldsCountChanged += ChangeInfo;

            ChangeInfo(0);
        }

        private void OnDestroy()
        {
            _spaceshipDie.ShieldsCountChanged -= ChangeInfo;
        }

        private void ChangeInfo(int shieldsCount)
        {
            _shieldsCountValue.text = shieldsCount.ToString();

            if(shieldsCount > 0 && _infoPanel.activeSelf == false)
                _infoPanel.SetActive(true);
            else if(shieldsCount == 0)
                _infoPanel.SetActive(false);
        }
    }
}
