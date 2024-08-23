using Assets.RaceTheSun.Sources.Gameplay.Cameras;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.CoroutineRunner;
using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship
{
    public class StartMovement : MonoBehaviour
    {
        [SerializeField] private Spaceship _spaceship;
        [SerializeField] private SpaceshipMovement _spaceshipMovement;
        [SerializeField] private CollisionPortalPoint _collisionPortalPoint;
        [SerializeField] private SpaceshipTurning _spaceshipTurning;

        private Cameras.GameplayCameras _cameras;

        [Inject]
        private void Construct(Cameras.GameplayCameras cameras)
        {
            _cameras = cameras;
        }

        public void Move(Action endCallback = null)
        {
            StartCoroutine(SpaceshipMover(endCallback));
        }

        public void MoveUpper(Action startCallback = null, Action finishCallback = null)
        {
            StartCoroutine(UpperMover(startCallback, finishCallback));
        }

        private IEnumerator UpperMover(Action startCallback, Action finishCallback)
        {
            _spaceship.transform.position = new Vector3(0, 2.4f, 0);
            _cameras.IncludeCamera(Cameras.GameplayCameraType.UpperCamera);

            yield return new WaitForSeconds(1);
            startCallback?.Invoke();

            _cameras.IncludeCamera(Cameras.GameplayCameraType.MainCamera);
            finishCallback?.Invoke();
        }

        private IEnumerator SpaceshipMover(Action endCallback)
        {
            Vector3 startPosition = new Vector3(0, 6.2f, -900);
            Vector3 endPosition = new Vector3(0, 6.2f, 100);
            float duration = 3;
            float time = 0;

            _spaceshipMovement.IsStopped = true;
            _spaceshipTurning.CanTurn = false;
            _cameras.IncludeCamera(Cameras.GameplayCameraType.StartCamera);

            while (_spaceship.transform.position != endPosition)
            {
                time += Time.deltaTime;
                float progress = time / duration;

                _spaceship.transform.position = Vector3.Lerp(startPosition, endPosition, progress);

                yield return null;
            }

            _cameras.IncludeCamera(Cameras.GameplayCameraType.MainCamera);
            _spaceshipMovement.IsStopped = false;
            _spaceshipTurning.CanTurn = true;
            endCallback?.Invoke();
        }
    }
}
