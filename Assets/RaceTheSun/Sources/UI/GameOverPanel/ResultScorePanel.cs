using System;
using System.Collections;
using System.Globalization;
using Agava.YandexGames;
using Assets.RaceTheSun.Sources.Gameplay.Counters;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class ResultScorePanel : MonoBehaviour
    {
        [SerializeField] private float _showAnimationDuration;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _highScoreText;

        private ScoreCounter _scoreCounter;
        private IPersistentProgressService _persistentProgressService;
        private bool _isOpened;

        [Inject]
        private void Construct(ScoreCounter scoreCounter, IPersistentProgressService persistentProgressService)
        {
            _scoreCounter = scoreCounter;
            _persistentProgressService = persistentProgressService;
            _isOpened = false;
        }

        public void ShowResult(Action callbakc)
        {
            if (_isOpened)
                return;

            int score = (int)_scoreCounter.Score;

            if (score > _persistentProgressService.Progress.HighScore)
            {
                _highScoreText.SetActive(true);
                _persistentProgressService.Progress.HighScore = score;
            }
            else
            {
                _highScoreText.SetActive(false);
            }

            StartCoroutine(ShowAnimator(score, callbakc));
        }

        private string DivideIntegerOnDigits(int value)
        {
            if (value == 0)
                return "0";

            var culture = new CultureInfo("ru-RU");
            return value.ToString("#,#", culture);
        }

        private IEnumerator ShowAnimator(int resultScore, Action callbakc)
        {
            int currentScore = 0;
            float passedTime = 0;
            float progress;

            _isOpened = true;

            while (currentScore != resultScore)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _showAnimationDuration;

                currentScore = (int)Mathf.Lerp(0, resultScore, progress);
                _text.text = DivideIntegerOnDigits(currentScore);

                yield return null;
            }

            _isOpened = false;
            callbakc?.Invoke();
        }
    }
}