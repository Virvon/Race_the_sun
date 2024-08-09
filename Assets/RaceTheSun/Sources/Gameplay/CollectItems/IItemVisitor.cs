namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public interface IItemVisitor
    {
        void Visit(Shield shield);
        void Visit(JumpBoost jumpBoost);
        void Visit(ScoreItem scoreItem);
        void Visit(SpeedBoost speedBoost);
        void Visit(MysteryBox mysteryBox);
    }
}