using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class CollisionFx : MonoBehaviour
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
            Debug.Log("instantiate");
            Instantiate(_particleSystemPrefab, transform.position + _instantiateOffset, Quaternion.identity);
        }
    }
}
