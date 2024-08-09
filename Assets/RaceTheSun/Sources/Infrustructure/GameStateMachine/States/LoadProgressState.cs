using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine;
using Assets.RaceTheSun.Sources.Infrastructure.GameStateMachine.States;
using Assets.RaceTheSun.Sources.MainMenu.Spaceship;
using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Assets.RaceTheSun.Sources.Infrustructure.GameStateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly Infrastructure.GameStateMachine.GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(Infrastructure.GameStateMachine.GameStateMachine stateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _gameStateMachine = stateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public UniTask Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<MainMenuState>().Forget();

            return default;
        }

        public UniTask Exit()
        {
            return default;
        }

        private void LoadProgressOrInitNew() =>
            _persistentProgressService.Progress = _saveLoadService.LoadProgress() ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress()
        {
            List<SpaceshipData> spaceshipDatas = new();
            SpaceshipConfig[] spaceshipConfigs = _staticDataService.GetSpaceships();
            
            spaceshipDatas.AddRange(spaceshipConfigs.Select(spaceshipConfig => new SpaceshipData(spaceshipConfig.Type, spaceshipConfig.Battery.StartBoost, spaceshipConfig.ExperienceMultiplier.StartBoost, spaceshipConfig.PickUpRange.StartBoost, spaceshipConfig.FloatTime.StartBoost, spaceshipConfig.IsUnlockedOnStart)));

            PlayerProgress progress = new(spaceshipDatas);

            progress.AvailableStatsToUpgrade.Stats.Add(StatType.Battery);
            progress.Wallet.Value = 6000;
            progress.Upgrading.UpgradedTypes.Add(Upgrading.UpgradeType.JumpBoost);
            progress.Upgrading.UpgradedTypes.Add(Upgrading.UpgradeType.ShieldPortal);

            return progress;
        }
    }
}