using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems.Effects
{
    public class CollectItemEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;

        public void Show() =>
            _effect.Play();
    }
}
