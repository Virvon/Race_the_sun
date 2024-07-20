using Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class Spaceship : MonoBehaviour
    {
        private const float DefaultSpeed = 100;

        [SerializeField] private SpaceshipMovement _spaceshipMovement;
        [SerializeField] private Battery _battery;

        private BoostedSpeed _boostedSpeed;

        public ISpeedProvider SpeedProvider { get; private set; }

        private void Start()
        {
            CreateSpeedDecorator();
        }

        public void BoostSpeed()
        {
            _boostedSpeed.Boost();
        }

        public void StopBoostSpeed()
        {
            _boostedSpeed.StopBoost();
        }

        private void CreateSpeedDecorator()
        {
            SpeedProvider = new SpaceshipSpeed(DefaultSpeed);
            SpeedProvider = new CollisionSpeed(SpeedProvider, _spaceshipMovement, DefaultSpeed, GetComponentInChildren<StartMovement>());
            _boostedSpeed = new BoostedSpeed(SpeedProvider, DefaultSpeed, this);
            SpeedProvider = _boostedSpeed;
            SpeedProvider = new BatterySpeed(SpeedProvider, _battery);
        }

        public class Factory : PlaceholderFactory<string, UniTask<Spaceship>>
        {
        }
    }
}
