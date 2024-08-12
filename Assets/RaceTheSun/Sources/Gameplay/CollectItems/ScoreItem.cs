using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class ScoreItem : MonoBehaviour, IItem
    {
        public void Accept(IItemVisitor visitor)
        {
            visitor.Visit(this);
        }

        public class Factory : PlaceholderFactory<string, UniTask<ScoreItem>>
        {
        }
    }
}