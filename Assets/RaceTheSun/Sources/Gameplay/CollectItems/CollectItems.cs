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
        [SerializeField] private SpaceshipDie _spaceshipDie;
        [SerializeField] private SpaceshipJump _spaceshipJump;

        private IItemVisitor _itemVisitor;

        [Inject]
        private void Construct(ScoreCounter.ScoreCounter scoreCounter)
        {
            _itemVisitor = new ItemVisitor(scoreCounter, _spaceship, _spaceshipDie, _spaceshipJump);
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
            private readonly SpaceshipDie _spaceshipDie;
            private readonly SpaceshipJump _spaceshipJump;

            public ItemVisitor(ScoreCounter.ScoreCounter scoreCounter, Spaceship.Spaceship spaceship, SpaceshipDie spaceshipDie, SpaceshipJump spaceshipJump)
            {
                _scoreCounter = scoreCounter;
                _spaceship = spaceship;
                _spaceshipDie = spaceshipDie;
                _spaceshipJump = spaceshipJump;
            }

            public void Visit(Shield shield)
            {
                _spaceshipDie.TakeShield();
            }

            public void Visit(JumpBoost jumpBoost)
            {
                _spaceshipJump.TakeJumpBoost();
            }

            public void Visit(ScoreItem scoreItem)
            {
                _scoreCounter.TakeItem();
            }

            public void Visit(SpeedBoost speedBoost)
            {
                _spaceship.BoostSpeed();
            }

            public void Visit(MysteryBox mysteryBox)
            {
                
            }
        }
    }
}
