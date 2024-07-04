using Assets.RaceTheSun.Sources.Gameplay.ScoreCounter;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class MultiplierProgressView : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private GameObject _prefab;

        private ScoreCounter _scoreCounter;
        private List<GameObject> _prefabs;

        [Inject]
        private void Construct(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;

            _scoreCounter.MultiplierProgressChanged += OnMultiplierProgressChanged;
            _prefabs = new();
        }

        private void OnDestroy()
        {
            _scoreCounter.MultiplierProgressChanged -= OnMultiplierProgressChanged;
        }

        private void OnMultiplierProgressChanged(int multiplierProgress)
        {
            if (_prefabs.Count > multiplierProgress)
            {
                foreach (var prefab in _prefabs)
                    Destroy(prefab);

                _prefabs.Clear();
            }

            GameObject x = Instantiate(_prefab, transform.position + new Vector3(_distance * multiplierProgress, 0, 0), Quaternion.identity, transform);

            _prefabs.Add(x);
        }
    }
}
