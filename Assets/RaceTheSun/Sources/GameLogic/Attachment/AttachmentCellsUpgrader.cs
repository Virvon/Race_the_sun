using Assets.RaceTheSun.Sources.Data;
using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using Assets.RaceTheSun.Sources.Upgrading;

namespace Assets.RaceTheSun.Sources.GameLogic.Attachment
{
    public class AttachmentCellsUpgrader
    {
        private readonly IPersistentProgressService _persistentProgressService;

        public AttachmentCellsUpgrader(IPersistentProgressService persistentProgressService) =>
            _persistentProgressService = persistentProgressService;

        public void TryUpgrade(int stageNumber)
        {
            UpgradingData upgradingData = _persistentProgressService.Progress.Upgrading;

            switch (stageNumber)
            {
                case 2:
                    upgradingData.Upgrade(UpgradeType.FirstAttachmentCell);
                    break;
                case 3:
                    upgradingData.Upgrade(UpgradeType.SecondAttachmentCell);
                    break;
                case 4:
                    upgradingData.Upgrade(UpgradeType.ThirdAttachmentCell);
                    break;
            }
        }
    }
}
