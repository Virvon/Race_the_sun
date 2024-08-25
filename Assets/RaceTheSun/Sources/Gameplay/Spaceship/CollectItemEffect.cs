using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class CollectItemEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;

        public void Show()
        {
            _effect.Play();
        }
    }
}
