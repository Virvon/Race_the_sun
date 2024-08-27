using Assets.RaceTheSun.Sources.Trail;
using System;
using System.Collections.Generic;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Wallet Wallet;
        public AvailableStatsToUpgrade AvailableStatsToUpgrade;
        public AvailableSpaceships AvailableSpaceships;
        public UpgradingData Upgrading;
        public LevelProgress LevelProgress;
        public SpaceshipMainCameraSettings SpaceshipMainCameraSettings;
        public AvailableTrails AvailableTrails;
        public MysteryBoxesData MysteryBoxes;
        public AudioSettings AudioSettings;
        public int HighScore;
        public Education Education;

        public PlayerProgress(List<SpaceshipData> spaceshipDatas, List<TrailType> trails)
        {
            Wallet = new();
            AvailableStatsToUpgrade = new();
            AvailableSpaceships = new(spaceshipDatas);
            Upgrading = new();
            LevelProgress = new(Upgrading);
            SpaceshipMainCameraSettings = new();
            AvailableTrails = new(trails);
            MysteryBoxes = new();
            AudioSettings = new();
            HighScore = 0;
            Education = new();
        }
    }
}