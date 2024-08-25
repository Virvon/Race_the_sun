using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public interface IItem
    {
        public Color DestroyEffectColor { get; }
        void Accept(IItemVisitor visitor);
    }
}