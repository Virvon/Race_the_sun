using Assets.RaceTheSun.Sources.Infrastructure.Factories.GameplayFactory;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Upgrading;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.Spawners
{
    public class JumpBoostSpawner : MonoBehaviour
    {
        private IGameplayFactory _gameplayFactory;
        private PersistentProgressService _persistentProgressService;

        [Inject]
        private void Construct(IGameplayFactory gameplayFacory, PersistentProgressService persistentProgressService)
        {
            _gameplayFactory = gameplayFacory;
            _persistentProgressService = persistentProgressService;
        }

        private async void Start()
        {
            if (_persistentProgressService.Progress.Upgrading.IsUpgraded(UpgradeType.JumpBoost))
                await _gameplayFactory.CreateJumpBoost(transform.position, transform);
        }
    }
}
