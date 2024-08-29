using Assets.RaceTheSun.Sources.Gameplay.Counters;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud.ProgressPanel
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreValue;

        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;

            _scoreCounter.ScoreCountChanged += OnScoreCountChanged;
        }

        private void OnDestroy() =>
            _scoreCounter.ScoreCountChanged -= OnScoreCountChanged;

        private void OnScoreCountChanged(int score) =>
            _scoreValue.text = score.ToString();
    }
}