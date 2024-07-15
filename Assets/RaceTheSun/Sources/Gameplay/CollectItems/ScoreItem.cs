using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class ScoreItem : MonoBehaviour, IItem
    {
        public void Accept(IItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}