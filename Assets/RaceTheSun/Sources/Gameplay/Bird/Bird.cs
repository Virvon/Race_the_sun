using Assets.RaceTheSun.Sources.Gameplay.CollectItems;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Upgrading;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Bird
{
    public class Bird : MonoBehaviour
    {
        private const float Delta = 0.5f;
        private const float ScoreItemPositionY = 0.5f;
        private const float ItemLiveTime = 5;
        private const int MinItemNumber = 1;
        private readonly Vector3 _dropPositionOffset = new Vector3(0, -5, 0);

        [SerializeField] private float _speed;
        [SerializeField] private float _itemDropSpeed;

        private Vector3[] _movementPath;
        private bool _isMovementPathEnded;
        private int _targetPointIndex;
        private IGameplayFactory _gameplayFactory;
        private IPersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IGameplayFactory gameplayFactory, IPersistentProgressService persistentProgerssService)
        {
            _gameplayFactory = gameplayFactory;
            _persistentProgressService = persistentProgerssService;
        }

        private void Start()
        {
            _isMovementPathEnded = true;
        }

        private async void Update()
        {
            if (_isMovementPathEnded)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _movementPath[_targetPointIndex], _speed * Time.deltaTime);

            if(Vector3.Distance(transform.position, _movementPath[_targetPointIndex]) <= Delta)
            {
                if (_targetPointIndex < _movementPath.Length - 1)
                {
                    _targetPointIndex++;
                    
                    if(_targetPointIndex < _movementPath.Length - 1)
                    {
                        ScoreItem scoreItem = await _gameplayFactory.CreateScoreItem(transform.position + _dropPositionOffset);
                        StartCoroutine(ItemMover(scoreItem.transform, ScoreItemPositionY));
                    }
                    else
                    {
                        await CreateBonusItem();
                    }
                }
                else
                {
                    _isMovementPathEnded = true;
                }
            }
        }

        public void Move(Vector3[] movementPath)
        {
            _movementPath = movementPath;
            transform.position = movementPath[0];
            _isMovementPathEnded = false;
            _targetPointIndex = 1;
        }

        private async UniTask CreateBonusItem()
        {
            bool isItmeChoosed = false;
            Transform itemTransform = null;
            float targetPositionY = 0;

            while(isItmeChoosed == false)
            {
                int itemIndex = Random.Range(MinItemNumber, 4);

                switch (itemIndex)
                {
                    case 1:
                        SpeedBoost speedBoost = await _gameplayFactory.CreateSpeedBoost(transform.position + _dropPositionOffset);
                        itemTransform = speedBoost.transform;
                        targetPositionY = 3;
                        isItmeChoosed = true;
                        break;
                    case 2:
                        if (_persistentProgressService.Progress.Upgrading.IsUpgraded(UpgradeType.JumpBoost) == false)
                            continue;

                        JumpBoost jumpBoost = await _gameplayFactory.CreateJumpBoost(transform.position + _dropPositionOffset);
                        itemTransform = jumpBoost.transform;
                        targetPositionY = 3;
                        isItmeChoosed = true;
                        break;
                    case 3:
                        if (_persistentProgressService.Progress.Upgrading.IsUpgraded(UpgradeType.ShieldPortal) == false)
                            continue;

                        Shield shield = await _gameplayFactory.CreateShield(transform.position + _dropPositionOffset);
                        itemTransform = shield.transform;
                        targetPositionY = 3;
                        isItmeChoosed = true;
                        break;
                }
            }

            StartCoroutine(ItemMover(itemTransform, targetPositionY));

            return;
        }

        private IEnumerator ItemMover(Transform transform, float targetPositionY)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, targetPositionY, transform.position.z);
            Vector3 startPosition = transform.position;
            float passedTime = 0;
            float progress;

            while (transform.position != targetPosition)
            {
                passedTime += Time.deltaTime + passedTime * Time.deltaTime;
                progress = passedTime / _itemDropSpeed;

                transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

                yield return null;
            }

            Destroy(transform.gameObject, ItemLiveTime);
        }

        public class Factory : PlaceholderFactory<string, UniTask<Bird>>
        {
        }
    }
}
