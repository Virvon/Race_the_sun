using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using System;

namespace Assets.RaceTheSun.Sources.Gameplay.WorldGenerator
{
    public class CurrentSpaceshipStage
    {
        public event Action<Stage> StageChanged;

        public void SetCurrentStage(Stage stage)
        {
            StageChanged?.Invoke(stage);
        }
    }
}
