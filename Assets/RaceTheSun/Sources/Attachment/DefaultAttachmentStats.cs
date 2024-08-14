namespace Assets.RaceTheSun.Sources.Attachment
{
    public class DefaultAttachmentStats : IAttachmentStatsProvider
    {
        private const float CollectRadius = 5;
        private const int MaxJumpBoostsCount = 1;
        private const int MaxShieldsCount = 1;

        private readonly AttachmentStats _attachmentStats;

        public DefaultAttachmentStats()
        {
            _attachmentStats = new(CollectRadius, MaxJumpBoostsCount, MaxShieldsCount);
        }

        public AttachmentStats GetStats()
        {
            return _attachmentStats;
        }
    }
}
