namespace Assets.RaceTheSun.Sources.GameLogic.Attachment
{
    public class ShieldsStorage : AttachmentStatsDecorator
    {
        public const int BoostedMaxShieldsCount = 2;

        public ShieldsStorage(IAttachmentStatsProvider wrappedEntity) : base(wrappedEntity)
        {
        }

        protected override AttachmentStats GetStatsInternal()
        {
            AttachmentStats stats = WrappedEntity.GetStats();
            stats.MaxShileldsCount = BoostedMaxShieldsCount;

            return stats;
        }
    }
}
