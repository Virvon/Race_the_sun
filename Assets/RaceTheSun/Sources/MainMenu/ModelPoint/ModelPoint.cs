using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Trail;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu.ModelPoint
{
    public class ModelPoint : MonoBehaviour
    {
        private IPersistentProgressService _persistentProgressService;
        private ISpaceshipModelFactory _spaceshipModelFactory;
        private SpaceshipModel _currentModel;

        [Inject]
        private void Construct(PersistentProgressService persistentProgressService, ISpaceshipModelFactory spaceshipModelFactory)
        {
            _persistentProgressService = persistentProgressService;
            _spaceshipModelFactory = spaceshipModelFactory;
        }

        private async void Start()
        {
            await Change(_persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType);
        }

        public async UniTask Change(SpaceshipType spaceshipType)
        {
            if (_currentModel != null)
                Destroy(_currentModel.gameObject);

            _currentModel = await _spaceshipModelFactory.CreateSpaceshipModel(spaceshipType, transform.position, transform);
        }

        public void ChangeTrail(TrailType trailType)
        {
            _currentModel.GetComponentInChildren<TrailSpawner>().CreateTrails(trailType);
        }

        public class Factory : PlaceholderFactory<string, UniTask<ModelPoint>>
        {
        }
    }
}
