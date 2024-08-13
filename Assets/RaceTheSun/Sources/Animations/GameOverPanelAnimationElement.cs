using System;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Animations
{
    public class GameOverPanelAnimationElement : HudAnimationElement
    {
        public event Action Opened;
        public event Action Hided;

        public void OnOpened()
        {
            Opened?.Invoke();
            Debug.Log("Base invoke");
        }
        
        public void OnHided()
        {
            Hided?.Invoke();
        }
    }
}
