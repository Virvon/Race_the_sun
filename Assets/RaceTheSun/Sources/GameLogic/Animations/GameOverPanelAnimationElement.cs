using System;

namespace Assets.RaceTheSun.Sources.GameLogic.Animations
{
    public class GameOverPanelAnimationElement : HudAnimationElement
    {
        public event Action Opened;
        public event Action Hided;

        public void OnOpened() =>
            Opened?.Invoke();

        public void OnHided() =>
            Hided?.Invoke();
    }
}
