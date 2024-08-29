using System;
using System.Collections.Generic;
using Assets.RaceTheSun.Sources.Upgrading;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class UpgradingData
    {
        public List<UpgradeType> UpgradedTypes;
        public int AttachmentCellsCount;

        public UpgradingData()
        {
            UpgradedTypes = new ();
            AttachmentCellsCount = 0;
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

            switch (type)
            {
                case UpgradeType.FirstAttachmentCell:
                    AttachmentCellsCount++;
                    break;
                case UpgradeType.SecondAttachmentCell:
                    AttachmentCellsCount++;
                    break;
                case UpgradeType.ThirdAttachmentCell:
                    AttachmentCellsCount++;
                    break;
            }

            UpgradedTypes.Add(type);
        }

        public bool IsUpgraded(UpgradeType type) =>
            UpgradedTypes.Contains(type);
    }
}