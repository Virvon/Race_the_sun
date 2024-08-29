using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.GameLogic.Audio;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Portals
{
    public class SpaceshipShieldPortal : MonoBehaviour
    {
        private const int CollisionPortalShowDuration = 1;
        private const int ShieldPortalShowDuration = 2;

        private IGameplayFactory _gameplayFactory;
        private SpaceshipMovement _spaceshipMovement;
        private Spaceship.Spaceship _spaceship;
        private GameplayCameras _cameras;
        private CollisionPortalPoint _collisionPortalPoint;
        private SpaceshipTurning _spaceshipTurning;
        private StageMusic _stageMusic;

        [Inject(Id = GameplayFactoryInjectId.PortalSound)]
        private SoundPlayer _portalSound;

        private Sun.Sun _sun;
        private Spaceship.Plane _plane;

        public event Action Activated;

        [Inject]
        private void Construct(
            IGameplayFactory gameplayFactory,
            SpaceshipMovement spaceshipMovement,
            Spaceship.Spaceship spaceship,
            GameplayCameras cameras,
            CollisionPortalPoint collisionPortalPoint,
            SpaceshipTurning spaceshipTurning,
            StageMusic stageMusic,
            Sun.Sun sun,
            Spaceship.Plane plane)
        {
            _gameplayFactory = gameplayFactory;
            _spaceshipMovement = spaceshipMovement;
            _spaceship = spaceship;
            _cameras = cameras;
            _collisionPortalPoint = collisionPortalPoint;
            _spaceshipTurning = spaceshipTurning;
            _stageMusic = stageMusic;
            _sun = sun;
            _plane = plane;
        }

        public void Activate(bool createdCollisionPortal = true)
        {
            _stageMusic.Play();
            _portalSound.Play();

            Vector3 revivalPosition = new Vector3(_spaceship.transform.position.x, 120, _spaceship.transform.position.z + 2);
            transform.position = revivalPosition;

            _spaceshipMovement.Restart();
            _spaceshipTurning.Restart();

            if (createdCollisionPortal)
                _gameplayFactory.CreateShieldPortal(_collisionPortalPoint.transform.position);

            _gameplayFactory.CreateShieldPortal(revivalPosition);
            StartCoroutine(Revivler(revivalPosition, createdCollisionPortal));
        }

        private IEnumerator Revivler(Vector3 revivalPosition, bool includedCollisionPortalCamera)
        {
            if (includedCollisionPortalCamera)
            {
                _plane.HideEffect();
                _sun.IsStopped = true;
                _cameras.IncludeCamera(GameplayCameraType.CollisionPortalCamera);

                yield return new WaitForSeconds(CollisionPortalShowDuration);
            }

            _cameras.IncludeCamera(GameplayCameraType.ShieldPortalCamera);

            yield return new WaitForSeconds(ShieldPortalShowDuration);

            _spaceship.transform.position = revivalPosition;
            _spaceshipMovement.IsStopped = false;
            _spaceship.gameObject.SetActive(true);

            Activated?.Invoke();

            _plane.ShowEffect();
            _sun.IsStopped = false;
            _cameras.IncludeCamera(GameplayCameraType.MainCamera);
        }

        public class Factory : PlaceholderFactory<string, UniTask<SpaceshipShieldPortal>>
        {
        }
    }
}
