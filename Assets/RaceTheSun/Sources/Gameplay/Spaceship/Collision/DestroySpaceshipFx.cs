using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Collision
{
    public class DestroySpaceshipFx : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystemPrefab;
        [SerializeField] private SpaceshipDie _spaceshipDie;

        private readonly Vector3 _instantiateOffset = new Vector3(0, 0, -1);

        private void Start()
        {
            _spaceshipDie.Died += OnDied;
        }

        private void OnDestroy()
        {
            _spaceshipDie.Died -= OnDied;
        }

        private void OnDied()
        {
            Instantiate(_particleSystemPrefab, transform.position + _instantiateOffset, Quaternion.identity);
        }
    }
}
