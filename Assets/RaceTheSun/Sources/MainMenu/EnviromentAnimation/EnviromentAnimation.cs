using UnityEngine;

namespace Assets.RaceTheSun.Sources.MainMenu.EnviromentAnimation
{
    public class EnviromentAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _finishPosition;
        [SerializeField] private float _duration;

        private float _progress;
        private float _passedTime;

        private void Update()
        {
            _passedTime += Time.deltaTime;

            if (transform.position == _finishPosition)
            {
                transform.position = _startPosition;
                _passedTime = 0;
            }

            _progress = _passedTime / _duration;

            transform.position = Vector3.Lerp(_startPosition, _finishPosition, _progress);
        }
    }
}
