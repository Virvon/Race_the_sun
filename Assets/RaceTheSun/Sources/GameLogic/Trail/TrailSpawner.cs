using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.SpaceshipModelFactory;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.GameLogic.Trail
{
    public class TrailSpawner : MonoBehaviour
    {
        [SerializeField] private SpaceshipType _spaceshipType;
        [SerializeField] private Transform[] _trailPoints;

        private ISpaceshipModelFactory _spaceshipModelFactory;
        private IPersistentProgressService _persistentProgressService;
        private List<Trail> _createdeTrails;

        [Inject]
        private void Construct(ISpaceshipModelFactory spaceshipModelFactory, IPersistentProgressService persistentProgressService)
        {
            _spaceshipModelFactory = spaceshipModelFactory;
            _persistentProgressService = persistentProgressService;

            _createdeTrails = new List<Trail>();
        }

        private void Start()
        {
            CreateTrails(_persistentProgressService.Progress.AvailableSpaceships.GetSpaceshipData(_spaceshipType).TrailType);
        }

        public async void CreateTrails(TrailType type)
        {
            foreach (Trail createdTrail in _createdeTrails)
                Destroy(createdTrail.gameObject);

            _createdeTrails.Clear();

            foreach (Transform trailPoint in _trailPoints)
            {
                Trail trail = await _spaceshipModelFactory.CreateTrail(type, trailPoint.position, trailPoint);

                _createdeTrails.Add(trail);
            }
        }
    }
}
