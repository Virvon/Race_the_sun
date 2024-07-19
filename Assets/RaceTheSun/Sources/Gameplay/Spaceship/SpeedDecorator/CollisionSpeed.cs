﻿using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class CollisionSpeed : SpeedDecorator
    {
        private const float MinSpeed = 60;
        private const float DestoryDot = -0.5f;
        private const float AccelerationFromMinSpeed = 15;

        private readonly Collision _collision;
        private readonly float _defaultSpeed;
        private readonly StartMovement _startMovement;

        private float _speed;

        public CollisionSpeed(ISpeedProvider wrappedEntity, Collision collision, float defaultSpeed, StartMovement startMovement) : base(wrappedEntity)
        {
            _collision = collision;
            _defaultSpeed = defaultSpeed;
            _speed = _defaultSpeed;
            _startMovement = startMovement;
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

            if (_collision.IsCollided)
            {
                if (_collision.Dot > DestoryDot)
                    _speed = MinSpeed;
                else
                    _startMovement.MoveUp();
            }

            return _speed;
        }
    }
}
