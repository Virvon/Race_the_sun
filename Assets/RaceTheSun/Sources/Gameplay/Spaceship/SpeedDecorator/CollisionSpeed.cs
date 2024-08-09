using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class CollisionSpeed : SpeedDecorator
    {
        private const float MinSpeed = 60;
        private const float DestoryDot = -0.5f;
        private const float AccelerationFromMinSpeed = 15;

        private readonly SpaceshipMovement _spaceshipMovement;
        private readonly float _defaultSpeed;
        private readonly SpaceshipDie _spaceshipDie;
        

        private float _speed;

        public CollisionSpeed(ISpeedProvider wrappedEntity, SpaceshipMovement spaceshipMovement, float defaultSpeed, SpaceshipDie spaceshipDie) : base(wrappedEntity)
        {
            _spaceshipMovement = spaceshipMovement;
            _defaultSpeed = defaultSpeed;
            _speed = _defaultSpeed;
            _spaceshipDie = spaceshipDie;
        }

        protected override float GetSpeedInternal()
        {
            if(_speed < _defaultSpeed && _speed != 0)
            {
                float speedFactor = AccelerationFromMinSpeed * Time.deltaTime;

                if (_speed + speedFactor > _defaultSpeed)
                    _speed = _defaultSpeed;
                else
                    _speed += speedFactor;
            }

            if (_spaceshipMovement.IsCollided)
            {
                if (_spaceshipMovement.CollisionInfo.Dot > DestoryDot)
                    _speed = MinSpeed;
                else
                    _spaceshipDie.TryRevive();
            }

            return _speed;
        }
    }
}
