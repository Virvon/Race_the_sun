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

        public ISpeedProvider SpeedProvider { get; private set; }

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
            SpeedProvider = new SpaceshipSpeed(DefaultSpeed);
            SpeedProvider = new CollisionSpeed(SpeedProvider, _spaceshipMovement, DefaultSpeed, _spaceshipDie);
            _boostedSpeed = new BoostedSpeed(SpeedProvider, DefaultSpeed, this, _sun);
            SpeedProvider = _boostedSpeed;
            SpeedProvider = new BatterySpeed(SpeedProvider, _battery);
        }

        

        public class Factory : PlaceholderFactory<string, UniTask<Spaceship>>
        {
        }
    }
}
