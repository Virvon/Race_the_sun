﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class AvailableSpaceships
    {
        private const SpaceshipType StartSpaceship = SpaceshipType.Swallow;

        public List<SpaceshipData> Spaceships;
        public SpaceshipType CurrentSpaceshipType;

        public event Action<SpaceshipType> SpaceshipUnlocked;

        public AvailableSpaceships(List<SpaceshipData> SpaceshipDatas)
        {
            Spaceships = SpaceshipDatas;
            CurrentSpaceshipType = StartSpaceship;
        }

        public SpaceshipData GetSpaceshipData(SpaceshipType type) => 
            Spaceships.First(spaceshipData => spaceshipData.Type == type);

        public void Unlock(SpaceshipType type)
        {
            GetSpaceshipData(type).IsUnlocked = true;
            SpaceshipUnlocked?.Invoke(type);
        }

        public void Selcect(SpaceshipType type)
        {
            if (GetSpaceshipData(type).IsUnlocked)
                CurrentSpaceshipType = type;
        }
    }
}