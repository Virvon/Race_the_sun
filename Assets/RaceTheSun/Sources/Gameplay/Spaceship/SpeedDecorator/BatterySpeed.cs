using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class BatterySpeed : SpeedDecorator
    {
        private const float StopSpeed = 0.2f;

        private readonly Battery _battery;

        private float _speedMultiplier;

        public BatterySpeed(ISpeedProvider wrappedEntity, Battery battery) : base(wrappedEntity)
        {
            _battery = battery;
            _speedMultiplier = 1;
        }

        protected override float GetSpeedInternal()
        {
            if (_battery.Discharged == false)
            {
                _speedMultiplier = 1;
            }
            else
            {
                _speedMultiplier -= Time.deltaTime * StopSpeed;

                _speedMultiplier = _speedMultiplier <= 0 ? 0 : _speedMultiplier;
            }

            return WrappedEntity.GetSpeed() * _speedMultiplier;
        }
    }
}
