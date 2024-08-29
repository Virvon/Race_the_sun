using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class BatterySpeed : SpeedDecorator
    {
        private const float StopSpeed = 0.2f;
        private const float HalfBatteryValue = 0.5f;
        private readonly Battery.Battery _battery;

        private float _speedMultiplier;
        private SpaceshipDie _spaceshipDie;
        private bool _isStopped;
        private SpaceshipTurning _spaceshipTurning;

        public BatterySpeed(ISpeedProvider wrappedEntity, Battery.Battery battery, SpaceshipDie spaceshipDie, SpaceshipTurning spaceshipTurning)
            : base(wrappedEntity)
        {
            _battery = battery;
            _spaceshipDie = spaceshipDie;
            _speedMultiplier = 1;
            _isStopped = false;
            _spaceshipTurning = spaceshipTurning;
        }

        protected override float GetSpeedInternal()
        {
            if (_battery.Discharged == false)
            {
                if (_battery.Value > HalfBatteryValue)
                    _speedMultiplier = 1;
                else
                    _speedMultiplier -= Time.deltaTime * StopSpeed;
            }
            else
            {
                _speedMultiplier = 0;

                if (_isStopped == false)
                {
                    _isStopped = true;
                    _spaceshipDie.Stop();
                    _spaceshipTurning.CanTurn = false;
                }
            }

            _speedMultiplier = _speedMultiplier <= 0 ? 0 : _speedMultiplier;

            return WrappedEntity.GetSpeed() * _speedMultiplier;
        }
    }
}
