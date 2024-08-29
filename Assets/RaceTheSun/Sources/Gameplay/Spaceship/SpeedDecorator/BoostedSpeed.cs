using System.Collections;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class BoostedSpeed : SpeedDecorator
    {
        private const float BoostValue = 100;
        private const float AccelerationDuration = 1;
        private const float BoostedSpeedTime = 8;

        private readonly float _defaultSpeed;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly Sun.Sun _sun;

        private float _speed;
        private bool _isBoosted;
        private Coroutine _booster;

        public BoostedSpeed(ISpeedProvider wrappedEntity, float defaultSpeed, MonoBehaviour coroutineRunner, Sun.Sun sun) : base(wrappedEntity)
        {
            _defaultSpeed = defaultSpeed;
            _coroutineRunner = coroutineRunner;
            _sun = sun;
        }

        public void Boost()
        {
            if (_booster != null)
                _coroutineRunner.StopCoroutine(_booster);

            _booster = _coroutineRunner.StartCoroutine(Booster());
        }

        public void StopBoost()
        {
            if (_booster != null)
                _coroutineRunner.StopCoroutine(_booster);

            _isBoosted = false;

            _sun.SetMovementDirection(true);
        }

        protected override float GetSpeedInternal() =>
            _isBoosted ? _speed : WrappedEntity.GetSpeed();

        private IEnumerator Booster()
        {
            float startSpeed = WrappedEntity.GetSpeed();
            float targetSpeed = startSpeed + BoostValue;
            float time = 0;
            float progress;

            _speed = startSpeed;
            _isBoosted = true;

            _sun.SetMovementDirection(false);

            while (_speed != targetSpeed)
            {
                time += Time.deltaTime;
                progress = time / AccelerationDuration;

                _speed = Mathf.Lerp(startSpeed, targetSpeed, progress);

                yield return null;
            }

            _sun.SetMovementDirection(true);

            yield return new WaitForSeconds(BoostedSpeedTime);

            startSpeed = _speed;
            targetSpeed = _defaultSpeed;
            time = 0;
            progress = 0;

            while (_speed != targetSpeed)
            {
                time += Time.deltaTime;
                progress = time / AccelerationDuration;

                _speed = Mathf.Lerp(startSpeed, targetSpeed, progress);

                yield return null;
            }

            _isBoosted = false;
        }
    }
}
