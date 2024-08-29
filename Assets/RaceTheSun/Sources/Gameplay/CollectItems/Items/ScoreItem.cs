using Cysharp.Threading.Tasks;
using Assets.RaceTheSun.Sources.Gameplay.CollectItems.ItemVisitor;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems.Items
{
    public class ScoreItem : MonoBehaviour, IItem
    {
        [SerializeField] private Color _destroyEffectColor;

        public Color DestroyEffectColor => _destroyEffectColor;

        public void Accept(IItemVisitor visitor) =>
            visitor.Visit(this);

        public class Factory : PlaceholderFactory<string, UniTask<ScoreItem>>
        {
        }
    }
}