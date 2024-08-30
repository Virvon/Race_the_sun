using System;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.StageInfo
{
    public class CurrentSpaceshipStage
    {
        public event Action<Stage> StageChanged;

        public void SetCurrentStage(Stage stage) =>
            StageChanged?.Invoke(stage);
    }
}
