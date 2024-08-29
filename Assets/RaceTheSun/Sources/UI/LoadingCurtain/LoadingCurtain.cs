using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.LoadingCurtain
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private Image _curtain;

        private Color _color;

        private void Start()
        {
            _color = _curtain.color;
        }

        public void Show(float duration = 0, Action callback = null)
        {
            gameObject.SetActive(true);
            StartCoroutine(TransparentChanger(duration, new Color(_color.r, _color.g, _color.b, 0), _color, callback));
        }

        public void Hide(float duration = 0, Action callback = null)
        {
            StartCoroutine(TransparentChanger(duration, _color, new Color(_color.r, _color.g, _color.b, 0), callback: () =>
            {
                callback?.Invoke();
                gameObject.SetActive(false);
            }));
        }

        private IEnumerator TransparentChanger(float duration, Color startColor, Color targetColor, Action callback)
        {
            float progerss = 0;
            float time = 0;

            while (_curtain.color != targetColor)
            {
                time += Time.deltaTime;
                progerss = time / duration;
                _curtain.color = Color.Lerp(startColor, targetColor, progerss);

                yield return null;
            }

            callback?.Invoke();
        }

        public class Factory : PlaceholderFactory<string, UniTask<LoadingCurtain>>
        {
        }
    }
}
