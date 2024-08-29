using Assets.RaceTheSun.Sources.Gameplay.CollectItems.ItemVisitor;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems.Items
{
    public class Shield : MonoBehaviour, IItem
    {
        [SerializeField] private Color _destroyEffectColor;

        public Color DestroyEffectColor => _destroyEffectColor;

        public void Accept(IItemVisitor visitor) =>
            visitor.Visit(this);

        public class Factory : PlaceholderFactory<string, UniTask<Shield>>
        {
        }
    }
}