using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.Gameplay.Counters;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class CollectItems : MonoBehaviour
    {
        private readonly Collider[] _overlapColliders = new Collider[128];

        [SerializeField] private Spaceship.Spaceship _spaceship;
        [SerializeField] private SpaceshipDie _spaceshipDie;
        [SerializeField] private SpaceshipJump _spaceshipJump;
        [SerializeField] private CollectItemEffect _collectItemEffect;
        [SerializeField] private ParticleSystem _destroyItemEffectPrefab;

        private float _collectRadius;
        private IPersistentProgressService _persistentProgressService;
        private IItemVisitor _itemVisitor;

        [Inject]
        private void Construct(ScoreItemsCounter scoreItemsCounter, IPersistentProgressService persistentProgressService, CollectItemsSoundEffects collectItemsSoundEffects)
        {
            _persistentProgressService = persistentProgressService;
            _itemVisitor = new ItemVisitor(_spaceship, _spaceshipDie, _spaceshipJump, scoreItemsCounter, persistentProgressService, collectItemsSoundEffects);
        }

        private void Start()
        {
            _collectRadius = _persistentProgressService.Progress.AvailableSpaceships.GetCurrentSpaceshipData().PickUpRange.Value + _spaceship.AttachmentStats.CollectRadius;
        }

        private void Update()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _collectRadius, _overlapColliders);

            for(int i = 0; i < overlapCount; i++)
            {
                if (_overlapColliders[i].TryGetComponent(out IItem item))
                {
                    TakeItem(item);
                    Instantiate(_destroyItemEffectPrefab, _overlapColliders[i].transform.position, Quaternion.identity).GetComponent<DestroyItemEffect>().SetColor(item.DestroyEffectColor);
                    _collectItemEffect.Show();
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
            private readonly Spaceship.Spaceship _spaceship;
            private readonly SpaceshipDie _spaceshipDie;
            private readonly SpaceshipJump _spaceshipJump;
            private readonly ScoreItemsCounter _scoreItemsCounter;
            private readonly IPersistentProgressService _persistentProgressService;
            private readonly CollectItemsSoundEffects _collectItemsSoundEffects;

            public ItemVisitor(Spaceship.Spaceship spaceship, SpaceshipDie spaceshipDie, SpaceshipJump spaceshipJump, ScoreItemsCounter scoreItemsCounter, IPersistentProgressService persistentProgressService, CollectItemsSoundEffects collectItemsSoundEffects)
            {
                _spaceship = spaceship;
                _spaceshipDie = spaceshipDie;
                _spaceshipJump = spaceshipJump;
                _scoreItemsCounter = scoreItemsCounter;
                _persistentProgressService = persistentProgressService;
                _collectItemsSoundEffects = collectItemsSoundEffects;
            }

            public void Visit(Shield shield)
            {
                _spaceshipDie.GiveShield();
                _collectItemsSoundEffects.TakeItem();
            }

            public void Visit(JumpBoost jumpBoost)
            {
                _spaceshipJump.GiveJumpBoost();
                _collectItemsSoundEffects.TakeItem();
            }

            public void Visit(ScoreItem scoreItem)
            {
                _scoreItemsCounter.Give();
                _collectItemsSoundEffects.TakeScoreItem();
            }

            public void Visit(SpeedBoost speedBoost)
            {
                _spaceship.BoostSpeed();
            }

            public void Visit(MysteryBox mysteryBox)
            {
                _persistentProgressService.Progress.MysteryBoxes.Give();
                _collectItemsSoundEffects.TakeItem();
            }
        }
    }
}
