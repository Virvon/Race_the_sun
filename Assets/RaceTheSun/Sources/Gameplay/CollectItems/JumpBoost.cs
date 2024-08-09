using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class JumpBoost : MonoBehaviour, IItem
    {
        public void Accept(IItemVisitor visitor)
        {
            visitor.Visit(this);
        }

        public class Factory : PlaceholderFactory<string, UniTask<JumpBoost>>
        {

        }
    }
}