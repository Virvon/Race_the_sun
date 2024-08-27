using Assets.RaceTheSun.Sources.Animations;
using Assets.RaceTheSun.Sources.UI.MainMenu;
using MPUIKIT;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.GameOverPanel
{
    public class RevivalPanel : MonoBehaviour
    {
        [SerializeField] private MPImage _timerValue;
        [SerializeField] private float _duration;
        [SerializeField] private GameOverPanelAnimationElement _revivalPanelAnimationElement;
        [SerializeField] private Button _revivalButton;
        [SerializeField] private GameObject _header;
        [SerializeField] private Button _hideButton;

        private Coroutine _timer;

        public event Action RevivalButtonClicked;
        public event Action RevivalTimeEnded;

        private void OnEnable()
        {
            _revivalPanelAnimationElement.Opened += OnRevivalPanelAnimationElementOpened;
            _revivalButton.onClick.AddListener(OnRevivalButtonClicked);
            _hideButton.onClick.AddListener(OnHideButtonClicked);
        }

        private void OnDisable()
        {
            _revivalPanelAnimationElement.Opened -= OnRevivalPanelAnimationElementOpened;
            _revivalButton.onClick.RemoveListener(OnRevivalButtonClicked);
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
        }

        public void Open()
        {
            _revivalPanelAnimationElement.Open();
            _header.SetActive(true);
        }

        public void Hide(Action callback = null)
        {
            _revivalPanelAnimationElement.Hided += ()=> callback?.Invoke();
            _revivalPanelAnimationElement.Hide();
            _header.SetActive(false);
        }

        private void OnRevivalPanelAnimationElementOpened()
        {
            _timer = StartCoroutine(Timer());
        }

        private void OnRevivalButtonClicked()
        {
            if(_timer != null)
                StopCoroutine(_timer);

            RevivalButtonClicked?.Invoke();
        }

        private void OnHideButtonClicked()
        {
            if (_timer != null)
                StopCoroutine(_timer);

            RevivalTimeEnded?.Invoke();
        }

        private IEnumerator Timer()
        {
            float passedTime = 0;
            float startValue = 1;
            float progress;

            while(_timerValue.fillAmount != 0)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _duration;

                _timerValue.fillAmount = Mathf.Lerp(startValue, 0, progress);

                yield return null;
            }

            RevivalTimeEnded?.Invoke();
        }
    }
}
