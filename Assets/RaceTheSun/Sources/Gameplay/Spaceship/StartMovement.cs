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

        private Cameras.Cameras _cameras;

        [Inject]
        private void Construct(Cameras.Cameras cameras)
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
            _cameras.IncludeCamera(Cameras.CameraType.UpperCamera);

            yield return new WaitForSeconds(1);
            startCallback?.Invoke();

            _cameras.IncludeCamera(Cameras.CameraType.MainCamera);
            finishCallback?.Invoke();
        }

        private IEnumerator SpaceshipMover(Action endCallback)
        {
            Vector3 startPosition = new Vector3(0, 2, -100);
            Vector3 endPosition = new Vector3(0, 2, 30);
            float duration = 1f;
            float time = 0;

            _spaceshipMovement.IsStopped = true;
            _cameras.IncludeCamera(Cameras.CameraType.StartCamera);

            while (_spaceship.transform.position != endPosition)
            {
                time += Time.deltaTime;
                float progress = time / duration;

                _spaceship.transform.position = Vector3.Lerp(startPosition, endPosition, progress);

                yield return null;
            }

            _cameras.IncludeCamera(Cameras.CameraType.MainCamera);
            _spaceshipMovement.IsStopped = false;
            endCallback?.Invoke();
        }
    }
}
