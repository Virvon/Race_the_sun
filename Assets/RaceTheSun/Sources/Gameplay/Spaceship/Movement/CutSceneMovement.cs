using System;
using System.Collections;
using Assets.RaceTheSun.Sources.GameLogic.Cameras.Gameplay;
using Assets.RaceTheSun.Sources.Gameplay.Portals;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.Spaceship.Movement
{
    public class CutSceneMovement : MonoBehaviour
    {
        private const int StartMoveDuration = 3;
        private const int UpperCameraShowDuraiton = 1;

        private readonly Vector3 _startMoveStartPosition = new Vector3(0, 6.2f, -900);
        private readonly Vector3 _startMoveEndPosition = new Vector3(0, 6.2f, 100);
        private readonly Vector3 _upperMoveStartPosition = new Vector3(0, 6.2f, 0);

        [SerializeField] private Spaceship _spaceship;
        [SerializeField] private SpaceshipMovement _spaceshipMovement;
        [SerializeField] private CollisionPortalPoint _collisionPortalPoint;
        [SerializeField] private SpaceshipTurning _spaceshipTurning;

        private GameplayCameras _cameras;

        [Inject]
        private void Construct(GameplayCameras cameras) =>
            _cameras = cameras;

        public void MoveStart(Action endCallback = null) =>
            StartCoroutine(SpaceshipStartMover(endCallback));

        public void MoveUpper(Action startCallback = null, Action finishCallback = null) =>
            StartCoroutine(UpperMover(startCallback, finishCallback));

        private IEnumerator UpperMover(Action startCallback, Action finishCallback)
        {
            _spaceship.transform.position = _upperMoveStartPosition;
            _cameras.IncludeCamera(GameplayCameraType.UpperCamera);

            yield return new WaitForSeconds(UpperCameraShowDuraiton);

            startCallback?.Invoke();

            _cameras.IncludeCamera(GameplayCameraType.MainCamera);

            finishCallback?.Invoke();
        }

        private IEnumerator SpaceshipStartMover(Action endCallback)
        {
            float time = 0;

            _spaceshipMovement.IsStopped = true;
            _spaceshipTurning.CanTurn = false;
            _cameras.IncludeCamera(GameplayCameraType.StartCamera);

            while (_spaceship.transform.position != _startMoveEndPosition)
            {
                time += Time.deltaTime;
                float progress = time / StartMoveDuration;

                _spaceship.transform.position = Vector3.Lerp(_startMoveStartPosition, _startMoveEndPosition, progress);

                yield return null;
            }

            _cameras.IncludeCamera(GameplayCameraType.MainCamera);
            _spaceshipMovement.IsStopped = false;
            _spaceshipTurning.CanTurn = true;
            endCallback?.Invoke();
        }
    }
}
