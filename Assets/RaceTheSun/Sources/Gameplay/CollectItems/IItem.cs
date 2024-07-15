using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public interface IItem
    {
        void Accept(IItemVisitor visitor);
    }
}