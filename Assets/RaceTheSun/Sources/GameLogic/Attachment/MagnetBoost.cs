namespace Assets.RaceTheSun.Sources.GameLogic.Attachment
{
    public class MagnetBoost : AttachmentStatsDecorator
    {
        private const float BoostedCollectRadius = 15;

        public MagnetBoost(IAttachmentStatsProvider wrappedEntity)
            : base(wrappedEntity)
        {
        }

        protected override AttachmentStats GetStatsInternal()
        {
            AttachmentStats stats = WrappedEntity.GetStats();
            stats.CollectRadius = BoostedCollectRadius;

            return stats;
        }
    }
}
