using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class CollectItems : MonoBehaviour
    {
        [SerializeField] private SpaceshipDie _spaceshipDie;
        [SerializeField] private SpaceshipJump _spaceShipJump;

        private ScoreCounter.ScoreCounter _scoreCounter;
        private IPersistentProgressService _persistentProgress;

        [Inject]
        private void Construct(ScoreCounter.ScoreCounter scoreCounter, IPersistentProgressService persistentProgress)
        {
            _scoreCounter = scoreCounter;
            _persistentProgress = persistentProgress;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out Item _))
            {
                _persistentProgress.Progress.Wallet.Take(1);
                _scoreCounter.TakeItem();
                Destroy(other.transform.gameObject);
            }
            else if(other.transform.TryGetComponent(out Shield _))
            {
                _spaceshipDie.TakeShield();
                Destroy(other.transform.gameObject);
            }
            else if(other.transform.TryGetComponent(out JumpBoost _))
            {
                _spaceShipJump.TakeJumpBoost();
                Destroy(other.transform.gameObject);
            }
        }
    }
}
