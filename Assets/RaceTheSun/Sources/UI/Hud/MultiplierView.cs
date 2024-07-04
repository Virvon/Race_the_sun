using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class MultiplierView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _multiplilerValue;

        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;

            _scoreCounter.MultiplierChanged += OnMultiplierChanged;
        }

        private void OnDestroy()
        {
            _scoreCounter.MultiplierChanged -= OnMultiplierChanged;
        }

        private void OnMultiplierChanged(int multipliler)
        {
            _multiplilerValue.text = multipliler.ToString();
        }
    }
}
