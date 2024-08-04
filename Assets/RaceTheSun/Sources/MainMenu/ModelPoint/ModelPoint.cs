using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Infrastructure.Factories.MainMenuFactory;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.MainMenu.ModelPoint
{
    public class ModelPoint : MonoBehaviour
    {
        private IMainMenuFactory _mainMenuFactory;
        private IPersistentProgressService _persistentProgressService;

        private SpaceshipModel _currentModel;

        [Inject]
        private void Construct(IMainMenuFactory mainMenuFactory, IPersistentProgressService persistentProgressService)
        {
            _mainMenuFactory = mainMenuFactory;
            _persistentProgressService = persistentProgressService;
        }

        private async void Start()
        {
            await Change(_persistentProgressService.Progress.AvailableSpaceships.CurrentSpaceshipType);
        }

        public async UniTask Change(SpaceshipType spaceshipType)
        {
            if (_currentModel != null)
                Destroy(_currentModel.gameObject);

            _currentModel = await _mainMenuFactory.CreateSpaceshipModel(spaceshipType, transform.position);
        }

        public class Factory : PlaceholderFactory<string, UniTask<ModelPoint>>
        {
        } 
    }
}
