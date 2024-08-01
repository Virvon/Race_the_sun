using System;
using System.Collections.Generic;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class AvailableSpaceships
    {
        private const SpaceshipType StartSpaceship = SpaceshipType.Swallow;

        public List<SpaceShipData> Spaceships;
        public SpaceshipType CurrentSpaceshipType;

        public AvailableSpaceships()
        {
            Spaceships = new();
            CurrentSpaceshipType = StartSpaceship;
        }
    }
}