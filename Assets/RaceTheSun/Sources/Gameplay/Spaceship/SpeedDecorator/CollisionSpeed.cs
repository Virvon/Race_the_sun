using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.SpeedDecorator
{
    public class CollisionSpeed : SpeedDecorator
    {
        private const float MinSpeed = 150;
        private const float DestoryDot = -0.5f;
        private const float AccelerationFromMinSpeed = 20;

        private readonly SpaceshipMovement _spaceshipMovement;
        private readonly float _defaultSpeed;
        private readonly SpaceshipDie _spaceshipDie;
        private readonly IGameplayFactory _gameplayFactory;
        private readonly GameplayCameras _gameplayCameras;
        
        private float _speed;

        public CollisionSpeed(ISpeedProvider wrappedEntity, SpaceshipMovement spaceshipMovement, float defaultSpeed, SpaceshipDie spaceshipDie, GameplayCameras gameplayCameras, IGameplayFactory gameplayFactory) : base(wrappedEntity)
        {
            _spaceshipMovement = spaceshipMovement;
            _defaultSpeed = defaultSpeed;
            _speed = _defaultSpeed;
            _spaceshipDie = spaceshipDie;
            _gameplayFactory = gameplayFactory;

            IsCollidedPerStage = false;
            _gameplayCameras = gameplayCameras;
        }

        public bool IsCollidedPerStage { get; private set; }

        public void Reset()
        {
            IsCollidedPerStage = false;
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
                {
                    _speed = MinSpeed;
                    _gameplayCameras.SpaceshipMainCamera.Shake();
                    _gameplayFactory.CreateCollisionFx(_spaceshipMovement.CollisionInfo.CollisionPosition, _spaceshipMovement.transform);
                }
                else if (_spaceshipDie.TryRevive() == false)
                {
                    _speed = 0;
                }

                IsCollidedPerStage = true;
            }

            return _speed;
        }
    }
}
