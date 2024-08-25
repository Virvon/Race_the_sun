﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.CollectItems
{
    public class SpeedBoost : MonoBehaviour, IItem
    {
        [SerializeField] private Color _destroyEffectColor;

        public Color DestroyEffectColor => _destroyEffectColor;

        public void Accept(IItemVisitor visitor)
        {
            visitor.Visit(this);
        }

        public class Factory : PlaceholderFactory<string, UniTask<SpeedBoost>>
        {
        }
    }
}