using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class MysteryBox : MonoBehaviour, IItem
    {
        public void Accept(IItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}