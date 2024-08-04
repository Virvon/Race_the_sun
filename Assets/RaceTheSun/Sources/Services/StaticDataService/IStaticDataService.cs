﻿using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Cysharp.Threading.Tasks;

namespace Assets.RaceTheSun.Sources.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        StageConfig GetStage(Stage stage);
        SpaceshipConfig GetSpaceship(SpaceshipType type);
        SpaceshipConfig[] GetSpaceships();
    }
}