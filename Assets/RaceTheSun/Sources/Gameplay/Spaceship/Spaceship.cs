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

        private BoostedSpeed _boostedSpeed;
        private Sun.Sun _sun;
        private ISpeedProvider _speedProvider;
        private AttachmentStats _attachmentStats;

        [Inject]
        private void Construct(ScoreCounter.ScoreCounter scoreCounter, Attachment.Attachment attachment, IPersistentProgressService persistentProgressService)
        {
            scoreCounter.Init(this);

            _attachmentStats = GetAttachmentStats(attachment, persistentProgressService);

            Debug.Log(_attachmentStats.CollectRadius);
            Debug.Log(_attachmentStats.MaxJumpBoostsCount);
            Debug.Log(_attachmentStats.MaxShileldsCount);
        }

        public float Speed { get; private set; }

        private void Update()
        {
            if(_speedProvider != null)
                Speed = _speedProvider.GetSpeed();
        }

        public void Init(Sun.Sun sun)
        {
            _sun = sun;
        }

        public void BoostSpeed()
        {
            _boostedSpeed.Boost();
        }

        public void StopBoostSpeed()
        {
            _boostedSpeed.StopBoost();
        }

        public void UpdateSpeedDecorator()
        {
            _speedProvider = new SpaceshipSpeed(DefaultSpeed);
            _speedProvider = new CollisionSpeed(_speedProvider, _spaceshipMovement, DefaultSpeed, _spaceshipDie);
            _boostedSpeed = new BoostedSpeed(_speedProvider, DefaultSpeed, this, _sun);
            _speedProvider = _boostedSpeed;
            _speedProvider = new BatterySpeed(_speedProvider, _battery);
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
