using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems.Effects
{
    public class DestroyItemEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _destroyDelay;

        private void Start() =>
            Destroy(gameObject, _destroyDelay);

        public void SetColor(Color color)
        {
            ParticleSystem.MainModule settings = _particleSystem.main;

            settings.startColor = new ParticleSystem.MinMaxGradient(color);
        }
    }
}