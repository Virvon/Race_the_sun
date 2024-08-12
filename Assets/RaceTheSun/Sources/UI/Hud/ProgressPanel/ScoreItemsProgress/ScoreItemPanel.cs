using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class ScoreItemPanel : MonoBehaviour
    {
        [SerializeField] private Color _emptyColor;
        [SerializeField] private Color _filedColor;
        [SerializeField] private Image _image;
        [SerializeField] private Vector3 _fillAnimationScale;
        [SerializeField] private float _halfFillAnimationDuration;
        [SerializeField] private float _releasingAnimationDuration;

        private Coroutine _animation;
        private Action _relesedCallback;
        private bool _isReleased;

        private void Start()
        {
            _image.color = _emptyColor;
            _isReleased = true;
        }

        private void OnValidate()
        {
            if(_releasingAnimationDuration < _halfFillAnimationDuration)
            {
                Debug.LogError($"{nameof(_releasingAnimationDuration)} should not be less {nameof(_halfFillAnimationDuration)}");
                _releasingAnimationDuration = _halfFillAnimationDuration;
            }
        }

        public void Fill()
        {
            if (_isReleased == false)
                _relesedCallback?.Invoke();

            TryStopCoroutine();

            _animation = StartCoroutine(Filling());
        }

        public void Release(Action callback = null)
        {
            TryStopCoroutine();

            _relesedCallback = callback;

            _animation = StartCoroutine(Releasing(_relesedCallback));
        }

        private void TryStopCoroutine()
        {
            if (_animation != null)
                StopCoroutine(_animation);
        }

        private IEnumerator Releasing(Action callback)
        {
            float passedTime = 0;
            float progress;

            _isReleased = false;

            transform.localScale = Vector3.one;

            while(_image.color != _emptyColor)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _releasingAnimationDuration;

                _image.color = Color.Lerp(_filedColor, _emptyColor, progress);

                yield return null;
            }

            _isReleased = true;
            callback?.Invoke();
        }

        private IEnumerator Filling()
        {
            _image.color = _filedColor;
            Vector3 startScale = Vector3.one;
            float passedTime = 0;
            float progress;

            while(_image.transform.localScale != _fillAnimationScale)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _halfFillAnimationDuration;

                _image.transform.localScale = Vector3.Lerp(startScale, _fillAnimationScale, progress);

                yield return null;
            }

            passedTime = 0;

            while (_image.transform.localScale != startScale)
            {
                passedTime += Time.deltaTime;
                progress = passedTime / _halfFillAnimationDuration;

                _image.transform.localScale = Vector3.Lerp(_fillAnimationScale, startScale, progress);

                yield return null;
            }
        }
    }
}
