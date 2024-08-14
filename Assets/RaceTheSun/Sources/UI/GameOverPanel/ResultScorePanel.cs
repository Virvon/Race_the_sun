using Agava.YandexGames;
using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class ResultScorePanel : MonoBehaviour
    {
        [SerializeField] private float _showAnimationDuration;
        [SerializeField] private TMP_Text _text;

        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public void ShowResult()
        {
            StartCoroutine(ShowAnimator((int)_scoreCounter.Score));
        }

        private IEnumerator ShowAnimator(int resultScore)
        {
            int currentScore = 0;
            float passedTime = 0;
            float progress;

            while(currentScore != resultScore)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _showAnimationDuration;

                currentScore = (int)Mathf.Lerp(0, resultScore, progress);
                _text.text = currentScore.ToString();

                yield return null;
            }
        }
    }
}
 