using System;
using System.Collections.Generic;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class AvailableSpaceShips
    {
        public List<SpaceShipData> Spaceships;

        public AvailableSpaceShips()
        {
            Spaceships = new();
        }
    }
}