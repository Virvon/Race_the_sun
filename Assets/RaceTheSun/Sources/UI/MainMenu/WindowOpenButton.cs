using System;

namespace Assets.RaceTheSun.Sources.UI.MainMenu
{
    public class WindowOpenButton : WindowInteractionButton
    {
        protected override void Interact() =>
            OpenableWindow.Open();
    }
}
