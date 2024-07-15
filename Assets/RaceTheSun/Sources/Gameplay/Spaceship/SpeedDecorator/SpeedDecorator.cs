namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public abstract class SpeedDecorator : ISpeedProvider
    {
        protected readonly ISpeedProvider WrappedEntity;

        protected SpeedDecorator(ISpeedProvider wrappedEntity) =>
            WrappedEntity = wrappedEntity;

        public float GetSpeed() =>
            GetSpeedInternal();

        protected abstract float GetSpeedInternal();
    }
}
