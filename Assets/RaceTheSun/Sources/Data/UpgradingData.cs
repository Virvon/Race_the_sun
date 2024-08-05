using Assets.RaceTheSun.Sources.Upgrading;
using System;
using System.Collections.Generic;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class UpgradingData
    {
        public List<UpgradeType> UpgradedTypes;

        public UpgradingData()
        {
            UpgradedTypes = new();
        }

        public void Upgrade(int level)
        {
            switch (level)
            {
                case (int)UpgradeType.JumpBoost:
                    Upgrade(UpgradeType.JumpBoost);
                    break;
                case (int)UpgradeType.ShieldPortal:
                    Upgrade(UpgradeType.ShieldPortal);
                    break;
                case (int)UpgradeType.Magnet:
                    Upgrade(UpgradeType.Magnet);
                    break;
                case (int)UpgradeType.JumpBoostsStorage:
                    Upgrade(UpgradeType.JumpBoostsStorage);
                    break;
                case (int)UpgradeType.ShieldPortalsStorage:
                    Upgrade(UpgradeType.ShieldPortalsStorage);
                    break;
            }
        }

        public void Upgrade(UpgradeType type)
        {
            if (UpgradedTypes.Contains(type))
                return;

            UpgradedTypes.Add(type);
        }

        public bool IsUpgraded(UpgradeType type) =>
            UpgradedTypes.Contains(type);
    }
}