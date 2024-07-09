using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class JumpBoostsCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _jumpBoostsCountValue;

        private SpaceshipJump _spaceshipJump;

        //[Inject]
        //private void Construct(SpaceshipJump spaceshipJump)
        //{
        //    _spaceshipJump = spaceshipJump;

        //    _spaceshipJump.JumpBoostsCountChanged += OnJumpBoostsCountChanged;
        //}

        //private void OnDestroy()
        //{
        //    _spaceshipJump.JumpBoostsCountChanged -= OnJumpBoostsCountChanged;
        //}

        private void OnJumpBoostsCountChanged(int jumpBoostsCount)
        {
            _jumpBoostsCountValue.text = jumpBoostsCount.ToString();
        }
    }
}
