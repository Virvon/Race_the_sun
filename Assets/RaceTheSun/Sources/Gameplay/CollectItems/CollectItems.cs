using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class CollectItems : MonoBehaviour
    {
        private readonly Collider[] _overlapColliders = new Collider[128];

        [SerializeField] private float _collectRadius;
        [SerializeField] private Spaceship.Spaceship _spaceship;

        private IItemVisitor _itemVisitor;

        [Inject]
        private void Construct(ScoreCounter.ScoreCounter scoreCounter)
        {
            _itemVisitor = new ItemVisitor(scoreCounter, _spaceship);
        }

        private void Update()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _collectRadius, _overlapColliders);

            for(int i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IItem item))
                {
                    TakeItem(item);
                    Destroy(_overlapColliders[i].gameObject);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _collectRadius);
        }

        private void TakeItem(IItem item)
        {
            item.Accept(_itemVisitor);
        }

        private class ItemVisitor : IItemVisitor
        {
            private readonly ScoreCounter.ScoreCounter _scoreCounter;
            private readonly Spaceship.Spaceship _spaceship;

            public ItemVisitor(ScoreCounter.ScoreCounter scoreCounter, Spaceship.Spaceship spaceship)
            {
                _scoreCounter = scoreCounter;
                _spaceship = spaceship;
            }

            public void Visit(Shield shield)
            {
                Debug.Log("shield");
            }

            public void Visit(JumpBoost jumpBoost)
            {
                Debug.Log("jumpboost");
            }

            public void Visit(ScoreItem scoreItem)
            {
                _scoreCounter.TakeItem();
            }

            public void Visit(SpeedBoost speedBoost)
            {
                _spaceship.BoostSpeed();
            }
        }
        //[SerializeField] private SpaceshipDie _spaceshipDie;
        //[SerializeField] private SpaceshipJump _spaceShipJump;

        //private ScoreCounter.ScoreCounter _scoreCounter;
        //private IPersistentProgressService _persistentProgress;

        //[Inject]
        //private void Construct(ScoreCounter.ScoreCounter scoreCounter, IPersistentProgressService persistentProgress)
        //{
        //    _scoreCounter = scoreCounter;
        //    _persistentProgress = persistentProgress;
        //}

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.transform.TryGetComponent(out Item _))
        //    {
        //        _persistentProgress.Progress.Wallet.Take(1);
        //        _scoreCounter.TakeItem();
        //        Destroy(other.transform.gameObject);
        //    }
        //    else if(other.transform.TryGetComponent(out Shield _))
        //    {
        //        _spaceshipDie.TakeShield();
        //        Destroy(other.transform.gameObject);
        //    }
        //    else if(other.transform.TryGetComponent(out JumpBoost _))
        //    {
        //        _spaceShipJump.TakeJumpBoost();
        //        Destroy(other.transform.gameObject);
        //    }
        //}
    }
}
