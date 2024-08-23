using Assets.RaceTheSun.Sources.Attachment;
using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class Spaceship : MonoBehaviour
    {
        private const float DefaultSpeed = 300;

        [SerializeField] private SpaceshipMovement _spaceshipMovement;
        [SerializeField] private Battery _battery;
        [SerializeField] private SpaceshipDie _spaceshipDie;
        [SerializeField] private SpaceshipTurning _spaceshipTurning;

        private BoostedSpeed _boostedSpeed;
        private CollisionSpeed _collisionSpeed;
        private Sun.Sun _sun;
        private ISpeedProvider _speedProvider;

        public event Action SpeedBoosted;
       

        [Inject]
        private void Construct(ScoreCounter.ScoreCounter scoreCounter, Attachment.Attachment attachment, IPersistentProgressService persistentProgressService)
        {
            scoreCounter.Init(this);

            AttachmentStats = GetAttachmentStats(attachment, persistentProgressService);
        }

        public float Speed { get; private set; }
        public AttachmentStats AttachmentStats { get; private set; }

        private void Update()
        {
            if(_speedProvider != null)
                Speed = _speedProvider.GetSpeed();
        }

        public void Init(Sun.Sun sun)
        {
            _sun = sun;
        }

        public bool GetCollisionPerStage()
        {
            bool isCollidedPerStage = _collisionSpeed.IsCollidedPerStage;

            _collisionSpeed.Reset();

            return isCollidedPerStage;
        }

        public void BoostSpeed()
        {
            _boostedSpeed.Boost();
            SpeedBoosted?.Invoke();
        }

        public void StopBoostSpeed()
        {
            _boostedSpeed.StopBoost();
        }

        public void UpdateSpeedDecorator()
        {
            _speedProvider = new SpaceshipSpeed(DefaultSpeed);
            _collisionSpeed = new CollisionSpeed(_speedProvider, _spaceshipMovement, DefaultSpeed, _spaceshipDie);
            _speedProvider = _collisionSpeed;
            _boostedSpeed = new BoostedSpeed(_speedProvider, DefaultSpeed, this, _sun);
            _speedProvider = _boostedSpeed;
            _speedProvider = new BatterySpeed(_speedProvider, _battery, _spaceshipDie, _spaceshipTurning);
        }

        private AttachmentStats GetAttachmentStats(Attachment.Attachment attachment, IPersistentProgressService persistentProgressService)
        {
            SpaceshipData spaceshipData = persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType);

            IAttachmentStatsProvider attachmentStatsProvider = new DefaultAttachmentStats();

            foreach(Upgrading.UpgradeType upgradeType in spaceshipData.UpgradeTypes)
                attachmentStatsProvider = attachment.Wrap(upgradeType, attachmentStatsProvider);

            return attachmentStatsProvider.GetStats();
        }

        public class Factory : PlaceholderFactory<string, UniTask<Spaceship>>
        {
        }
    }
}
