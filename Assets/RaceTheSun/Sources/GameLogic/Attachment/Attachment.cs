using Assets.RaceTheSun.Sources.Services.StaticDataService;
using Assets.RaceTheSun.Sources.Services.StaticDataService.Configs;
using Assets.RaceTheSun.Sources.Upgrading;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.GameLogic.Attachment
{
    public class Attachment
    {
        private readonly IStaticDataService _staticDataService;

        public Attachment(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public IAttachmentStatsProvider Wrap(
            UpgradeType attachmentUpgradeType,
            IAttachmentStatsProvider wrappedAttachmentStatsProvider)
        {
            switch (attachmentUpgradeType)
            {
                case UpgradeType.Magnet:
                    return new MagnetBoost(wrappedAttachmentStatsProvider);
                case UpgradeType.JumpBoostsStorage:
                    return new JumpBoostsStorage(wrappedAttachmentStatsProvider);
                case UpgradeType.ShieldPortalsStorage:
                    return new ShieldsStorage(wrappedAttachmentStatsProvider);
                default:
                    return wrappedAttachmentStatsProvider;
            }
        }

        public Sprite GetIcon(UpgradeType attachmentUpgradeType)
        {
            AttachmentConfig attachmentConfig = _staticDataService.GetAttachment(attachmentUpgradeType);

            return attachmentConfig != null ? attachmentConfig.Icon : null;
        }
    }
}