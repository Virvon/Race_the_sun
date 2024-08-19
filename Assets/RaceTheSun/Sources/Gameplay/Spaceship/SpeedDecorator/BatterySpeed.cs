﻿using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class BatterySpeed : SpeedDecorator
    {
        private const float StopSpeed = 0.2f;

        private readonly Battery _battery;

        private float _speedMultiplier;
        private SpaceshipDie _spaceshipDie;
        private bool _isStopped;

        public BatterySpeed(ISpeedProvider wrappedEntity, Battery battery, SpaceshipDie spaceshipDie) : base(wrappedEntity)
        {
            _battery = battery;
            _spaceshipDie = spaceshipDie;
            _speedMultiplier = 1;
            _isStopped = false;
        }

        protected override float GetSpeedInternal()
        {
            if (_battery.Discharged == false)
            {
                if(_battery.Value > 0.5f)
                    _speedMultiplier = 1;
                else
                    _speedMultiplier -= Time.deltaTime * StopSpeed;
            }
            else
            {
                _speedMultiplier = 0;
                
                if(_isStopped == false)
                {
                    _isStopped = true;
                    _spaceshipDie.Stop();
                }
            }

            _speedMultiplier = _speedMultiplier <= 0 ? 0 : _speedMultiplier;

            return WrappedEntity.GetSpeed() * _speedMultiplier;
        }
    }
}
