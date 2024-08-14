using Assets.RaceTheSun.Sources.Upgrading;
using System.Collections.Generic;

namespace Assets.RaceTheSun.Sources.Attachment
{
    public class Attachment
    {
        public IAttachmentStatsProvider Wrap(UpgradeType attachmentUpgradeType, IAttachmentStatsProvider wrappedAttachmentStatsProvider)
        {
            switch (attachmentUpgradeType)
            {
                case UpgradeType.Magnet:
                    return new MagnetBoost(wrappedAttachmentStatsProvider);
                case UpgradeType.JumpBoostsStorage:
                    return new JumpBoostsStorage(wrappedAttachmentStatsProvider); ;
                case UpgradeType.ShieldPortalsStorage:
                    return new ShieldsStorage(wrappedAttachmentStatsProvider);
                default:
                    return wrappedAttachmentStatsProvider;
            }
        }
    }
}