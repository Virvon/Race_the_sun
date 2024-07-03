using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private int _cellLength;
        [SerializeField] private int _renderDistance;

        private IGameplayFactory _gameplayFactory;
        private Transform _spaceship;

        private HashSet<GameObject> _tilesMatrix;

        [Inject]
        private void Construct(IGameplayFactory gameplayFactory, Spaceship.Spaceship spaceship)
        {
            _gameplayFactory = gameplayFactory;
            _spaceship = spaceship.transform;

            _tilesMatrix = new();
        }

        private async void Update()
        {
            if (_spaceship == null)
                return;

            await Fill(_spaceship.position, _renderDistance);
            await Empty(_spaceship.position);
        }

        private UniTask Empty(Vector3 spaceshipPositoin)
        {
            HashSet<GameObject> removedTiles = new();

            foreach(GameObject tile in _tilesMatrix)
            {
                int tileGridPosition = WorldToGridPosition(tile.transform.position);
                int spaceshipGridPosition = WorldToGridPosition(spaceshipPositoin);

                if (tileGridPosition < spaceshipGridPosition)
                    removedTiles.Add(tile);
            }

            Remove(removedTiles);

            return UniTask.CompletedTask;
        }

        private void Remove(HashSet<GameObject> removedTiles)
        {
            foreach(GameObject tile in removedTiles)
            {
                _tilesMatrix.Remove(tile);
                Destroy(tile);
            }
        }

        private async UniTask Fill(Vector3 spaceshipPosition, int renderDistance)
        {
            int cellCoundOnAxis = renderDistance / _cellLength;
            int fillStart = WorldToGridPosition(spaceshipPosition);
            
            for(int z = 0; z < cellCoundOnAxis; z++)
            {
                await TryCreate(fillStart + z);
            }
        }

        private async UniTask TryCreate(int gridPosition)
        {
            if (_tilesMatrix.Any(tile => WorldToGridPosition(tile.transform.position) == gridPosition))
                return;

            Vector3 position = GridToWorldPosition(gridPosition);

            GameObject tileObject = await _gameplayFactory.CreateTile(position, transform);

            _tilesMatrix.Add(tileObject);
        }

        private Vector3 GridToWorldPosition(int gridPosition)
        {
            return new Vector3(
                _spaceship.position.x,
                transform.position.y,
                gridPosition * _cellLength);
        }

        private int WorldToGridPosition(Vector3 worldPosition)
        {
            return (int)(worldPosition.z / _cellLength);
        }
    }
}
