using Assets.RaceTheSun.Sources.Gameplay.CollectItems.ItemVisitor;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems.Items
{
    public class MysteryBox : MonoBehaviour, IItem
    {
        [SerializeField] private Color _destroyEffectColor;

        public Color DestroyEffectColor => _destroyEffectColor;

        public void Accept(IItemVisitor visitor) =>
            visitor.Visit(this);
    }
}