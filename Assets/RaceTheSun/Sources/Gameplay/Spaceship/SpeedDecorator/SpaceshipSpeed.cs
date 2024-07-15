namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class SpaceshipSpeed : ISpeedProvider
    {
        private readonly float _defaultSpeed;

        public SpaceshipSpeed(float defaultSpeed)
        {
            _defaultSpeed = defaultSpeed;
        }

        public float GetSpeed()
        {
            return _defaultSpeed;
        }
    }
}
