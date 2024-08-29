using System;
using System.Collections;
using Assets.RaceTheSun.Sources.Gameplay.Counters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud.ProgressPanel
{
    public class ScoreMultiplierPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _multiplilerValue;
        [SerializeField] private float _halfAnimationDuration;
        [SerializeField] private Image _image;
        [SerializeField] private Color _increaseColor;
        [SerializeField] private Color _decreaseColor;

        private MultiplierProgressCounter _multiplierProgressCounter;
        private Color _panelColor;
        private Coroutine _animation;

        [Inject]
        private void Construct(MultiplierProgressCounter multiplierProgressCounter)
        {
            _multiplierProgressCounter = multiplierProgressCounter;
            _panelColor = _image.color;

            _multiplierProgressCounter.MultiplierChanged += OnMultiplierChanged;
        }

        private void OnDestroy()
        {
            _multiplierProgressCounter.MultiplierChanged -= OnMultiplierChanged;
        }

        private void OnMultiplierChanged(int multipliler)
        {
            Color targetColor = multipliler > Convert.ToInt32(_multiplilerValue.text) ? _increaseColor : _decreaseColor;

            _multiplilerValue.text = multipliler.ToString();

            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Animation(targetColor));
        }

        private IEnumerator Animation(Color targetColor)
        {
            float passedTime = 0;
            float progress;
            Color startColor = _image.color;

            while (_image.color != targetColor)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _halfAnimationDuration;

                _image.color = Color.Lerp(startColor, targetColor, progress);

                yield return null;
            }

            passedTime = 0;

            while (_image.color != _panelColor)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _halfAnimationDuration;

                _image.color = Color.Lerp(startColor, _panelColor, progress);

                yield return null;
            }
        }
    }
}
