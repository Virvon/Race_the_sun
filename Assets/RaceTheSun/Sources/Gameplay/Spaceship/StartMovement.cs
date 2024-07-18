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

        private CinemachineVirtualCamera _startCamera;

        [Inject]
        private void Construct(StartCamera startCamera)
        {
            _startCamera = startCamera.GetComponent<CinemachineVirtualCamera>();
        }

        public void Move(Action endCallback = null)
        {
            StartCoroutine(SpaceshipMover(endCallback));
        }

        private IEnumerator SpaceshipMover(Action endCallback)
        {
            Vector3 startPosition = new Vector3(0, 2, -100);
            Vector3 endPosition = new Vector3(0, 2, 30);
            float duration = 1f;
            float time = 0;

            _spaceshipMovement.IsStopped = true;
            _startCamera.Priority = (int)CameraPriority.Use;

            while (_spaceship.transform.position != endPosition)
            {
                time += Time.deltaTime;
                float progress = time / duration;

                _spaceship.transform.position = Vector3.Lerp(startPosition, endPosition, progress);

                yield return null;
            }

            _startCamera.Priority = (int)CameraPriority.NotUse;
            _spaceshipMovement.IsStopped = false;
            endCallback?.Invoke();
        }
    }
}
