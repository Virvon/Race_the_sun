using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class ShieldsCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _shieldsCountValue;

        private SpaceshipDie _spaceshipDie;

        //[Inject]
        //private void Construct(SpaceshipDie spaceshipDie)
        //{
        //    _spaceshipDie = spaceshipDie;

        //    _spaceshipDie.ShieldsCountChanged += OnShieldsCountChanged;
        //}

        //private void OnDestroy()
        //{
        //    _spaceshipDie.ShieldsCountChanged -= OnShieldsCountChanged;
        //}

        private void OnShieldsCountChanged(int shieldsCount)
        {
            _shieldsCountValue.text = shieldsCount.ToString();
        }
    }
}
