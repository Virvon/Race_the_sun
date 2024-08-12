using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class StartBetweenStageTile : StartStageTile
    {
        [SerializeField] private Transform[] _birdMovementPath;

        private Bird.Bird _bird;

        [Inject]
        private void Construct(Bird.Bird bird)
        {
            _bird = bird;
        }

        public override void Invoke()
        {
            base.Invoke();

            Vector3[] movementPath = new Vector3[_birdMovementPath.Length];

            for(int i = 0; i < _birdMovementPath.Length; i++)
                movementPath[i] = _birdMovementPath[i].position;

            _bird.Move(movementPath);
        }
    }
}
