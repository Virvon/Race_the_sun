using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.ScoreView
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreValue;

        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;

            _scoreCounter.ScoreCountChanged += OnScoreCountChanged;
        }

        private void OnDestroy()
        {
            _scoreCounter.ScoreCountChanged -= OnScoreCountChanged;
        }

        private void OnScoreCountChanged(int score)
        {
            _scoreValue.text = score.ToString();
        }
    }
}
