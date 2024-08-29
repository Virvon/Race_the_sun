namespace Assets.RaceTheSun.Sources.GameLogic.Attachment
{
    public class AttachmentStats
    {
        public float CollectRadius;
        public int MaxJumpBoostsCount;
        public int MaxShileldsCount;

        public AttachmentStats(float collectRadius, int maxJumpBoostsCount, int maxShileldsCount)
        {
            CollectRadius = collectRadius;
            MaxJumpBoostsCount = maxJumpBoostsCount;
            MaxShileldsCount = maxShileldsCount;
        }
    }
}
