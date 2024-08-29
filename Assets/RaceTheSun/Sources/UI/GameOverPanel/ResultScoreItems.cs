using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class ResultScoreItems : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreItemsValue;
        [SerializeField] private float _showAnimationDuration;

        public void SetScoreItemsValue(int value, Action callback) =>
            StartCoroutine(ShowAnimator(value, callback));

        public void SetScoreItemsValue(int value) =>
            _scoreItemsValue.text = value.ToString();

        public IEnumerator ShowAnimator(int value, Action callback)
        {
            int currentValue = 0;
            float passedTime = 0;
            float progress;

            while (currentValue != value)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _showAnimationDuration;

                currentValue = (int)Mathf.Lerp(0, value, progress);
                _scoreItemsValue.text = currentValue.ToString();

                yield return null;
            }

            callback?.Invoke();
        }
    }
}
