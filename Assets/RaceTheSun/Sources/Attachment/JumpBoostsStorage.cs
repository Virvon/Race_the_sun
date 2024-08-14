namespace Assets.RaceTheSun.Sources.Attachment
{
    public class JumpBoostsStorage : AttachmentStatsDecorator
    {
        private const int BoostedMaxJumpBoostsCount = 2; 

        public JumpBoostsStorage(IAttachmentStatsProvider wrappedEntity) : base(wrappedEntity)
        {
        }

        protected override AttachmentStats GetStatsInternal()
        {
            AttachmentStats stats = WrappedEntity.GetStats();
            stats.MaxJumpBoostsCount = BoostedMaxJumpBoostsCount;
            return stats;
        }
    }
}
