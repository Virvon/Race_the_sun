using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class ScoreItemsProgressPanel : MonoBehaviour
    {
        [SerializeField] private ScoreItemPanel[] _scoreItemPanels;

        private ScoreCounter _scoreCounter;
        private int _nextFilledScoreItemPanelIndex;
        private int _nextReleasedScoreItemPanelIndex;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            _nextFilledScoreItemPanelIndex = 0;

            _scoreCounter.MultiplierProgressChanged += OnMultiplierProgressChanged;
        }

        private void OnDestroy()
        {
            _scoreCounter.MultiplierProgressChanged -= OnMultiplierProgressChanged;
        }

        private void OnMultiplierProgressChanged(int multiplierProgress)
        {
            _scoreItemPanels[_nextFilledScoreItemPanelIndex].Fill();
            _nextFilledScoreItemPanelIndex++;

            if (_nextFilledScoreItemPanelIndex == _scoreItemPanels.Length)
                ReleaseScoreItemPanels();
        }

        private void ReleaseScoreItemPanels()
        {
            _nextReleasedScoreItemPanelIndex = 0;
            _nextFilledScoreItemPanelIndex = 0;

            _scoreItemPanels[_nextReleasedScoreItemPanelIndex].Release(callback: () =>
            {
                ReleaseNextScoreItemPanel();
            });
        }

        private void ReleaseNextScoreItemPanel()
        {
            _nextReleasedScoreItemPanelIndex++;

            if (_nextReleasedScoreItemPanelIndex == _scoreItemPanels.Length)
                return;

            _scoreItemPanels[_nextReleasedScoreItemPanelIndex].Release(callback: () =>
            {
                ReleaseNextScoreItemPanel();
            });
        }
    }
}
