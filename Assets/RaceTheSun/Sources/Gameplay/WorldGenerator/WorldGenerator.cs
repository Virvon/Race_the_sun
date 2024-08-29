using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class WorldGenerator : MonoBehaviour
    {
        private int _cellLength;
        private int _renderDistance;

        private IGameplayFactory _gameplayFactory;
        private Transform _spaceship;
        private HashSet<GameObject> _tilesMatrix;
        private bool _isFlowFree;
        private CurrentGenerationStage _currentGenerationStage;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IGameplayFactory gameplayFactory, Spaceship.Spaceship spaceship, IStaticDataService staticDataService, CurrentGenerationStage currentGenerationStage)
        {
            _gameplayFactory = gameplayFactory;
            _spaceship = spaceship.transform;
            _staticDataService = staticDataService;
            _currentGenerationStage = currentGenerationStage;

            GameplayWorldConfig gameplayWorldConfig = _staticDataService.GetGameplayWorld();

            _cellLength = gameplayWorldConfig.CellLength;
            _renderDistance = gameplayWorldConfig.RenderDistacne;

            _tilesMatrix = new();
            _isFlowFree = true;
        }

        private async void Update()
        {
            if (_spaceship == null || _isFlowFree == false)
                return;

            _isFlowFree = false;

            await Fill(_spaceship.position, _renderDistance);
            await Empty(_spaceship.position);

            _isFlowFree = true;
        }

        public void Clean()
        {
            foreach(var tile in _tilesMatrix)
            {
                Destroy(tile.gameObject);
            }

            _tilesMatrix.Clear();
        }

        public void Replace()
        {
            GameObject[] tiles = _tilesMatrix.OrderBy(value => value.transform.position.z).ToArray();

            for(int z = 0; z < tiles.Length; z++)
            {
                tiles[z].transform.position = GridToWorldPosition(z - 1);
            }
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
                Destroy(tile.gameObject);
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

            GameObject tileObject = await _gameplayFactory.CreateTile(_currentGenerationStage.GetTile(), position, transform);

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

        public class Factory : PlaceholderFactory<string, UniTask<WorldGenerator>>
        {
        }
    }
}
