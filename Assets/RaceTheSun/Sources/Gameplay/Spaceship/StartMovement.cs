using Assets.RaceTheSun.Sources.Gameplay.Cameras;
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

        private Cameras.GameplayCameras _cameras;
        private SpaceshipShieldPortal _spaceshipShieldPortal;

        [Inject]
        private void Construct(Cameras.GameplayCameras cameras, SpaceshipShieldPortal spaceshipShieldPortal)
        {
            _cameras = cameras;
            _spaceshipShieldPortal = spaceshipShieldPortal;
        }

        public void MoveUp()
        {
            StartCoroutine(UpMover());
        }

        public void Move(Action endCallback = null)
        {
            StartCoroutine(SpaceshipMover(endCallback));
        }

        public void MoveUpper(Action startCallback = null, Action finishCallback = null)
        {
            StartCoroutine(UpperMover(startCallback, finishCallback));
        }

        private IEnumerator UpMover()
        {
            SpaceshipMovement spaceshipMovement = _spaceship.GetComponentInChildren<SpaceshipMovement>();
            spaceshipMovement.IsStopped = true;
            _spaceshipShieldPortal.Activate(_collisionPortalPoint);
            _cameras.IncludeCamera(Cameras.GameplayCameraType.CollisionPortalCamera);

            yield return new WaitForSeconds(1);

            _cameras.IncludeCamera(Cameras.GameplayCameraType.ShieldPortalCamera);

            yield return new WaitForSeconds(2f);

            _spaceship.transform.position = _spaceshipShieldPortal.transform.position;
            _spaceshipMovement.IsStopped = false;

            _cameras.IncludeCamera(Cameras.GameplayCameraType.MainCamera);
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
            Vector3 startPosition = new Vector3(0, 2, -100);
            Vector3 endPosition = new Vector3(0, 2, 30);
            float duration = 1f;
            float time = 0;

            _spaceshipMovement.IsStopped = true;
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
            endCallback?.Invoke();
        }
    }
}
