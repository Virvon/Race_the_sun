using Assets.RaceTheSun.Sources.Audio;
using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class SpaceshipShieldPortal : MonoBehaviour 
    {
        private IGameplayFactory _gameplayFactory;
        private SpaceshipMovement _spaceshipMovement;
        private Spaceship _spaceship;
        private GameplayCameras _cameras;
        private CollisionPortalPoint _collisionPortalPoint;
        private SpaceshipTurning _spaceshipTurning;
        private StageMusic _stageMusic;
        private PortalSound _portalSound;
        private Sun.Sun _sun;
        private Plane _plane;

        public event Action Activated;

        [Inject]
        private void Construct(IGameplayFactory gameplayFactory, SpaceshipMovement spaceshipMovement, Spaceship spaceship, GameplayCameras cameras, CollisionPortalPoint collisionPortalPoint, SpaceshipTurning spaceshipTurning, StageMusic stageMusic, PortalSound portalSound, Sun.Sun sun, Plane plane)
        {
            _gameplayFactory = gameplayFactory;
            _spaceshipMovement = spaceshipMovement;
            _spaceship = spaceship;
            _cameras = cameras;
            _collisionPortalPoint = collisionPortalPoint;
            _spaceshipTurning = spaceshipTurning;
            _stageMusic = stageMusic;
            _portalSound = portalSound;
            _sun = sun;
            _plane = plane;
        }

        public void Activate(bool createdCollisionPortal = true)
        {
            _stageMusic.Play();
            _portalSound.Play();

            Vector3 revivalPosition = new Vector3(_spaceship.transform.position.x, 120, _spaceship.transform.position.z + 2);
            transform.position = revivalPosition;

            _spaceshipMovement.Reset();
            _spaceshipTurning.Reset();
            
            if(createdCollisionPortal)
                _gameplayFactory.CreateShieldPortal(_collisionPortalPoint.transform.position);

            _gameplayFactory.CreateShieldPortal(revivalPosition);
            StartCoroutine(Revivler(revivalPosition, createdCollisionPortal));
        }

        private IEnumerator Revivler(Vector3 revivalPosition, bool includedCollisionPortalCamera)
        {
            if(includedCollisionPortalCamera)
            {
                _plane.HideEffect();
                _sun.IsStopped = true;
                _cameras.IncludeCamera(GameplayCameraType.CollisionPortalCamera);

                yield return new WaitForSeconds(1);
            }

            _cameras.IncludeCamera(GameplayCameraType.ShieldPortalCamera);

            yield return new WaitForSeconds(2f);

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
