using Assets.RaceTheSun.Sources.GameLogic.Animations;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private HudAnimationElement[] _hudAnimationElements;

        [Inject]
        private void Construct(HudAnimation hudAnimation)
        {
            foreach (HudAnimationElement hudAnimationElement in _hudAnimationElements)
                hudAnimation.RegisterHudAnimationElement(hudAnimationElement);
        }

        public class Factory : PlaceholderFactory<string, UniTask<Hud>>
        {
        }
    }
}
